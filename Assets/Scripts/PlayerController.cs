using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private enum State { idle, walking, jumping, shooting, jumpshooting, runshooting, hurt, doublejump, crouching, crouchshooting };
	private State state = State.idle;
	int animLayer = 0;
	public Animator anim;
	private SpriteRenderer sprite;

	private float horizontal;
	public PlayerProjectile projectilePrefab;
	public PlayerProjectile superProjectilePrefab;
	public Transform fireOffset;
	int superAttackCount = 1;

	private bool isFacingRight = true;

	private float currentCooldown;
	private float healAmount;

	public Vector2 standingSize;
	public Vector2 crouchingSize;
	public Vector2 offsetSize;

	private bool canDash = true;

	private float defendCount = 1;


	public BoxCollider2D col;
	[SerializeField] private float pushForce = 3f;

	// SerializeField shows the variables in the inspector on Unity which is helpful for debugging.
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] public float health = 6f;
	[SerializeField] private float speed = 500f;

	[SerializeField] public float groundRadius;
	[SerializeField] private LayerMask ground;
	[SerializeField] public bool grounded = true;
	[SerializeField] public float jumpForce;
	[SerializeField] private bool canDoubleJump = false;

	[SerializeField] private float shootCooldown = 0.5f;
	[SerializeField] bool knockback;

	[SerializeField] public bool isInvincible = false;
	[SerializeField] private bool isDefending = false;

	[SerializeField] private bool isDashing;
	[SerializeField] private TrailRenderer trail;

	[SerializeField] private AudioSource footsteps;
	[SerializeField] public AudioSource jumpSound;
	[SerializeField] public AudioSource doubleJumpSound;
	[SerializeField] private AudioSource hurtSound;
	[SerializeField] public AudioSource victorySound;
	[SerializeField] public AudioSource bubbleSound;
	[SerializeField] public AudioSource crouchSound;
	[SerializeField] public AudioSource dashSound;

	[SerializeField] private float dashingPower = 5f;

	[SerializeField] private GameObject poof;

	[SerializeField] public BossController boss;
	private float dashingTime = 0.2f;
	private float dashingCooldown = 1f;
	public float maxHealth;

	private Vector2 dir;

	public KeptData data;
	public UIGameManager uiGM;

	public float counter;

	public float currentHealth;




	void Start()
	{
		trail = GetComponent<TrailRenderer>();
		anim = GetComponent<Animator>();
		col = GetComponent<BoxCollider2D>();
		standingSize = col.size;
		col.size = standingSize;

		maxHealth = health + data.healthMod;
		currentHealth = maxHealth;
		//Debug.Log(maxHealth);
		uiGM.SetMaxHealth(maxHealth);
		uiGM.SetHealth(maxHealth);
	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log(health);
		if (isDashing)
		{
			return;
		}

		//Movement
		Move();

		//Shooting
		if (Input.GetKey(KeyCode.J) && currentCooldown <= 0) //If J is pressed while shooting isn't on cooldown...
		{
			bubbleSound.Play();

			if (grounded && state == State.idle)
			{
				counter = 0.4f;
				state = State.shooting;
			}

			else if (state == State.jumping || state == State.jumpshooting || state == State.doublejump)
			{
				state = State.jumpshooting;
			}

			else if (grounded && state == State.crouching  /*|| state == State.crouchshooting || state == State.shooting*/)
			{
				counter = 0.4f;
				state = State.crouchshooting;
			}

			currentCooldown = shootCooldown;
			PlayerProjectile bullet = Instantiate(projectilePrefab, fireOffset.position, transform.rotation); //Spawns a bullet prefab using the fire offset object's position and player character's rotation.
		}

		else
		{
			currentCooldown -= Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.K) && data.heldElementalPowers > 0)
		{
			//superAttackCount--;
			bubbleSound.Play();
			PlayerProjectile bullet = Instantiate(superProjectilePrefab, fireOffset.position, transform.rotation);
			data.heldElementalPowers--;
		}

		//Jumping
		grounded = Physics2D.OverlapCircle(rb.position, groundRadius, ground); //Draws a circle around the player taking in the player's position and ground radius as input. The circle checks to see if a LayerMask of type ground is touching it.

		if (grounded)
		{
			if (Input.GetButtonDown("Jump")) //If grounded and inputting the jump, then jump.
			{
				Jump();
				canDoubleJump = true;
			}
		}

		else if (Input.GetButtonDown("Jump") && canDoubleJump && data.doubleJumpBought == true) //If inputting jump and already in the air then go ahead and break the laws of physics and jump again.
		{
			Jump();
			doubleJumpSound.Play();
			state = State.doublejump;
			canDoubleJump = false;
		}


		//Crouching
		if (Input.GetKey(KeyCode.S) && grounded && state != State.jumping && state != State.doublejump && state != State.crouchshooting) //Crouches when S button is held
		{
			state = State.crouching;
			crouchSound.Play();
			rb.velocity = new Vector2(0,0);
			transform.position = new Vector2(transform.position.x, -3.55f);
			col.size = crouchingSize;
			//Collider.offset = offsetSize;
		}

		if (Input.GetKeyUp(KeyCode.S)) //Uncrouches when S is no longer held who would have thought.
		{
			if(state == State.crouching || state == State.crouchshooting)
			{
				state = State.idle;
				transform.position = new Vector2(transform.position.x, -3.17f);
				col.size = standingSize;
				//Collider.offset = new Vector2(0, 0);
			}
		}

		if (knockback == true)
		{
			rb.AddForce(dir * pushForce, ForceMode2D.Impulse);
		}

		Flip();

		//Defending
		if (Input.GetKeyDown(KeyCode.O) && data.heldElementalPowers > 0)
		{
			data.heldElementalPowers--;
			StartCoroutine(Defend());

			/*if(defendCount != 0)
			{
				defendCount--;
			}
			*/
		}

		//Healing
		if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M)){
			if(data.healingEssence > 0){
				healEssence();
				currentHealth = health + data.healthMod;
				uiGM.SetHealth(currentHealth);

				//Debug.Log(health);
			}
		}

		StateChange();
		anim.SetInteger("state", (int)state);
	}

	private void Move()
	{
		horizontal = Input.GetAxisRaw("Horizontal"); //Returns -1 if the input is A and 1 if the input is D

		if(state != State.crouching && state != State.crouchshooting)
		{
			rb.velocity = new Vector2(horizontal * (speed + data.speedMod), rb.velocity.y); // Moves the player on the x-axis by taking a keyboard input and multipying it by the speed variable.
		}

		if (Input.GetKeyDown(KeyCode.RightShift) && canDash)
        {
			state = State.idle;
            StartCoroutine(Dash());
        }
	}

	private void Jump()
	{
		grounded = false;
		jumpSound.Play();
		rb.velocity = new Vector2(rb.velocity.x, jumpForce);
	}

	private void Flip()
	{
		if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) // Rotates the character's rotation by 180 degrees based on if the character is looking left or right
		{
			isFacingRight = !isFacingRight;
			transform.Rotate(0, -180f, 0);
		}
		transform.Rotate(0, 0, 0);
	}
	
	public void TakeDamage(int damage) //Takes damage and disappears when health equals zero
	{
		//gameObject.GetComponent<FlashOnHit>().Flash();
	    GameObject damagePoof = Instantiate(poof, transform.position, transform.rotation);
		hurtSound.Play();

		if (isInvincible == false)
		{
			if (isDefending)
			{
				health -= damage - 3;
			}

			else
			{
				health -= damage;
				state = State.hurt;
			}
			StartCoroutine(Invincible());
		}
		currentHealth = health + data.healthMod;
		uiGM.SetHealth(currentHealth);

		if (currentHealth <= 0)
		{
			boss.defeatSound.Play();
			StopMovement();
			anim.Play("Death");
		}
	}

	void Death()
	{
		gameObject.SetActive(false);
	}

	void StopMovement()
	{
		col.enabled = false;
		if (grounded)
		{
			Destroy(rb);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Damaging Entity")
		{
			Knockback(collision);
		}

		else if(collision.gameObject.tag == "Enemy")
		{
			Knockback(collision);
		}
	}

	private void Knockback(Collision2D collision)
	{
		dir = (transform.position - collision.gameObject.transform.position); //Boy howdy programming a solution for this took way longer than it should have. do yall even read this? well if yall do i hope you're having a nice day/night whatever. Also uh this function basically calculates the direction the player should be knocked back into and pushes them in that direction
		dir.y = 0;

		StartCoroutine(Knockback());
	}

	private void Footstep()
	{
		if (grounded)
		{
			footsteps.Play();
		}
	}
	bool isPlaying(Animator anim, string stateName) // Function that checks whether or not a specific animation is playing
	{
		if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
				anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
			return true;
		else
			return false;
	}

	private void StateChange()
	{
	    if (state == State.hurt)
		{
			if (Mathf.Abs(rb.velocity.x) < .1f)
			{
				state = State.idle;
			}
		}

		else  if (!grounded && state != State.jumpshooting && state !=State.doublejump)
		{
			state = State.jumping;
		}

		else if (state == State.jumpshooting || state == State.jumping || state == State.doublejump)
		{
			if (grounded)
			{
				if (Input.GetKey(KeyCode.J))
				{
					state = State.shooting;
				}
				state = State.idle;
			}
		}

		else if (Mathf.Abs(rb.velocity.x) > 2f && state != State.jumpshooting && state != State.runshooting && state != State.crouching && grounded == true)
		{
			state = State.walking;

			if (state == State.walking && Input.GetKey(KeyCode.J) && grounded)
			{
				state = State.runshooting;
			}

			if (state == State.shooting && Input.GetKey(KeyCode.J) && grounded)
			{
				state = State.runshooting;
			}

		}

		else if(state == State.walking)
		{
			if(Mathf.Abs(rb.velocity.x) < 2f && state != State.crouching)
			{
				state = State.idle;
			}
		}

		else if (state == State.runshooting)
		{
			if (rb.velocity.x == 0)
			{
				state = State.idle;
			}
		}

		else if(state == State.shooting || state == State.crouchshooting)
		{
			counter -= Time.deltaTime;

			if(counter <= 0 && state == State.shooting)
			{
				state = State.idle;
			}

			else if(counter <= 0 && state == State.crouchshooting)
			{
				state = State.crouching;
			}
		}

		/*else
		{
			state = State.idle;
		}*/

	}

	IEnumerator Knockback()
	{
		knockback = true;
		yield return new WaitForSecondsRealtime(0.3f);
		knockback = false;
	}

	IEnumerator Invincible() //Prevents the player from taking damage for 0.3 seconds
	{
		isInvincible = true;
		yield return new WaitForSecondsRealtime(0.5f);
		isInvincible = false;
	}

	private IEnumerator Dash()
	{
		if(data.dashBought == true){
			canDash = false;
			dashSound.Play();
			isDashing = true;
			float originalGravity = rb.gravityScale;
			rb.gravityScale = 0f; //Makes it so gravity doesnt affect GlassCannon while dashing.

			//Switches dashing direction based on where GlassCannon is facing.
			if (isFacingRight)
			{
			rb.isKinematic = false;
			rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
			}
			else
			{
				rb.isKinematic = false;
				rb.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
			}

			//Enables the dashing trail behind him.
			trail.emitting = true;
			yield return new WaitForSeconds(dashingTime);
			trail.emitting = false;
			rb.isKinematic = false;
			rb.gravityScale = originalGravity;
			isDashing = false;
			yield return new WaitForSeconds(dashingCooldown);
			canDash = true;
		}
	}

	private IEnumerator Defend()
	{
		isDefending = true;
		gameObject.GetComponent<FlashOnHit>().MultiFlash();
		yield return new WaitForSecondsRealtime(2f);
		isDefending = false;
	}

	private void healEssence(){
		if((maxHealth - health) < 6){
			healAmount = maxHealth - health;
		}
		else{
			healAmount = 6;
		}
		health += healAmount;
		data.healingEssence -= 1;


	}

	public float getHealth(){
		//Debug.Log(currentHealth);
		return currentHealth + data.healthMod;
	}
}
