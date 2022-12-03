using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenText : MonoBehaviour
{
    public Text essenceText;
    public Text renownText;
    public Text Title;
    [SerializeField] public PlayerController script;
    [SerializeField] public BossController script2;
    [SerializeField] public KeptData data;
    public GameObject vicScreen;


    int essenceVal = 0;
    int renown = 0;
    float health = 0;
    float bossHealth = 0;
    float initBoss = 0;
    float barHealth = 0;
    float currenthealth = 0;
    float checker;
    bool run;


    // Start is called before the first frame update
    void Start()
    {
        initBoss = script2.getHealth(); //Sets current boss health
        run = true;

    }

    // Update is called once per frame
    void Update()
    {
        health = script.getHealth();
        bossHealth = script2.getHealth();
        checker = (initBoss - bossHealth)/initBoss; //value to check for percent of boss health depleted


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
        else if(checker == 1){
            essenceVal = 5;
            renown = 5;
        }

        else{
            essenceVal = 0;
            renown = 0;
        }

        essenceText.text = "Essence: " + essenceVal.ToString(); //Strings for End Screen
        renownText.text = "Renown: " + essenceVal.ToString();
        //Debug.Log(script.getHealth());
        //Debug.Log(script2.getHealth());
        if (bossHealth == 0){
            Title.text = "Victory";
        }

        if (health <= 0){
            Title.text = "Defeat";
        }
        if(health <= 0 || bossHealth <= 0){
            show();
            if (run == true){
                //Debug.Log(goldVal);
                data.addRenown(essenceVal);
                data.addEssence(essenceVal);
                run = false;
            }
        }
        else{
            hide();
        }

    }

    public void hide(){
        vicScreen.SetActive(false);
    }

    public void show(){
        vicScreen.SetActive(true);
    }
}
