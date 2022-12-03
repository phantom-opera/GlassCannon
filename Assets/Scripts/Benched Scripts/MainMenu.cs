using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
   public Animator transition;
    public float trasitionTime = 1f;
    
    public void ExitButton()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void Play_Button()
    {
        StartCoroutine(LoadLevel("Coliseum"));
    }

    public void SettingsButton()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel("Settings Menu"));
    }

    public void MainMenuButton()
    {
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel("Main Menu"));
        
    }


    IEnumerator LoadLevel(string levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(trasitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
