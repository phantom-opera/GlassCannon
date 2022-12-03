using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VineArmController : MonoBehaviour
{
	[SerializeField] private Transform startPoint;

	public GameObject player;
	Vector2 direction;

	public float speed;

	public float rotationModifier;

	// Start is called before the first frame update
	void Start()
    {
		//direction = player.position - transform.position;
		transform.position = startPoint.position;
		Vector3 newPos = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z);
		transform.position = newPos;
		gameObject.transform.parent = startPoint.transform;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		/*RotateTowardsTarget();
		transform.rotation = LookAt2D(direction) * transform.rotation;
		*/

		if (player != null)
		{
			Vector3 vectorToTarget = player.transform.position - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
		}

	}
	private void RotateTowardsTarget()
	{
		var offset = 90f;
		//direction = player.position - transform.position;
		direction.Normalize();
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
	}

	static Quaternion LookAt2D(Vector2 forward) //Quaternion function that rotates the projectile to align with the forward x-axis.
	{
		return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
	}
}
