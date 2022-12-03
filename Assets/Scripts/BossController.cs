using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
	[SerializeField] protected float health = 500f;
	[SerializeField] protected float shootCooldown = 3f;
	[SerializeField] protected float countdown = 5f;
	[SerializeField] protected int counter = 0;

	public BossProjectile bulletPrefab;
	public Transform fireOffset;
	public Transform projectile;
	protected float currentCooldown = 0;
	protected float duration;
	public GameObject healthbar;
	public PlayerController player;
	public AudioSource defeatSound;
	public UIGameManager uiGM;
	void Start()
	{
		uiGM.SetBossMaxHealth(health);
		uiGM.SetBossHealth(health);
	}
	// Update is called once per frame
	public virtual void Update()
    {
		//Debug.Log(health);
		if (health <= 0)
		{
			player.victorySound.Play();
			player.anim.Play("Victory");
			gameObject.SetActive(false);
		}
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		uiGM.SetBossHealth(health);
		//  the statement below helps reflect orange bowl's health bar
		if (gameObject.name == "Orange Bowl"){
			// get the object
			healthbar = GameObject.Find("OrangeBowlHealthBar");
			healthbar.GetComponent<HealthBar>().SetHealth(health);
		}
	}


	public float getHealth(){
		return health;
	}
}
