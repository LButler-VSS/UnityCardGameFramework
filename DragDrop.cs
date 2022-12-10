using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private static GameObject Canvas;
    public GameObject GameManager;
    private bool isDragging = false;
    private GameObject startParent;
    private Vector2 startPosition;
    private GameObject dropZone;
    private bool isOverDropZone;

    void Start()
    {
        
    }

    public GameObject StartGrabCanvas() {
        Canvas = GameObject.Find("Main Canvas");
        return Canvas;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        isOverDropZone = true;
        dropZone = collision.gameObject;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        //Debug.Log("It is colliding with " + collision.gameObject);
        isOverDropZone = true;
        dropZone = collision.gameObject;    
    }

    private void OnCollisionExit2D(Collision2D collision) {
        isOverDropZone = false;
        dropZone = null;
    }

    public void StartDrag()
    {
        isDragging = true;
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(240, 344);
    }

    public void EndDrag()
    {
        isDragging = false;
        if (isOverDropZone){
            transform.SetParent(dropZone.transform, false);
        }
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
            transform.SetParent(Canvas.transform, true);
        }
    }
}
