using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCard : MonoBehaviour
{
    [Header("References")]
    public Image imgTower;
    public TextMeshProUGUI costTMP;
    public Image coinImg;

    public TowerItem towerItem { get; private set; }
    public void InitializeCard(TowerItem item)
    {
        towerItem = item;
        imgTower.sprite = item.Image;
        costTMP.text = item.cost.ToString();
        coinImg.sprite = item.CoinImg;
    }

    public void SelectCardButton()
    {
        if (towerItem)
        {
            TowerManager.Instance.SelectedTower(towerItem);
            Debug.Log("meme");
        }
    }

}
