using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    public void BackToMenu()
    {
        // Load scene t�n l� "MenuScene"
        SceneManager.LoadScene("MenuScene");
    }
}
