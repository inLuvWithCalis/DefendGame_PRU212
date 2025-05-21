using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        AudioManager.Instance.PlaySFX("btn_Click");
        StartCoroutine(LoadGameScene());
    }
    private void Start()
    {
    
      AudioManager.Instance.PlayMusic("Theme");

    }

    private IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(0.3f);
     //  AudioManeger.Instance.PlayMusic("Theme");
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
