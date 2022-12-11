using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class CardZoom : MonoBehaviour
{
    [FormerlySerializedAs("Canvas")] public GameObject canvas;
    [FormerlySerializedAs("ZoomCard")] public GameObject zoomCard;

    private GameObject m_ZoomCard;
    private Sprite m_ZoomSprite;
    
    public void Awake() {
        
        canvas = GameObject.Find("Main Canvas");
        m_ZoomSprite = gameObject.GetComponent<Image>().sprite;
    }

    public void OnHoverEnter(){
    
        m_ZoomCard = Instantiate(zoomCard, new Vector2(Input.mousePosition.x, Input.mousePosition.y + 250), Quaternion.identity);
        m_ZoomCard.GetComponent<Image>().sprite = m_ZoomSprite;
        m_ZoomCard.transform.SetParent(canvas.transform, true);
        RectTransform rect = m_ZoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(240, 344); 
        
    }

    public void OnHoverExit(){
        // Also being called on drag, trying to avoid zoom objects sticking around.
        Destroy(m_ZoomCard);
    }
}