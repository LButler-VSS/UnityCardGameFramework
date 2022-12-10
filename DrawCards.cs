using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public GameManager GameManager;
    public int currentHandSize;
    public GameObject PlayerArea;
    public void OnClick()
    {
        currentHandSize = PlayerArea.transform.childCount;
        //Debug.Log(currentHandSize);
        if (currentHandSize < 5){
            GameManager.CmdDealCards(currentHandSize);
        }
        else if (currentHandSize < 10) {
            GameManager.CmdDealCards(4);
        }
        else 
        {
            return;
        }
    }

}
