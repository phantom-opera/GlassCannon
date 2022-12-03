using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBlenderController : BossController
{
	[SerializeField] float value;
	[SerializeField] float icePuddleCountDown;
	[SerializeField] BlueBlenderHorizontalProjectile JumpProjectilePrefab;
	[SerializeField] BlueBlenderHorizontalProjectile CrouchProjectilePrefab;
	[SerializeField] IcePuddleShot puddleProjectilePrefab;
	[SerializeField] bool isHoriShotting = false;
	[SerializeField] GameObject background;

	[SerializeField] public AudioSource iceShotSound;
	[SerializeField] public AudioSource windSound;

	public Transform horizontalOffset;
	public Transform horizontalOffset2;
	public Animator anim;

	float timeRemaining;
	float horiCountdown = 8;
	float horiTimer;
	PullTowards pull;
	PushAway push;
	int spiral;
	int runningCount;

	// Start is called before the first frame update
	void Start()
    {
		anim = GetComponent<Animator>();
		pull = GetComponent<PullTowards>();
		push = GetComponent<PushAway>();
		value = UnityEngine.Random.Range(0, 2);
		spiral = 0;
		shootCooldown = 1;

		timeRemaining = countdown;

		horiTimer = horiCountdown;
		runningCount = 0;

		if(value == 0)
		{
			background.GetComponent<Animator>().Play("WindRight");
			pull.enabled = true;
		}

		else
		{
			background.GetComponent<Animator>().Play("WindLeft");
			push.enabled = true;
		}

	}

    // Update is called once per frame
	public override void Update()
    {
		base.Update();

		horiTimer -= Time.deltaTime;

		if (!isHoriShotting)
		{
			ArcShot();
		}

		if (timeRemaining <= 0)
		{
			if (pull.enabled == true)
			{
				pull.enabled = false;

				background.GetComponent<Animator>().Play("WindLeft");
				windSound.Play();
				push.enabled = true;
				timeRemaining = countdown;

			}
			else if (push.enabled == true)
			{
				push.enabled = false;

				background.GetComponent<Animator>().Play("WindRight");
				windSound.Play();
				pull.enabled = true;
				timeRemaining = countdown;
			}
		}

		if(horiTimer <= 0 && isHoriShotting != true)
		{
			isHoriShotting = true;
			horiTimer = horiCountdown;
			HorizontalShot();
		}

		if(icePuddleCountDown <= 0)
		{
			icePuddleCountDown = 9;
			IcePuddleShot();
		}
		
	}

	private void ArcShot()
	{
		if (currentCooldown <= 0)
		{
			currentCooldown = shootCooldown;
			BossProjectile bullet = Instantiate(bulletPrefab, fireOffset.position, projectile.rotation);

		}

		else
		{
			currentCooldown -= Time.deltaTime;
		}

		timeRemaining -= Time.deltaTime;
		icePuddleCountDown -= Time.deltaTime;
		spiral = UnityEngine.Random.Range(1, 21);

		if (spiral == 20)
		{
			shootCooldown = 0;
			runningCount = 10;
		}

		if (runningCount != 0)
		{
			runningCount--;
		}

		if (shootCooldown == 0 && runningCount == 0)
		{
			shootCooldown = 1;
		}
	}

	private void IcePuddleShot()
	{
		iceShotSound.Play();
		IcePuddleShot puddle = Instantiate(puddleProjectilePrefab, fireOffset.position, transform.rotation);
	}

	private void HorizontalShot()
	{
		value = UnityEngine.Random.Range(0, 2);

		if(value == 0)
		{
			StartCoroutine(FireBurst(JumpProjectilePrefab, horizontalOffset, 3));
		}

		else
		{
			StartCoroutine(FireBurst(CrouchProjectilePrefab, horizontalOffset2, 3));
		}
	}

	IEnumerator FireBurst(BlueBlenderHorizontalProjectile prefab, Transform offset, int burstSize)
	{
		for (int i = 0; i < burstSize; i++)
		{
			anim.Play("HorizShotAnimation");
			yield return new WaitForSeconds(3.1f);
			BlueBlenderHorizontalProjectile bullet = Instantiate(prefab, offset.position, transform.rotation);
			//BlueBlenderHorizontalProjectile bullet = Instantiate(prefab, Vector3(5.46, -1.59, 0), transform.rotation);
			yield return new WaitForSeconds(4.5f);
		}
		isHoriShotting = false;
		horiTimer = horiCountdown;
	}
	/*IEnumerator Wait()
	{
		timeRemaining = countdown;
		yield return new WaitForSeconds(5f);
		pull.enabled = false;
		push.enabled = false;
		value = Random.Range(0, 2);
		yield break;
	}
	*/
}
