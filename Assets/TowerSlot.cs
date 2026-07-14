using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    private GameObject currentTower; // 记录这个位置是否已经放了塔

    void OnMouseDown()
    {
        Debug.Log("塔位被点击了！");
        // 已经有塔了，不能重复放置
        if (currentTower != null)
        {
            Debug.Log("这个位置已经有塔了");
            return;
        }

        // 通知UI管理器：玩家点击了这个塔位，打开选塔面板
        PlacementManager.Instance.OnSlotClicked(this);
    }

    public void PlaceTower(GameObject towerPrefab)
    {
        currentTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public bool IsOccupied()
    {
        return currentTower != null;
    }
}