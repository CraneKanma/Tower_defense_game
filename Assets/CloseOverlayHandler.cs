using UnityEngine;
using UnityEngine.EventSystems;

public class CloseOverlayHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool skipNextClick = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (skipNextClick)
        {
            skipNextClick = false; // 消耗掉这一次跳过机会，之后恢复正常
            return;
        }

        UIManager.Instance.HideTowerSelectionPanel();
        PlacementManager.Instance.CancelSelection();
    }
}