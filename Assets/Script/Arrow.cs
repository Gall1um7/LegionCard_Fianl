using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector2 StartPoint;             // 起点（UI坐标）
    private Vector2 EndingPoint;           // 终点（UI坐标）
    private RectTransform arrow;
    private RectTransform canvasRect;       // ⚠️ 在 Inspector 中拖入你的 Canvas 的 RectTransform

    private float ArrowLength;
    private float ArrowTheta;
    private Vector2 ArrowPosition;

    void Start()
    {
        canvasRect = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>();
        arrow = GetComponent<RectTransform>();
        
    }

    void Update()
    {
        // 将鼠标的屏幕坐标转换为Canvas局部坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Input.mousePosition,
            canvasRect.GetComponentInParent<Canvas>().worldCamera,
            out EndingPoint
        );

        // 计算箭头中点、长度、角度
        ArrowPosition = (EndingPoint + StartPoint) / 2;
        ArrowLength = Vector2.Distance(EndingPoint, StartPoint);
        ArrowTheta = Mathf.Atan2(EndingPoint.y - StartPoint.y, EndingPoint.x - StartPoint.x);

        // 赋值
        arrow.anchoredPosition = ArrowPosition;
        arrow.sizeDelta = new Vector2(ArrowLength, arrow.sizeDelta.y);
        arrow.localEulerAngles = new Vector3(0.0f, 0.0f, ArrowTheta * Mathf.Rad2Deg);
    }

    public void SetStartPoint(Vector2 _startPoint)
    {
        // 将屏幕坐标转为Canvas局部坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            _startPoint,
            canvasRect.GetComponentInParent<Canvas>().worldCamera,
            out StartPoint
        );
    }
}
