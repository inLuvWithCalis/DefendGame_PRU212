using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int totalLevels = 3; // Số lượng level trong game

    public void PlayGame()
    {
        AudioManager.Instance.PlaySFX("btn_Click");
        StartCoroutine(LoadGameScene());
    }

    // Tính năng New Game - Reset tất cả tiến trình
    public void NewGame()
    {
        AudioManager.Instance.PlaySFX("btn_Click");
        StartCoroutine(ResetGameProgress());
    }

    private IEnumerator ResetGameProgress()
    {
        // Reset tất cả tiến trình level
        ResetAllLevels();
        
        // Chờ một chút để đảm bảo PlayerPrefs đã được lưu
        yield return new WaitForSeconds(0.2f);
        
        // Load về level selection hoặc level đầu tiên
        SceneManager.LoadScene(1); // Hoặc scene Level Selection nếu có
    }

    private void ResetAllLevels()
    {
        // Reset tất cả level progress
        for (int i = 1; i <= totalLevels; i++)
        {
            string levelKey = "Lv" + i.ToString().PadLeft(2, '0');
            PlayerPrefs.SetInt(levelKey, 0); // 0 = locked, 1 = unlocked
        }
        
        // Mở khóa level đầu tiên
        PlayerPrefs.SetInt("Lv01", 1);
        
        // Reset các dữ liệu khác nếu có (điểm số, tiền...)
        // Ví dụ:
        // PlayerPrefs.SetInt("HighScore", 0);
        // PlayerPrefs.SetInt("TotalMoney", 0);
        
        // Lưu tất cả thay đổi
        PlayerPrefs.Save();
        
        Debug.LogError("Game progress has been reset! All levels are now locked except Level 1.");
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Theme");
    }

    private IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(0.3f);
        //AudioManeger.Instance.PlayMusic("Theme");
        // Sau khi chờ, tải Scene mới
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySFX("btn_Click");
        Application.Quit();
        Debug.Log("Exit game!");
    }
}
