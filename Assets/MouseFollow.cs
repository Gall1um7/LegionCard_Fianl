using UnityEngine;
using UnityEngine.UI;

public class MouseFollow : MonoBehaviour
{
    public RectTransform canvas;
    public Camera cam;
    RectTransform rect;
    Vector2 mousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, Camera.main, out mousePos);
            rect.anchoredPosition= mousePos;

    }
}
