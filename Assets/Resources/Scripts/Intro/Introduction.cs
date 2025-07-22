using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Introduction : MonoBehaviour
{
	public static Introduction Instance;
	
	private Coroutine currentTextCoroutine;

	public TextMeshProUGUI instructionText;
	public Image itemImage;
	public Button nextButton;
	public Button backButton;


	public Sprite[] itemImages; // Mảng chứa hình ảnh vật phẩm
	public string[] itemTexts;     // Mảng chứa văn bản hướng dẫn
	public int step = 0;

	public bool isGuideActive = true;
	private void Awake()
	{
		Instance = this;
	}
	private void Start()
	{
		// Bắt đầu game logic (sinh ra quái vật, chạy game...)
		// Gán đối tượng RawImage
		//   FindAnyObjectByType<PauseMenu>().StopPlay();
		nextButton.onClick.AddListener(NextStep);
		backButton.onClick.AddListener(BackStep);
		//  backButton.gameObject.SetActive(false);

	}
	private void Update()
	{
		backButton.gameObject.SetActive(true);

		nextButton.gameObject.SetActive(true);
		if (step == 0)
		{
			backButton.gameObject.SetActive(false);

			nextButton.gameObject.SetActive(true);
		}
		else if (step == itemImages.Length - 1)
		{
			backButton.gameObject.SetActive(true);

			nextButton.gameObject.SetActive(false);
		}
	}
	public void Pause()
	{
		GameSpeed.instance.gameObject.SetActive(false);
		gameObject.SetActive(true);
		Time.timeScale = 0;
	}

	private void StartInstructionCoroutine()
	{

		StartCoroutine(ShowInstructions());

		UpdateItemImage(); // Cập nhật hình ảnh vật phẩm khi bắt đầu hướng dẫn

	}

	private void UpdateItemImage()
	{
		if (step > 0 && step < itemImages.Length)
		{
			itemImage.sprite = itemImages[step];
		}
	}

	private void UpdateItemImageAndText()
	{
		Time.timeScale = 0.001f;
		if (step >= 0 && step < itemImages.Length)
		{
			Debug.Log(itemImages[step]);
			//  Debug.Log(itemTexts[step]);

			itemImage.sprite = itemImages[step]; // Gán hình ảnh tương ứng với bước hướng dẫn hiện tại
			instructionText.text = itemTexts[step]; // Gán văn bản tương ứng với bước hướng dẫn hiện tại
			Debug.Log(itemImage);

		}

	}

	public void NextStep()
	{
		if (step < itemImages.Length - 1)
		{
			step++;
			UpdateItemImageAndText();
			
			if (this.currentTextCoroutine != null)
			{
				StopCoroutine(this.currentTextCoroutine); // Dừng Coroutine hiện tại nếu có
			}
			this.currentTextCoroutine = StartCoroutine(ShowInstructions()); // Bắt đầu Coroutine mới
			nextButton.gameObject.SetActive(true);
		}
	}
	public void Exit()
	{
		gameObject.SetActive(false);
		Time.timeScale = 1;
		GameSpeed.instance.gameObject.SetActive(true);
	}

	private void BackStep()
	{
		if (step > 0)
		{
			step--;
			UpdateItemImageAndText();
			if (this.currentTextCoroutine != null)
			{
				StopCoroutine(this.currentTextCoroutine);
			}
			this.currentTextCoroutine = StartCoroutine(this.ShowInstructions());
			backButton.gameObject.SetActive(true);
		}
	}
	
	private IEnumerator ShowTextLetterByLetter(string fullText)
	{
		instructionText.text = ""; // Xóa nội dung hiện tại
		int currentStep = step; // Lưu lại bước hiện tại để kiểm tra trạng thái

		for (int i = 0; i < fullText.Length; i++)
		{
			if (currentStep != step)
			{
				yield break;
			}
			instructionText.text += fullText[i]; // Thêm ký tự vào dòng văn bản
			yield return new WaitForSeconds(0.0000005f); // Chờ một khoảng thời gian trước khi hiển thị ký tự tiếp theo
		}
	}

	private IEnumerator ShowInstructions()
	{
		string fullText = itemTexts[step]; // Lấy toàn bộ văn bản từ bước hướng dẫn
		yield return ShowTextLetterByLetter(fullText); // Sử dụng Coroutine để hiển thị từng ký tự
		if (isGuideActive) // Kiểm tra trạng thái trước khi cập nhật hình ảnh vật phẩm
		{
			UpdateItemImage(); // Cập nhật hình ảnh vật phẩm sau khi hiển thị văn bản
		}
	}
}
