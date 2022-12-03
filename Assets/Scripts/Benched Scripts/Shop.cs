using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{


    public Text buyAttackText;
    public Text buyHealthText;
    public Text buySpeedText;
    public Text renownText;
    public Text essenceText;

    public KeptData data;

    public void buyAttack(){
        if(data.renown >= data.attackPrice){
            data.increaseDamage();
            data.loseRenown(data.attackPrice);
            data.attackPrice += data.attackPrice;
        }
    }
    public void buyHealth(){
        if(data.renown >= data.healthPrice){
            data.increaseHealth();
            data.loseRenown(data.healthPrice);
            data.healthPrice += data.healthPrice;
        }
    }
    public void buySpeed(){
        if(data.renown >= data.speedPrice){
            data.increaseSpeed();
            data.loseRenown(data.speedPrice);
            data.speedPrice += data.speedPrice;
        }
    }

    public GameObject shopMenu;
    public void toggleOff(){
        shopMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void blueBattle(){
        Time.timeScale = 1f;
		//SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene("SampleScene");
    }

	public void pinkBattle()
	{
		Time.timeScale = 1f;
		//SceneManager.LoadScene("SampleScene");
		SceneManager.LoadScene("PinkVaseStage");
	}


	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        buyAttackText.text = "Buy Attack: "+data.attackPrice;
        buyHealthText.text = "Buy Health: "+data.healthPrice;
        buySpeedText.text = "Buy Speed: "+data.speedPrice;
        renownText.text = "Renown: " + data.renown;
        essenceText.text = "Essences: " + data.essences;

    }
}
