using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Declare Relevant Game Objects and Values
    public GameObject gManager;
    [FormerlySerializedAs("PlayerArea")] public GameObject playerArea;
    [FormerlySerializedAs("DropZone")] public GameObject dropZone;
    [FormerlySerializedAs("RuneAreaGiant")] public GameObject runeAreaGiant;
    [FormerlySerializedAs("RuneAreaGod")] public GameObject runeAreaGod;
    [FormerlySerializedAs("RuneAreaLife")] public GameObject runeAreaLife;
    [FormerlySerializedAs("RuneAreaMan")] public GameObject runeAreaMan;
    [FormerlySerializedAs("Card01")] public GameObject card01;
    [FormerlySerializedAs("Card02")] public GameObject card02;
    [FormerlySerializedAs("Card03")] public GameObject card03;
    [FormerlySerializedAs("Card04")] public GameObject card04;
    [FormerlySerializedAs("LifeRune")] public GameObject lifeRune;
    [FormerlySerializedAs("GiantRune")] public GameObject giantRune;
    [FormerlySerializedAs("GodRune")] public GameObject godRune;
    [FormerlySerializedAs("ManRune")] public GameObject manRune;
    [FormerlySerializedAs("Canvas")] public GameObject canvas;
    public GameObject mainCamera;
    public GameObject[] cardObjects;
    public GameObject[] cards;
    


    // Declare relevant starting values
    [FormerlySerializedAs("enemyHP")] public int enemyHp;
    [FormerlySerializedAs("playerHPCurrent")] public int playerHpCurrent;
    [FormerlySerializedAs("playerHPMax")] public int playerHpMax;
    public int manaPool;
    public int godRuneTotal;
    public int giantRuneTotal;
    public int lifeRuneTotal;
    public int manRuneTotal;

    // Declare relevant UI text objects
    [FormerlySerializedAs("EnemyHP")] public TextMeshProUGUI tmpEnemyHp;
    [FormerlySerializedAs("ManaPool")] public TextMeshProUGUI tmpManaPool;
    [FormerlySerializedAs("PlayerHP")] public TextMeshProUGUI playerHp;

    // Generate an array for card dealing

    void Start() {
        enemyHp = PlayerPrefs.GetInt("EnemyHp");
        playerHpCurrent = PlayerPrefs.GetInt("PlayerHpCur");
        playerHpMax = PlayerPrefs.GetInt("PlayerHpMax");
        manaPool = PlayerPrefs.GetInt("ManaPool");
        godRuneTotal = PlayerPrefs.GetInt("GodRune");
        giantRuneTotal = PlayerPrefs.GetInt("GiantRune");
        lifeRuneTotal = PlayerPrefs.GetInt("LifeRune");
        manRuneTotal = PlayerPrefs.GetInt("ManRune");

        canvas = gManager.GetComponent<DragDrop>().StartGrabCanvas();
        // This section is temporary to provide cards to pull from the deck and draw an initial 5 cards as the game starts.
        cards = new GameObject[cardObjects.Length]; //makes sure they match length
        for (int i = 0; i < cardObjects.Length; i++)
        {
            cards[i] = Instantiate(cardObjects[i]);
        }
        CmdDealCards(0);
        
        LoadRunes();
        // Set relevant values at the start of the game.
        playerHp.text = playerHpCurrent + "/" + playerHpMax + " HP";
        tmpEnemyHp.text = enemyHp.ToString();
        tmpManaPool.text = manaPool + " Mana";
    }    

    public void CmdDealCards(int i) {
        // Deals Cards to hand. Currently deals up to 5 cards at a time when a hand is empty.
        while (i < 5) {
            GameObject card = Instantiate(cards[Random.Range(0, cards.Length)], new Vector2(0, 0), Quaternion.identity);
            card.transform.SetParent(playerArea.transform, false);

            i++;
        }
    }

    private void UpdateSave()
    {
        PlayerPrefs.SetInt("EnemyHp", enemyHp);
        PlayerPrefs.SetInt("PlayerHpCur", playerHpCurrent);
        PlayerPrefs.SetInt("PlayerHpMax", playerHpMax);
        PlayerPrefs.SetInt("ManaPool", manaPool);
        PlayerPrefs.SetInt("GodRune", giantRuneTotal);
        PlayerPrefs.SetInt("GiantRune", godRuneTotal);
        PlayerPrefs.SetInt("LifeRune", lifeRuneTotal);
        PlayerPrefs.SetInt("ManRune", manRuneTotal);
    }

    private void Heal() {
        // Update text on screen to reflect new player health total
        if (playerHpCurrent <= playerHpMax) {
            playerHp.text = playerHpCurrent + "/" + playerHpMax + " HP";
        }
        else {
            playerHpCurrent = playerHpMax;
            playerHp.text = playerHpCurrent + "/" + playerHpMax + " HP";
        }
    }

    private void Mana() {
        // Update text on screen to reflect new player Mana Pool
        tmpManaPool.text = manaPool + " Mana";
    }

    private void LoadRunes()
    {
        for (int i = 0; i < giantRuneTotal; i++)
        {
            GameObject runeGi = Instantiate(this.giantRune, new Vector2(0, 0), Quaternion.identity);
            runeGi.transform.SetParent(runeAreaGiant.transform, false);
        }

        for (int i = 0; i < godRuneTotal; i++)
        {
            GameObject runeGo = Instantiate(this.godRune, new Vector2(0, 0), Quaternion.identity);
            runeGo.transform.SetParent(runeAreaGod.transform, false);
        }

        for (int i = 0; i < lifeRuneTotal; i++)
        {
            GameObject runeLi = Instantiate(this.lifeRune, new Vector2(0, 0), Quaternion.identity);
            runeLi.transform.SetParent(runeAreaLife.transform, false);
        }

        for (int i = 0; i < manRuneTotal; i++)
        {
            GameObject runeMa = Instantiate(this.manRune, new Vector2(0, 0), Quaternion.identity);
            runeMa.transform.SetParent(runeAreaMan.transform, false);
        }
    }
    
    private int PlayCard(string cardName) {
        // When a card has been detected as played in Update(), it will send the name of the card to this function, 
        // which will relay what the appropriate action for that card would be.
        // CHANGE TO BOOL IF NO MULTI ENEMY FIGHTS ARE IMPLEMENTED
        // OTHERWISE TARGETING SYSTEM AND LOGIC WILL NEED TO BE ADDED
        switch (cardName) {
            case "Card01(Clone)(Clone)":
                if (giantRuneTotal < 8){
                    GameObject runeGi = Instantiate(this.giantRune, new Vector2(0, 0), Quaternion.identity);
                    runeGi.transform.SetParent(runeAreaGiant.transform, false);
                    giantRuneTotal++;
                }
                enemyHp -= 5;
                tmpEnemyHp.text = enemyHp.ToString();
                return 1;
            case "Card02(Clone)(Clone)":
                if (godRuneTotal < 8) {
                    GameObject runeGo = Instantiate(this.godRune, new Vector2(0, 0), Quaternion.identity);
                    runeGo.transform.SetParent(runeAreaGod.transform, false);
                    godRuneTotal++;
                }
                manaPool += 3;
                Mana();
                return 2;
            case "Card03(Clone)(Clone)":
                if (lifeRuneTotal < 8) {
                    GameObject runeLi = Instantiate(this.lifeRune, new Vector2(0, 0), Quaternion.identity);
                    runeLi.transform.SetParent(runeAreaLife.transform, false);
                    lifeRuneTotal++;
                }
                playerHpCurrent += 5;
                Heal();
                return 2;
            case "Card04(Clone)(Clone)":
                if (manRuneTotal < 8) {
                    GameObject runeMa = Instantiate(this.manRune, new Vector2(0, 0), Quaternion.identity);
                    runeMa.transform.SetParent(runeAreaMan.transform, false);
                    manRuneTotal++;
                }
                enemyHp -= 2;
                playerHpCurrent += 2;
                manaPool += 1;
                tmpEnemyHp.text = enemyHp.ToString();
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
        if (dropZone.transform.childCount > 0) {
            var child = dropZone.transform.GetChild(0);
            var childName = child.name;
            //Debug.Log(childName);
            var validCard = PlayCard(childName);
            if (validCard != 0) {
                GameObject.Destroy(dropZone.transform.GetChild(0).gameObject);
            }
            else {
                child.transform.SetParent(playerArea.transform, true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateSave();
            SceneManager.LoadScene(0);
        }
    }
}
