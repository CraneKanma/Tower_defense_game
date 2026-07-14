using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI waveText;
    public Slider healthBar;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

   public void UpdateHealth(int currentHealth)
{
    healthText.text = "Health: " + currentHealth;
    
    if (healthBar != null)
    {
        healthBar.value = currentHealth; // 需要提前设置好Slider的Max Value等于满血值
    }
}

    public void UpdateGold(int currentGold)
    {
        goldText.text = "Gold: " + currentGold;
    }

    public void UpdateWave(int currentWave, int totalWaves)
    {
        waveText.text = "Wave: " + currentWave + " / " + totalWaves;
    }
}