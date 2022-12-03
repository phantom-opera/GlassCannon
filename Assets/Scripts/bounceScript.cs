using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceScript : MonoBehaviour
{

    //  reference to the game object colliding with the trampoline 
    public GameObject collider;
    // references the rigid body component of the collider 
    public Rigidbody2D rb; 
    // the launchforce of the trampoline, Can and Should be adjusted from within Unity
    public float launchForce;


    // the function that handles bouncing effect of the trampoline
    public void OnCollisionEnter2D(Collision2D other) {
        // get the rigid body of the collider 
        rb = other.gameObject.GetComponent<Rigidbody2D>();

        // if the collider had a rigid body attached then add the force
        if ( rb != null ){
            // add the force to the rigid body that collided with the trampoline/umbrella
            rb.AddForce(Vector2.up * launchForce, ForceMode2D.Impulse);
        }
    }
}
