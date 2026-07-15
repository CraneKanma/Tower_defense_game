using UnityEngine;
using UnityEngine.UI;
using TMPro; // 加这行
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject towerSelectionPanel; // 整个选塔UI面板
    public Transform buttonContainer;       // 面板里放按钮的父物体
    public GameObject towerButtonPrefab;    // 单个塔按钮的Prefab

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowTowerSelectionPanel(TowerData[] towers)
    {
        // 先清空之前生成的按钮，避免重复堆积
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // 动态生成每个塔的选择按钮
        foreach (TowerData tower in towers)
        {
            GameObject btnObj = Instantiate(towerButtonPrefab, buttonContainer);
            
            // 设置按钮图标和文字（假设按钮Prefab上有Image和Text子物体）
            btnObj.GetComponentInChildren<TextMeshProUGUI>().text = tower.towerName + "\n" + tower.cost + "Gold";
            
            Image iconImage = btnObj.transform.Find("Icon").GetComponent<Image>();
            if (iconImage != null && tower.icon != null)
            {
                iconImage.sprite = tower.icon;
            }

            
            TowerData capturedTower = tower; // 避免foreach闭包变量陷阱
            btnObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                PlacementManager.Instance.OnTowerSelected(capturedTower);
            });
        }

        towerSelectionPanel.SetActive(true);
    }

    public void HideTowerSelectionPanel()
    {
        towerSelectionPanel.SetActive(false);
    }
}
