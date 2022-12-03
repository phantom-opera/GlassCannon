using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAway : MonoBehaviour
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
		dir = player.gameObject.transform.position - transform.position;
		dir.y = 0;
		playerRB.AddForce(dir.normalized * windSpeed);
		
	}
}
