using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBossController : BossController
{
	public float walkSpeed;

	[HideInInspector]
	public bool mustPatrol;
	private bool mustTurn;

	public Rigidbody2D rb;
	public Transform wallCheckPos;
	public LayerMask groundLayer; //This honestly needs to be renamed to wall layer instead of ground layer but im way too lazy at least the script works for now
	public OrangeBowlBallProjectile ballPrefab;
	public Transform ballOffset;

	void Start()
    {
		mustPatrol = true;
		OrangeBowlBallProjectile ball = Instantiate(ballPrefab, ballOffset.position, transform.rotation);
	}

	public override void Update()
	{
		base.Update();
		if (mustPatrol)
        {
			Patrol();
        }
    }

	private void FixedUpdate()
    {
		if (mustPatrol)
        {
			mustTurn = Physics2D.OverlapCircle(wallCheckPos.position, 0.1f, groundLayer); //If the overlap circle hits a wall then Orange Bowl will change directions
        };
    }

	void Patrol()
    {
		if(mustTurn)
        {
			Flip();
        }

		rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
	void Flip()
    {
		mustPatrol = false;
		transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
		walkSpeed *= -1;
		mustPatrol = true;
    }
}
