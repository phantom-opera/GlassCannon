using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSetActive : MonoBehaviour
{
    public GameObject equipMenu;
    public void OpenDoor(){
        equipMenu.SetActive(true);
        Time.timeScale = 0f;
    }

}
