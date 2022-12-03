using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
	[SerializeField] private Material flashMaterial; //Set the unity material that lets the player's sprite flash.
	[SerializeField] private float duration;
	[SerializeField] private float blueDuration;
	[SerializeField] private Material blueFlashMaterial;

	private SpriteRenderer sprite;
	private Material originalMaterial;
	private Coroutine flashRoutine;

	// Start is called before the first frame update
	void Start()
    {
		sprite = GetComponent<SpriteRenderer>(); //Gets the player's current sprite
		originalMaterial = sprite.material;
	}

	public void Flash()
	{
		if (flashRoutine != null) //If currently flashing, stop the flash routine if this function is called again
		{
			StopCoroutine(flashRoutine);
		}

		// Start the Coroutine, and store the reference for it.
		flashRoutine = StartCoroutine(FlashRoutine());
	}

	public void MultiFlash()
	{
		if (flashRoutine != null) //If currently flashing, stop the flash routine if this function is called again
		{
			StopCoroutine(flashRoutine);
		}

		// Start the Coroutine, and store the reference for it.
		flashRoutine = StartCoroutine(BlueFlashRoutine());
	}

	private IEnumerator FlashRoutine()
	{
		// Swap to the flashMaterial.
		sprite.material = flashMaterial;

		// Pause the execution of this function for the duration.
		yield return new WaitForSeconds(duration);

		// After the pause, swap back to the original material.
		sprite.material = originalMaterial;

		// Set the routine to null, signaling that it's finished.
		flashRoutine = null;
	}

	private IEnumerator BlueFlashRoutine()
	{
		
		sprite.material = blueFlashMaterial;

		yield return new WaitForSeconds(blueDuration);

		sprite.material = originalMaterial;

		flashRoutine = null;
	}
}
