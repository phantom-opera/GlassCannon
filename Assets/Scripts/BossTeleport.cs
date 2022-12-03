using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossTeleport : MonoBehaviour
{

	//[SerializeField] private Transform boss;
	//[SerializeField] private GameObject vineArm;
	//[SerializeField] private GameObject vineArm2;
	//[SerializeField] private GameObject vineArm3;
	[SerializeField] private List<Transform> spawnTarget;
	[SerializeField] float countdown = 10;
	[SerializeField] private float counter = 10;
	private Transform spawnTargetPos;
	Animator anim;

	Vector3 originalScale;

	private void Start()
	{
		anim = GetComponent<Animator>();
		originalScale = gameObject.transform.localScale;
	}

	// Update is called once per frame
	void Update()
    {
		countdown -= Time.deltaTime;

		if(countdown <= 1)
		{
			if (gameObject.tag == "Enemy")
			{
				anim.Play("PinkRetreat");
			}
		}

		if (countdown <= 0)
		{
			spawnTargetPos = GetRandomTarget();

			if (spawnTargetPos.childCount > 0)
			{
				spawnTargetPos = GetRandomTarget();
			}

			else
			{
				transform.parent = null;

				if (gameObject.tag == "Enemy")
				{
					anim.Play("PinkEmerge");
					transform.position = spawnTargetPos.position;
					Vector3 newPos = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z);
					transform.position = newPos;
				}

				else
				{
					if (gameObject.tag == "Vine")
					{
						anim.Play("VineStrike");
						transform.position = spawnTargetPos.position;
						Vector3 newPos = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z);
						transform.position = newPos;
					}
					
				}

				gameObject.transform.SetParent(spawnTargetPos, true);
				countdown = counter;

			}
		}
	}

	public Transform GetRandomTarget() // Gets a random target from a list and returns that target to the dumb function up above, honestly I am seriously considering retirement.
	{
		int randomIndex = Random.Range(0, spawnTarget.Count);
		return spawnTarget[randomIndex];
	}
}
