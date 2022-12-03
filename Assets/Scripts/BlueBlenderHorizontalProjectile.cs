using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBlenderHorizontalProjectile : MonoBehaviour
{
	[SerializeField] public int dam = 2;
	[SerializeField] public GameObject jumpShotSpot;
	[SerializeField] public GameObject crouchShotSpot;

	public float speed = 8f;
	public KeptData data;
	public bool animComplete = false;
	public bool jumpShotPlaced = false;
	public bool crouchShotPlaced = false;


	private void Start()
	{
		GameObject boss = GameObject.FindGameObjectWithTag("Enemy");
		Physics2D.IgnoreCollision(boss.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());

		if (data.halfDamageBought == false)
		{
			dam = 2;
		}
		if (data.halfDamageBought == true)
		{
			dam = 1;
		}
	}
	void Update()
    {
		if (animComplete)
		{
			if(gameObject.tag == "Jump Projectile")
			{
				if (!jumpShotPlaced)
				{
					transform.position = Vector3.MoveTowards(transform.position, jumpShotSpot.transform.position, 15f * Time.deltaTime);

					if (transform.position == jumpShotSpot.transform.position)
					{
						jumpShotPlaced = true;
					}
				}

				else
				{
					transform.position += -transform.right * Time.deltaTime * speed; //Shoots the projectile forward based on player position.
				}

			}

			else if (gameObject.tag == "Crouch Projectile")
			{
				if (!crouchShotPlaced)
				{
					transform.position = Vector3.MoveTowards(transform.position, crouchShotSpot.transform.position, 15f * Time.deltaTime);

					if (transform.position == crouchShotSpot.transform.position)
					{
						crouchShotPlaced = true;
					}
				}

				else
				{
					transform.position += -transform.right * Time.deltaTime * speed; //Shoots the projectile forward based on player position.
				}
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) //Gives the player damage
	{
		if (collision.transform.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerController>().TakeDamage(dam);
			Destroy(gameObject);
		}

		else if(collision.transform.tag == "Damaging Entity")
		{
			Destroy(gameObject);
		}
	}

	void animFinished()
	{
		animComplete = true;
	}

	//When will the cringe sunk beneath Unity's documentation disperse?
}
