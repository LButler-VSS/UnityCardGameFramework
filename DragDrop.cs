using UnityEngine;
using UnityEngine.Serialization;

public class DragDrop : MonoBehaviour
{
    private static GameObject _canvas;
    [FormerlySerializedAs("GameManager")] public GameObject gameManager;
    private bool m_IsDragging;
    private GameObject m_StartParent;
    private Vector2 m_StartPosition;
    private GameObject m_DropZone;
    private bool m_IsOverDropZone;


    public GameObject StartGrabCanvas() {
        _canvas = GameObject.Find("Main Canvas");
        return _canvas;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        m_IsOverDropZone = true;
        m_DropZone = collision.gameObject;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        //Debug.Log("It is colliding with " + collision.gameObject);
        m_IsOverDropZone = true;
        m_DropZone = collision.gameObject;    
    }

    private void OnCollisionExit2D() {
        m_IsOverDropZone = false;
        m_DropZone = null;
    }

    public void StartDrag()
    {
        m_IsDragging = true;
        var transform1 = transform;
        m_StartParent = transform1.parent.gameObject;
        m_StartPosition = transform1.position;
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(240, 344);
    }

    public void EndDrag()
    {
        m_IsDragging = false;
        if (m_IsOverDropZone){
            transform.SetParent(m_DropZone.transform, false);
        }
        else
        {
            transform.position = m_StartPosition;
            transform.SetParent(m_StartParent.transform, false);
        }
    }

    void Update()
    {
        if (m_IsDragging)
        {
            transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
            transform.SetParent(_canvas.transform, true);
        }
    }
}
