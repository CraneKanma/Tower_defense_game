using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerData", menuName = "TowerDefense/TowerData")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public int cost;
    public GameObject towerPrefab;
    public Sprite icon;
}