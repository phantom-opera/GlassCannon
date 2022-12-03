using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHollowScript : MonoBehaviour
{
	public GameObject[] vineArms;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Vine" || collision.gameObject.tag == "Enemy")
		{
			if(vineArms[0] == null)
			{
				vineArms[0] = collision.gameObject;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		vineArms[0] = null;
	}
}
