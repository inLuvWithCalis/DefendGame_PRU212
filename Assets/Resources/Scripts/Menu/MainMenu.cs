using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        AudioManager.Instance.PlaySFX("btn_Click");
        StartCoroutine(LoadGameScene());
    }

    public void NewGame()
    {
        AudioManager.Instance.PlaySFX("btn_Click");
        StartCoroutine(ResetGameProgress());
    }

    private IEnumerator ResetGameProgress()
    {
        yield return new WaitForSeconds(0.2f);
        // Xóa tất cả PlayerPrefs liên quan đến tiến trình game
        PlayerPrefs.DeleteAll();
        Debug.Log("All game progress has been reset!");
        
        // Set up PlayerPrefs to levels.
        PlayerPrefs.SetInt("Lv01", 0);
        PlayerPrefs.SetInt("Lv02", 0);
        PlayerPrefs.SetInt("Lv03", 0);
        
        // Load về level selection.
        SceneManager.LoadScene(1);
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
