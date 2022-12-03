using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIGameManager : MonoBehaviour
{
    // for rn start and update are always gonna be at the bottom

    // --------------------------------shop stuff ---------------------------------------------------------
        public Text buyAttackText;
        public Text buyHealthText;
        public Text buySpeedText;
        public Text renownTextShop;
        public Text essenceTextShop;
        //public GameObject healthIncrease;
        public GameObject dashButton;
        public GameObject doubleButton;
        public GameObject halfDamageButton;
        public GameObject doubAttButton;
        public GameObject darkScabbardTextBox;
        public Text darkScabbardText;


        public KeptData data;
        public PlayerController pc;


        public void mouseOff(){
            darkScabbardTextBox.SetActive(false);
        }
        public void mouseOverAttack(){
            darkScabbardTextBox.SetActive(true);
            darkScabbardText.text = "One extra point of attack costs "+ data.attackPrice+ " Renown."; 
        }
        public void mouseOverHealth(){
            darkScabbardTextBox.SetActive(true);
            darkScabbardText.text = "One extra point of health costs "+ data.healthPrice+ " Renown."; 
        }
        public void mouseOverSpeed(){
            darkScabbardTextBox.SetActive(true);
            darkScabbardText.text = "One extra point of speed costs "+ data.speedPrice+ " Renown."; 
        }
        public void mouseOverDash(){
            darkScabbardTextBox.SetActive(true);
            darkScabbardText.text = "The ability to dash costs "+ data.dashPrice+ " Renown."; 
        }
        public void mouseOverTime(){
            darkScabbardTextBox.SetActive(true);
            darkScabbardText.text = "Extra time costs "+ data.timePrice+ " Renown."; 
        }
        public void mouseOverDoubleJump(){
            darkScabbardTextBox.SetActive(true);
            darkScabbardText.text = "The ability to double jump costs "+ data.doublePrice+ " Renown."; 
        }
        public void mouseOverHealing(){
            darkScabbardTextBox.SetActive(true);
            darkScabbardText.text = "A Healing Essence costs "+ data.healingPrice+ " Essence and can be used in battle to heal yourself."; 
        }
        public void mouseOverHalfDamage(){
            darkScabbardTextBox.SetActive(true);
            darkScabbardText.text = "It costs "+ data.halfDamagePrice+ " Renown to make bosses do 1/2 damage."; 
        }
        public void mouseOverElementalPower(){
            darkScabbardTextBox.SetActive(true);
            darkScabbardText.text = "An Elemental Power costs "+ data.elementalPowerPrice+ " Renown and "+ data.elementalPowerPrice+ " Essence for extra damage to bosses."; 
        }
        public void mouseOverDoubleAttack(){
            darkScabbardTextBox.SetActive(true);
            darkScabbardText.text = "It costs "+ data.doubAttPrice+ " Renown to double your attack."; 
        }
        public void buyAttack(){
            if(data.renown >= data.attackPrice){
                data.increaseDamage();
                data.loseRenown(data.attackPrice);
                data.attackPrice += data.attackPrice;
            }
        }
        public void buyHealth(){
            float GChealth = pc.getHealth();
            if(data.renown >= data.healthPrice && ((GChealth + data.healthMod) < 18)){
                data.increaseHealth();
                data.loseRenown(data.healthPrice);
                //healthIncrease.SetActive(false);
                data.healthPrice += 10;
            }
        }
        public void buySpeed(){
            if(data.renown >= data.speedPrice){
                data.increaseSpeed();
                data.loseRenown(data.speedPrice);
                data.speedPrice += data.speedPrice;
            }
        }
        public void buyDash(){
            if (data.renown >= data.dashPrice){
                data.unlockDash();
                data.loseRenown(data.dashPrice);
                dashButton.SetActive(false);
            }
        }
        public void buyDouble(){
            if (data.renown >= data.doublePrice){
                data.unlockDouble();
                data.loseRenown(data.doublePrice);
                doubleButton.SetActive(false);
            }
        }
        public void buyTime(){
            if (data.renown >= data.timePrice){
                data.buyTime();
                data.loseRenown(data.timePrice);
                data.timePrice += data.timePrice;
            }
        }
        public void buyHalfDamage(){
            if (data.renown >= data.halfDamagePrice){
                data.halfDamageUnlock();
                data.loseRenown(data.halfDamagePrice);
                halfDamageButton.SetActive(false);
            }
        }
        public void buyDoubAtt(){
            if (data.renown >= data.doubAttPrice){
                data.buyDoubAtt();
                data.loseRenown(data.doubAttPrice);
                doubAttButton.SetActive(false);
            }
        }
        public void buyElementPower(){
            if(data.renown >= data.elementalPowerPrice && data.essences >= data.elementalPowerPrice && data.heldElementalPowers < 5){
                data.buyElementalPower();
                data.loseRenown(data.elementalPowerPrice);
                data.loseEssence(data.elementalPowerPrice);
            }
        }
        public void buyHealing(){
            if(data.essences >= data.healingPrice && data.healingEssence < 5){
                data.buyHealing();
                data.loseEssence(data.healingPrice);

            }
        }


        public GameObject shopMenu;
        public void toggleOff(){
            shopMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        public void beginBattle(){
            Time.timeScale = 1f;
            if(data.defeatedBossNumber == 0){
                SceneManager.LoadScene("SampleScene");
            }
            if(data.defeatedBossNumber == 1){
                SceneManager.LoadScene("OrangeLevel");
            }
        }

    // --------------------------------health bar stuff ---------------------------------------------------------
        public Slider slider;
        public Slider bossSlider;

        public void SetMaxHealth(float health){
            if(slider != null){
                slider.maxValue = health;
                slider.value = health;
            }
        }
        public void SetHealth(float health){
            if(slider != null){
                slider.value = health;
            }
        }
        public void SetBossMaxHealth(float health){
            if(bossSlider != null){
                bossSlider.maxValue = health;
                bossSlider.value = health;
            }
        }
        public void SetBossHealth(float health){
            if(bossSlider != null){
                bossSlider.value = health;
            }
        }
        


    // -------------------------------- Displays for Player Items --------------------------------------------
        public Text elemPowers;
        public Text healing;

    // --------------------------------door stuff ---------------------------------------------------------
        public GameObject equipMenu;
        public void OpenDoor(){
            if(equipMenu != null){
                equipMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    // --------------------------------main menu stuff ---------------------------------------------------------
        public Animator transition;
        public float trasitionTime = 1f;
        public GameObject controlsImage;

        public void Play_Button()
        {
            StartCoroutine(LoadLevel("Coliseum"));
        }
        bool controlsOpen = false;
        public void openControls(){
            if(!controlsOpen){
                controlsImage.SetActive(true);
                controlsOpen = true;
            } else{
                controlsImage.SetActive(false);
                controlsOpen = false;
            }
        }
        /*public void ExitButton()
        {
            Time.timeScale = 1f;
            Application.Quit();
            Debug.Log("Game Closed");
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

        }*/


        IEnumerator LoadLevel(string levelIndex){
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(trasitionTime);
            SceneManager.LoadScene(levelIndex);
        }
    // --------------------------------results screen stuff ---------------------------------------------------------
        public Text essenceTextResult;
        public Text renownTextResult;
        public Text Title;
        [SerializeField] public PlayerController script;
        [SerializeField] public BossController script2;
        public GameObject vicScreen;
        public GameObject gc;
        [SerializeField] public TimerScript timer;
        public GameObject victoryObj;
        public GameObject defeatObj;


        int essenceVal = 0;
        int goldVal = 0;
        int renown = 0;
        float health = 0;
        float bossHealth = 0;
        float initBoss = 0;
        float checker;
        bool run;

        public void hide(){
            vicScreen.SetActive(false);
        }

        public void show(){
            vicScreen.SetActive(true);
        }
        public void victory(){
            victoryObj.SetActive(true);
        }
        public void defeat(){
            defeatObj.SetActive(true);
        }
    // --------------------------------pause menu stuff ---------------------------------------------------------
        // DECLARE CONSTANTS HERE
        //public float trasitionTime = 1f; //transition time for menus
        //public Animator transition;
        public GameObject pauseFirstButton; //references the first button to be selected when the pause menu pops up
        public GameObject playerObject; //references the player object
        public GameObject bossObject;

        // This variable acts as a flag to check if the game is flagged
        //  true -> game is paused and false -> not paused
        public static bool GameIsPaused = false;

        public GameObject pauseMenuUI;

        // function that runs to resume the game
        public void Resume(){
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            playerObject.GetComponent<PlayerController>().enabled = true;
            if(bossObject.GetComponent<PullTowards>() != null){
                bossObject.GetComponent<PullTowards>().enabled = true;
            }
            if(bossObject.GetComponent<PushAway>() != null ){
                bossObject.GetComponent<PushAway>().enabled = true;
            }
        }

        // function that runs to pause the game
        public void Pause(){
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            playerObject.GetComponent<PlayerController>().enabled = false;
            if(bossObject.GetComponent<PullTowards>() != null){
                bossObject.GetComponent<PullTowards>().enabled = false;
            }
            if(bossObject.GetComponent<PushAway>() != null){
                bossObject.GetComponent<PushAway>().enabled = false;
            }

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


    // Start is called before the first frame update
    void Start()
    {
        // results screen
        if(script != null && script2 != null){
            initBoss = script2.getHealth(); //Sets current boss health
            run = true;
        }
        //pause menu
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //shop menu
        if(buyAttackText != null && buyHealthText != null && buySpeedText != null && renownTextShop != null && essenceTextShop.text != null){
            buyAttackText.text = "Buy Attack: "+data.attackPrice;
            buyHealthText.text = "Buy Health: "+data.healthPrice;
            buySpeedText.text = "Buy Speed: "+data.speedPrice ;
            renownTextShop.text = "Renown: " + data.renown;
            essenceTextShop.text = "Essences: " + data.essences;
        }
        if(data.dashBought == true){
            dashButton.SetActive(false);
        }
        if(data.doubleJumpBought == true){
            doubleButton.SetActive(false);
        }
        if(data.halfDamageBought == true){
            doubleButton.SetActive(false);
        }
        if(data.doubAttBought == true){
            halfDamageButton.SetActive(false);
        }
        /*if(data.healthMod == 2){
            healthIncrease.SetActive(false);
        }*/

        //SetHealth(health);
        //results screen
        if(script != null && script2 != null && victoryObj != null && defeatObj != null){
            health = script.getHealth();
            bossHealth = script2.getHealth();
            checker = (initBoss - bossHealth)/initBoss; //value to check for percent of boss health depleted
            //Debug.Log(checker);


            //Various if else statements for determining amount of renown and essence to show
            if(checker >= 0.25 && checker < 0.5){
                essenceVal = 1;
                renown = 1;
            }
            else if(checker >= 0.5 && checker < 0.75){
                essenceVal = 2;
                renown = 2;
            }
            else if(checker >= 0.75 && checker < 1){
                essenceVal = 3;
                renown = 3;
            }
            else if(checker >= 1){
                essenceVal = 5;
                renown = 5;
            }

            else{
                essenceVal = 0;
                renown = 0;
            }
            int elementals = data.heldElementalPowers;
            int healingtxt = data.healingEssence;
            elemPowers.text = "x"+elementals.ToString();
            healing.text = "x"+healingtxt.ToString();

            essenceTextResult.text = "Essence: " + essenceVal.ToString(); //Strings for End Screen
            renownTextResult.text = "Renown: " + renown.ToString();
            //Debug.Log(script.getHealth());
            //Debug.Log(script2.getHealth());
            
            if(health <= 0 || bossHealth <= 0 || timer.TimeLeft <= 0){
				show();

				// If either the player or boss is dead, begin calculating the renown and essences ~ David
				if (script == null && script2 == null) 
				{
					if(run == true)
					{
						Calculate(renown, essenceVal);
					}
				}
                if (run == true){
					data.addRenown(renown);
                    data.addEssence(essenceVal);
					run = false;
				}
				
            }
            else{
                hide();
            }
            if (bossHealth == 0){

                victory();
                data.bossDefeat();
            }

            if (health <= 0 || timer.TimeLeft == 0){
                defeat();
            }
        }

        //pause menu
            // When the escape key is clicked
        if(playerObject != null && pauseFirstButton != null &&  pauseMenuUI != null){
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
    }
	void Calculate(int renown, int essenceVal) // Added the calculation to a seperate function for debugging and organization purposes ~ David
	{
		if(run == true)
		{
			data.addRenown(renown);
			data.addEssence(essenceVal);
			gc.gameObject.SetActive(false);
			run = false;
		}
	}
}
