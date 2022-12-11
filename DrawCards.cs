using UnityEngine;
using UnityEngine.Serialization;

public class DrawCards : MonoBehaviour
{
    [FormerlySerializedAs("GameManager")] public GameManager gameManager;
    public int currentHandSize;
    [FormerlySerializedAs("PlayerArea")] public GameObject playerArea;
    public void OnClick()
    {
        currentHandSize = playerArea.transform.childCount;
        switch (currentHandSize)
        {
            //Debug.Log(currentHandSize);
            case < 5:
                gameManager.CmdDealCards(currentHandSize);
                break;
            case < 10:
                gameManager.CmdDealCards(4);
                break;
        }
    }

}
