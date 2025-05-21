using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditRoll : MonoBehaviour
{
    public float scrollSpeed = 30f; // Điều chỉnh tốc độ cuộn tùy ý
    public float startPositionY = -500f; // Điểm bắt đầu của cuộn tín dụng
    public float endPositionY = 500f; // Điểm kết thúc của cuộn tín dụng

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, startPositionY);
    }

    private void Update()
    {
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (rectTransform.anchoredPosition.y >= endPositionY)
        {
            rectTransform.anchoredPosition = new Vector2(0, startPositionY);
        }
    }
}
