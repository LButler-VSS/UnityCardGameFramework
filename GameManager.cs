using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : ScriptableObject
{
    public string test = "Hello world";
}

public class GameManager : MonoBehaviour
{
    // Declare Relevant Game Objects
    public GameObject gManager;
    public GameObject PlayerArea;
    public GameObject DropZone;
    public GameObject RuneAreaGiant;
    public GameObject RuneAreaGod;
    public GameObject RuneAreaLife;
    public GameObject RuneAreaMan;
    public GameObject Card01;
    public GameObject Card02;
    public GameObject Card03;
    public GameObject Card04;
    public GameObject LifeRune;
    public GameObject GiantRune;
    public GameObject GodRune;
    public GameObject ManRune;
    public GameObject Canvas;
    public GameObject[] cardObjects;
    public GameObject[] cards;


    // Declare relevant starting values
    public int enemyHP = 1000;
    public int playerHPCurrent = 75;
    public int playerHPMax = 75;
    public int manaPool = 0;
    public int godRuneTotal = 0;
    public int giantRuneTotal = 0;
    public int lifeRuneTotal = 0;
    public int manRuneTotal = 0;

    // Declare relevant UI text objects
    public TextMeshProUGUI EnemyHP;
    public TextMeshProUGUI ManaPool;
    public TextMeshProUGUI PlayerHP;

    // Generate an array for card dealing

    void Start() {
        Canvas = gManager.GetComponent<DragDrop>().StartGrabCanvas();
        // This section is temporary to provide cards to pull from the deck and draw an initial 5 cards as the game starts.
        cards = new GameObject[cardObjects.Length]; //makes sure they match length
        for (int i = 0; i < cardObjects.Length; i++) {
             cards[i] = Instantiate(cardObjects[i]) as GameObject;
        }
        CmdDealCards(0);

        // Set relevant values at the start of the game.
        PlayerHP.text = playerHPCurrent + "/" + playerHPMax + " HP";
        EnemyHP.text = "1000";
        ManaPool.text = "0 Mana";
    }    

    public void CmdDealCards(int i) {
        // Deals Cards to hand. Currently deals up to 5 cards at a time when a hand is empty.
        while (i < 5) {
            GameObject card = Instantiate(cards[Random.Range(0, cards.Length)], new Vector2(0, 0), Quaternion.identity);
            card.transform.SetParent(PlayerArea.transform, false);

            i++;
        }
    }

    public void Heal() {
        // Update text on screen to reflect new player health total
        if (playerHPCurrent <= playerHPMax) {
            PlayerHP.text = playerHPCurrent.ToString() + "/" + playerHPMax.ToString() + " HP";
            }
        else {
            playerHPCurrent = playerHPMax;
            PlayerHP.text = playerHPCurrent.ToString() + "/" + playerHPMax.ToString() + " HP";
        }
    }
    
    public void Mana() {
        // Update text on screen to reflect new player Mana Pool
        ManaPool.text = manaPool.ToString() + " Mana";
    }

    public int PlayCard(string cardName) {
        // When a card has been detected as played in Update(), it will send the name of the card to this function, 
        // which will relay what the appropiate action for that card would be.
        // CHANGE TO BOOL IF NO MULTI ENEMY FIGHTS ARE IMPLEMENTED
        // OTHERWISE TARGETTING SYSTEM AND LOGIC WILL NEED TO BE ADDED
        switch (cardName) {
            case "Card01(Clone)(Clone)":
                if (RuneAreaGiant.transform.childCount < 8){
                    GameObject giantRune = Instantiate(GiantRune, new Vector2(0, 0), Quaternion.identity);
                    giantRune.transform.SetParent(RuneAreaGiant.transform, false);
                }
                enemyHP -= 5;
                EnemyHP.text = enemyHP.ToString();
                return 1;
            case "Card02(Clone)(Clone)":
                GameObject godRune = Instantiate(GodRune, new Vector2(0, 0), Quaternion.identity);
                if (RuneAreaGod.transform.childCount < 8) {
                    godRune.transform.SetParent(RuneAreaGod.transform, false);
                }
                manaPool += 3;
                Mana();
                return 2;
            case "Card03(Clone)(Clone)":
                GameObject lifeRune = Instantiate(LifeRune, new Vector2(0, 0), Quaternion.identity);
                if (RuneAreaLife.transform.childCount < 8) {
                    lifeRune.transform.SetParent(RuneAreaLife.transform, false);
                }
                playerHPCurrent += 5;
                Heal();
                return 2;
            case "Card04(Clone)(Clone)":
                GameObject manRune = Instantiate(ManRune, new Vector2(0, 0), Quaternion.identity);
                if (RuneAreaMan.transform.childCount < 8) {
                    manRune.transform.SetParent(RuneAreaMan.transform, false);
                }
                enemyHP -= 2;
                playerHPCurrent += 2;
                manaPool += 1;
                EnemyHP.text = enemyHP.ToString();
                Mana();
                Heal();
                return 1;
            default:
                return 0;
        }
    } 


    void Update() {
        // Still thinking of a way to perform this outside of Update, but for now this is working.
        // Checks is the dropzone has a child, determines the card, implements the effect and destroys the card.
        if (DropZone.transform.childCount > 0) {
            var child = DropZone.transform.GetChild(0);
            var childName = child.name;
            //Debug.Log(childName);
            var ValidCard = PlayCard(childName);
            if (ValidCard != 0) {
                GameObject.Destroy(DropZone.transform.GetChild(0).gameObject);
            }
            else {
                child.transform.SetParent(PlayerArea.transform, true);
            }
        }
    }
}
