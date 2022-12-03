using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTriggerButton : MonoBehaviour
{
    public UIGameManager uiGM;
    //https://youtu.be/cLzG1HDcM4s 
    public bool isInRange;
    public UnityEvent interactAction;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) 
        {
            if (isInRange)
            {
                uiGM.OpenDoor();
            }
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            
        }
    }
}
