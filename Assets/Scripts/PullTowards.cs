using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullTowards : MonoBehaviour
{
	[SerializeField] float windSpeed = 3;
	[SerializeField] private Rigidbody2D playerRB;
	[SerializeField] private GameObject player;
	private Vector2 dir;

	private void Start()
	{
		playerRB = player.GetComponent<Rigidbody2D>();
	}

	void Update()
	{

		dir = transform.position - player.gameObject.transform.position;
		dir.y = 0;
		playerRB.AddForce(dir.normalized * windSpeed);

	}
	/*void OnTriggerStay2D(Collider2D c)
	{
		rb = c.GetComponent<Rigidbody2D>();
		dir = transform.position - c.gameObject.transform.position;
		dir.y = 0;
		pulling = true;
		//StartCoroutine(Pull());
	} */

	//Fun fact, I originally wanted this script to PUSH the player away but I accidentally made it pull instead. This is why im not a math major yall.
}
