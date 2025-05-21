using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	
	public class Base : MonoBehaviour
	{
	    public float totalHealth = 100f;   // Sửa từ 'totalHeath' thành 'totalHealth'
	    public float currentHealth;        // Sửa từ 'currentHeath' thành 'currentHealth'
	
	    public Text currentHeathText;
	    public Slider healthSlider;
	
	    // Start is called before the first frame update
	    void Start()
	    {
	        currentHealth = totalHealth;
	        healthSlider.maxValue = totalHealth;
	        healthSlider.value = currentHealth;
	        currentHeathText.text = currentHealth.ToString() + "/" + totalHealth.ToString();
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	    }
	
	    public void takeDamage(float damage)
	    {
	        currentHealth -= damage;
	        if (currentHealth <= 20)
	        {
	            currentHeathText.color = Color.red;
	            AudioManager.Instance.PlaySFX("base");
	        }
	        if (currentHealth <= 0)
	        {
	            currentHealth = 0;
	            GameOver();  // Thay thế việc tắt GameObject bằng hàm GameOver()
	        }
	        healthSlider.value = currentHealth;
	        currentHeathText.text = currentHealth.ToString() + "/" + totalHealth.ToString();
	    }
	    
	    private void GameOver()
	    {
	        Debug.Log("Game Over");
	        // Thêm logic xử lý game over ở đây
	        gameObject.SetActive(false);
	        // Ví dụ: SceneManager.LoadScene("GameOver");
	    }
	}