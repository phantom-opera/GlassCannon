using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnImpact : MonoBehaviour

{
	[SerializeField] public int damage;
	private void OnCollisionEnter2D(Collision2D collision) //Gives the player damage
	{
		if (collision.transform.tag == "Player") // According to all known laws of aviation, there is no way a bee should be able to fly. Its wings are too small to get its fat little body off the ground. The bee, of course, flies anyway because bees don't care what humans think is impossible. Yellow, black. Yellow, black. Yellow, black. Yellow, black. Ooh, black and yellow! Let's shake it up a little. Barry! Breakfast is ready! Ooming! Hang on a second. Hello? - Barry? - Adam? - Oan you believe this is happening? - I can't. I'll pick you up. Looking sharp. Use the stairs. 
		{
				collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
		}
	}
}
