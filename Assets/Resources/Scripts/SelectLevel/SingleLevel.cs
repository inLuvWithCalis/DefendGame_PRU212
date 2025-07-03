using UnityEngine;
        using UnityEngine.SceneManagement;
        
        public class SingleLevel : MonoBehaviour
        {
            public void ClickBackButton()
            {
                SceneManager.LoadScene("00. Level Selection");
            }
        
            public void ClickWinButton()
            {
                string activeScene = SceneManager.GetActiveScene().name;
                string levelIndex = activeScene.Split(".")[0];
                PlayerPrefs.SetInt("Lv" + levelIndex, 1);
                ClickBackButton();
            }
        }