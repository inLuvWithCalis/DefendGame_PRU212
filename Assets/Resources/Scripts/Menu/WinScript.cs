using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public  class WinScript : MonoBehaviour
{
    public GameObject loseScene;
    public GameObject winScene;
    public static WinScript instance;
   // public String nextLevel;
    private void Awake()
    {
        instance = this;
    }
    //public void Home()
    //{
    //    SceneManager.LoadScene(6);
    //    Time.timeScale = 1;

    //}
    public void Resume()
    {
       // pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }
    public void NextScene()
    {
		Scene currentScene = SceneManager.GetActiveScene();

		// Lấy tên của scene
		string sceneName = currentScene.name;
		Debug.Log("Tên scene hiện tại là: " + sceneName);
		// 01. Level 01
		string numberString = sceneName.Split('.')[0];
		int number = int.Parse(numberString);
		int numbernext = number + 1;
		Debug.Log(numbernext);
		string stringnext = numbernext.ToString();
		string levelIndex = stringnext.PadLeft(2, '0');
		Debug.Log(levelIndex);


        string levelSceneName = levelIndex + ". Level " + levelIndex ;


		SceneManager.LoadScene(levelSceneName);


    }
    public void BackToHome()
    {
        Debug.Log("Homeeee: ");

        SceneManager.LoadScene("MenuScene");
    }
    public void Resart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }
    public void SelectLevel()
    {
        Debug.Log("Homeeee: ");

        SceneManager.LoadScene("00. Level Selection");
    }

}
