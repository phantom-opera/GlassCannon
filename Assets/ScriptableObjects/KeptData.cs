using UnityEngine;
[CreateAssetMenu(fileName = "KeptData", menuName = "KeptData")]
public class KeptData : ScriptableObject
{
    [SerializeField] public int renown = 0;
    [SerializeField] public int essences = 0;
    [SerializeField] public float healthMod = 0;
	[SerializeField] public int speedMod = 0;
    [SerializeField] public int attackMod = 0;
    [SerializeField] public bool dashBought = false;
    [SerializeField] public bool doubleJumpBought = false;
    [SerializeField] public bool halfDamageBought = false;
    [SerializeField] public bool doubAttBought = false;
    [SerializeField] public bool timeBought = false;
    [SerializeField] public int heldElementalPowers = 0;
    [SerializeField] public int healingEssence = 0;
    [SerializeField] public int defeatedBossNumber = 0;

    public void resetValues(){
        renown = 0;
        essences = 0;
        healthMod = 0;
        speedMod = 0;
        attackMod = 0;
        attackPrice = 10;
        speedPrice = 10;
        healthPrice = 10;
        timePrice = 10;
        doubleJumpBought = false;
        dashBought = false;
        halfDamageBought = false;
        doubAttBought = false;
        timeBought = false;
        heldElementalPowers = 0;
        healingEssence = 0;
        defeatedBossNumber = 0;
    }

    public void addRenown(int newRenown){
        renown += newRenown;
    }
    public void loseRenown(int lostRenown){
        renown -= lostRenown;
    }

    public void addEssence(int newEssence){
        essences += newEssence;
    }
    public void loseEssence(int lostEssence){
        essences -= lostEssence;
    }

    public void increaseHealth(){
		healthMod += 2;
	}
	public void increaseSpeed(){
		speedMod ++;
	}
    public void increaseDamage(){
		attackMod++;
	}
    public void unlockDash(){
        dashBought = true;
    }
    public void unlockDouble(){
        doubleJumpBought = true;
    }
    public void halfDamageUnlock(){
        halfDamageBought = true;
    }
    public void buyDoubAtt(){
        doubAttBought = true;
    }
    public void buyTime(){
        timeBought = true;
    }

    public void buyElementalPower(){
        heldElementalPowers += 1;
        Debug.Log(heldElementalPowers);
    }
    public void buyHealing(){
        healingEssence += 1;
        Debug.Log(healingEssence);
    }

    public void usingHealing(){
        healingEssence -= 1;
    }
    public void usingElements(){
        heldElementalPowers -= 1;
    }
    public void bossDefeat(){
        defeatedBossNumber++;
    }


    [SerializeField] public int attackPrice = 10;
    [SerializeField] public int healthPrice = 10;
    [SerializeField] public int speedPrice = 10;
    [SerializeField] public int dashPrice = 10;
    [SerializeField] public int doublePrice = 10;
    [SerializeField] public int doubAttPrice = 50;
    [SerializeField] public int halfDamagePrice = 10;
    [SerializeField] public int timePrice = 10;
    [SerializeField] public int elementalPowerPrice = 1;
    [SerializeField] public int healingPrice = 3;

}
