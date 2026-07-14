using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public GameObject baseObject; 
    public static GameManager Instance;

    public int playerHealth = 10;
    private bool isGameOver = false; // 防止游戏结束后逻辑重复触发

    void Awake()
    {
        HUDManager.Instance.UpdateHealth(playerHealth); 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerTakeDamage(int amount)
    {
        if (isGameOver) return; // 游戏已经结束就不再处理

        playerHealth -= amount;
        HUDManager.Instance.UpdateHealth(playerHealth);
        StartCoroutine(FlashBase()); // 加这行，触发闪烁效果
        Debug.Log("玩家扣血，剩余血量：" + playerHealth);

        if (playerHealth <= 0)
        {
            OnGameLose();
        }
    }
    IEnumerator FlashBase()
    {
        if (baseObject == null) yield break;

        SpriteRenderer sr = baseObject.GetComponent<SpriteRenderer>();
        Color original = sr.color;
        sr.color = Color.red; // 瞬间变红，表示受击
        yield return new WaitForSeconds(0.15f);
        sr.color = original; // 恢复原色
    }
    // 新增：游戏胜利
    public void OnGameWin()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("游戏胜利！");
        // 之后可以在这里加：显示胜利界面UI、暂停游戏（Time.timeScale = 0）等
        FindObjectOfType<GameOverUI>().ShowWinPanel();
    }

    // 新增：游戏失败
    public void OnGameLose()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("游戏失败！");
        // 之后可以在这里加：显示失败界面UI、暂停游戏等
        FindObjectOfType<GameOverUI>().ShowLosePanel();
    }
}