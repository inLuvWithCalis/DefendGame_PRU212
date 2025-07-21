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
    }
    
    private void UpdateLevelStatus()
    {      
        int currentLevelNum = int.Parse(gameObject.name);
        int previousLevelNum = int.Parse(gameObject.name) - 1;
        Debug.LogError("Checking level: " + gameObject.name + ", Previous Level: " + previousLevelNum);

        if (currentLevelNum == 1)
        {
            this.unlocked = true;
        }
        else
        {
            string previousLevelKey = "Lv" + previousLevelNum.ToString().PadLeft(2, '0');
            this.unlocked = PlayerPrefs.GetInt(previousLevelKey) == 1;
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
