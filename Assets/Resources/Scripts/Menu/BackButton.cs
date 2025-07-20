using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    public void BackToMenu()
    {
        // Load scene tên là "MenuScene"
        SceneManager.LoadScene("MenuScene");
    }
}
