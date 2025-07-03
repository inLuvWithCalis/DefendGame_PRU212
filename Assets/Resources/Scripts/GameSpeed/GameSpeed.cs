using TMPro;
using UnityEngine;
	
	public class GameSpeed : MonoBehaviour
	{
	    public int gameSpeed = 1;
	    public TextMeshProUGUI gameSpeedText;
	    public static GameSpeed instance;
	    
	    private void Awake()
	    {
	        // Cải thiện mẫu singleton
	        if (instance == null)
	        {
	            instance = this;
	        }
	        else
	        {
	            Destroy(gameObject);
	            return;
	        }
	    }
	    
	    // Update is called once per frame
	    void Update()
	    {
	        // Set the UI Text
	        gameSpeedText.text = "X" + gameSpeed.ToString();
	        // Update speed of our game
	        Time.timeScale = gameSpeed;
	    }
	    
	    public void ChangeSpeed()
	    {
	        // Sửa lại giới hạn tốc độ tối đa là 4 thay vì 6
	        if (gameSpeed < 4)
	        {
	            gameSpeed++;
	        }
	        else
	        {
	            gameSpeed = 1;
	        }
	    }
	}