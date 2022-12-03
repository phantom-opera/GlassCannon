using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    // DECLARE CONSTANTS HERE
    public float trasitionTime = 1f; //transition time for menus
    public Animator transition;
    public GameObject pauseFirstButton; //references the first button to be selected when the pause menu pops up
    public GameObject playerObject; //references the player object
    public GameObject bossObject;

    // This variable acts as a flag to check if the game is flagged
    //  true -> game is paused and false -> not paused
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Start(){
    pauseMenuUI.SetActive(false);
}
    // Update is called once per frame
    void Update()
    {
        // When the escape key is clicked
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // if game is already paused, then resume
            if (GameIsPaused)
            {
                Resume();
            }
            else //if game is not paused, then pause
            {
                Pause();
            }
        }
    }

// function that runs to resume the game
    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        playerObject.GetComponent<PlayerController>().enabled = true;
        bossObject.GetComponent<PullTowards>().enabled = true;
        bossObject.GetComponent<PushAway>().enabled = true;
    }

// function that runs to pause the game
    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        playerObject.GetComponent<PlayerController>().enabled = false;
        bossObject.GetComponent<PullTowards>().enabled = false;
        bossObject.GetComponent<PushAway>().enabled = false;

        // Keyboard selection
        //Clear the selected object
        EventSystem.current.SetSelectedGameObject(null);
        // set a new slected object
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

// function that loads the main Menu
    public void LoadMainMenu(){
        // This function invokes the main Menu
        // Reference the "MainMenu.cs" script within the "Main Menu" scene
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel("Main Menu"));
    }
    public void QuitGame(){
        // This function should quit the game
        Application.Quit();
    }

    public void Back(){
        // This function should the back functionality
        Resume(); //i just made it so this button unpauses the game for now
        Debug.Log("I am the back button ~> still in progress");
    }


    // Same as the IEnumerator in the "MainMenu.cs" script within the "Main Menu" scene
    IEnumerator LoadLevel(string levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(trasitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
