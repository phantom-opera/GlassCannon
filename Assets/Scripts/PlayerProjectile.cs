using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	public float speed = 8f;
	public int damage = 1;
	public KeptData data;

	// Update is called once per frame
	void Update()
    {
		transform.position += transform.right * Time.deltaTime * speed; //Shoots the projectile forward based on player position.
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if( collision.transform.tag == "Enemy")
		{
			if(data.doubAttBought == false){
				collision.gameObject.GetComponent<BossController>().TakeDamage(damage + data.attackMod);
				Debug.Log("should do it");
			}
			if (data.doubAttBought == true){
				collision.gameObject.GetComponent<BossController>().TakeDamage(2*(damage + data.attackMod));
			}
		}
		Destroy(gameObject);
	}
}
