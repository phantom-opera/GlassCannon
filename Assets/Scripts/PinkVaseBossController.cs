using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkVaseBossController : BossController
{
	public GameObject vinePrefab;
	private double firstBreakpoint;
	private double secondBreakpoint;
	private int vinesSpawned;

	[SerializeField] private GameObject firstVineArm;
	[SerializeField] private GameObject secondVineArm;
	[SerializeField] private Transform startPoint;
	[SerializeField] private Animator anim;
	private void Start()
	{
		transform.position = startPoint.position;
		Vector3 newPos = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z);
		transform.position = newPos;
		gameObject.transform.parent = startPoint.transform;
		firstBreakpoint = health - (health * 0.25);
		secondBreakpoint = health - (health * 0.75);
		shootCooldown = 1;
		anim.Play("PinkIdle");
	}

	public override void Update()
	{
		base.Update();

		if (currentCooldown <= 0)
		{
			currentCooldown = shootCooldown;
			BossProjectile bullet = Instantiate(bulletPrefab, fireOffset.position, Quaternion.identity);

		}

		else
		{
			currentCooldown -= Time.deltaTime;
		}
		

		if (health == firstBreakpoint && vinesSpawned == 0)
		{
			vinesSpawned++;
			firstVineArm.SetActive(true);

		}

		if (health == secondBreakpoint && vinesSpawned == 1)
		{
			vinesSpawned++;
			secondVineArm.SetActive(true);
		}
	}
}