using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOutsideToClose : MonoBehaviour
{
    private RectTransform panelRect;
    private bool justOpened = false;

    void Awake()
    {
        panelRect = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        justOpened = true; // 标记这一帧刚打开，跳过这一帧的检测
    }

    void Update()
    {
        if (justOpened)
        {
            justOpened = false; // 跳过打开的这一帧，下一帧才开始检测
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // 判断这次点击是否落在这个面板的范围之外
            if (!RectTransformUtility.RectangleContainsScreenPoint(panelRect, Input.mousePosition, null))
            {
                UIManager.Instance.HideTowerSelectionPanel();
                PlacementManager.Instance.CancelSelection();
            }
        }
    }
}