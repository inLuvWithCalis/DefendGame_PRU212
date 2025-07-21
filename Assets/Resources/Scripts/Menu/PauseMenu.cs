using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    public void Pause()
    {
		GameSpeed.instance.gameObject.SetActive(false);
		Debug.Log("Hien thi");
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Home()
    {
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1;

    }
    public void Resume()
    {
		GameSpeed.instance.gameObject.SetActive(true);
		pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }
    public void Settings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    
    public void BackToPause()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    
    public void Resart()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
