using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked;
    public Image lockImage;

    private void Start()
    {
        AudioManager.Instance.PlayMusic("nhac_chon_map");
        UpdateLevelStatus();
        UpdateLevelImage();
        //PlayerPrefs.DeleteAll();
    }

    private void UpdateLevelStatus()
    {      
        int previousLevelNum = int.Parse(gameObject.name) - 1;
        if (PlayerPrefs.GetInt("Lv" + previousLevelNum.ToString().PadLeft(2, '0')) == 1)
        {
            unlocked = true;
        }
    }

    private void UpdateLevelImage()
    {
        if (unlocked)
        {
            Debug.Log("update " + gameObject.name);
            lockImage.gameObject.SetActive(false);
        }
        else
        {
            lockImage.gameObject.SetActive(true);
        }
        
        
    }

    public void PressSelection()
    {
        if(unlocked)
        {

            string levelIndex = gameObject.name.PadLeft(2, '0');
            string levelSceneName = levelIndex + ". Level " + levelIndex;
			AudioManager.Instance.PlaySFX(levelSceneName);

			SceneManager.LoadScene(levelSceneName);
        }
    }
}
