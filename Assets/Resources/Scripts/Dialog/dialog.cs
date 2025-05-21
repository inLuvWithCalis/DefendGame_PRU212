using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class dialog : MonoBehaviour
{


	public TextMeshProUGUI textComponent;
	public string[] lines;
	public float textSpeed;
	public GameObject gameUI;
	public GameObject panelUI;
	private int index;
	// Start is called before the first frame update

	void Start()
	{
		Time.timeScale = 0f;
	    GameSpeed.instance.gameObject.SetActive(false);
		textComponent.text = string.Empty;
		StartDialogue();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (textComponent.text == lines[index])
			{
				NextLine();
			}
			else
			{
				
				StopAllCoroutines();

				textComponent.text = lines[index];
			}
		}

	}

	void StartDialogue()
	{
		index = 0;
		StartCoroutine(TypeLine());
	}
	IEnumerator TypeLine()
	{
		Time.timeScale = 0.001f;
		foreach (char c in lines[index].ToCharArray())
		{
			textComponent.text += c;
			yield return new WaitForSeconds(textSpeed);
		}
	}

	void NextLine()
	{
		if (index < lines.Length - 1)
		{
		
			index++;
			textComponent.text = string.Empty;
			StartCoroutine(TypeLine());
		}
		else
		{
			Time.timeScale = 1f;
			GameSpeed.instance.gameObject.SetActive(true);
			panelUI.SetActive(false);
		}
	}


}
