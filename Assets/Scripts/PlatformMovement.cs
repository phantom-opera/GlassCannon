using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

	[SerializeField] float speed = 0;
	[SerializeField] Transform startPoint;
	[SerializeField] Transform endPoint;

	[SerializeField] Transform target;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
		UpdateTargetDestination();

    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		collision.collider.transform.SetParent(transform);
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		collision.collider.transform.SetParent(null);
	}

	void UpdateTargetDestination()
	{
		if(transform.position == startPoint.position)
		{
			target = endPoint;
		}

		else if (transform.position == endPoint.position)
		{
			target = startPoint;
		}
	}
}
