using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
  

    // Update is called once per frame
    void Update()
    {
		
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}
}
