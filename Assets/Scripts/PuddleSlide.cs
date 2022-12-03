using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleSlide : MonoBehaviour
{

	[SerializeField] private Rigidbody2D playerRB;
	[SerializeField] private GameObject player;
	[SerializeField] private float slideSpeed;
	private Vector2 dir;
	private float timer;

	private void Start()
	{
		timer = 3;
		playerRB = player.GetComponent<Rigidbody2D>();
		dir = transform.position - player.gameObject.transform.position;
		dir.y = 0;
	}
	void Update()
    {
		timer -= Time.deltaTime;

		if(timer <= 0)
		{
			Destroy(gameObject);
		}
    }

	private void OnTriggerEnter2D(Collider2D collision) //Gives the player damage
	{
		if (collision.transform.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerController>().jumpForce = 0;
		}

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.transform.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerController>().jumpForce = 5;
		}
	}

}
