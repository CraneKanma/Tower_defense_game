using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // 重新挑战当前关卡
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 新增：进入下一关
    public void NextLevel()
    {
        Time.timeScale = 1f;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        // 检查下一关是否存在（避免超出Build Settings里配置的场景数量）
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("已经是最后一关了！");
            // 之后可以在这里跳转到一个"全部通关"的专属画面
        }
    }
}