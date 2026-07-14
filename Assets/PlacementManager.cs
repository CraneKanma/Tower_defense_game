using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    public TowerData[] availableTowers; // 在Inspector里拖入所有塔的数据

    private TowerSlot selectedSlot; // 记录当前点击的是哪个塔位

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void CancelSelection()
    {
        selectedSlot = null;
    }
    // 玩家点击塔位时调用（由TowerSlot触发）
    public void OnSlotClicked(TowerSlot slot)
    {
        selectedSlot = slot;
        UIManager.Instance.ShowTowerSelectionPanel(availableTowers);
        CloseOverlayHandler.skipNextClick = true; // 面板刚打开，跳过紧接着这一次的点击判定
    }

    // 玩家在UI面板上选择了某个塔时调用（由UI按钮触发）
    public void OnTowerSelected(TowerData towerData)
    {
        if (selectedSlot == null) return;

        // 检查金币是否足够
        bool success = EconomyManager.Instance.SpendGold(towerData.cost);

        if (success)
        {
            selectedSlot.PlaceTower(towerData.towerPrefab);
        }
        else
        {
            Debug.Log("金币不足，无法放置 " + towerData.towerName);
        }

        UIManager.Instance.HideTowerSelectionPanel();
        selectedSlot = null;
    }
}