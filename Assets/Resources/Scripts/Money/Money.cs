using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    public static Money instance;
    [Header ("Clone button")]
    public Transform container;
    public MoneyCard cardPrefab;
    public TowerItem[] towers;

    [Header ("Coin infomation")]
    public TextMeshProUGUI textWarningTmp;
    public int currentMoney = 1000;
    public TextMeshProUGUI moneyTmp;
    /*public TextMeshProUGUI giveMoneyTmp;
    public TextMeshProUGUI spendMoneyTmp;*/



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadStore();

       
    }
    private void Update()
    {
        moneyTmp.text = currentMoney.ToString();
    }
    private void LoadStore()
    {
        for (int i = 0; i < towers.Length; i++)
        {
            MoneyCard card = Instantiate(cardPrefab,container);
            card.InitializeCard(towers[i]);
        }
    }

    public void GiveMoney(int amount)
    {
        currentMoney += amount;
     //   giveMoneyTmp.text = "+ "+ amount.ToString();
    }

    public bool SpendMoney(int amountToSpend)
    {
        bool spent = false;
        if (amountToSpend <= currentMoney)
        {
            spent = true;
            currentMoney -= amountToSpend;
     

            //     spendMoneyTmp.text = "- " + amountToSpend.ToString();
        }
        else
        {
			textWarningTmp.gameObject.SetActive(true);
			textWarningTmp.text = "Not enough gold";
			// StartCoroutine(ShowWarningAndDelay());
			Invoke("ShowWarningAndDelay", 2);
		}

        return spent;
    }

	/* private IEnumerator ShowWarningAndDelay()
	 {
		 yield return new WaitForSeconds(2.0f);
		 textWarningTmp.text = "";
	 }*/
	private void ShowWarningAndDelay()
	{
		textWarningTmp.gameObject.SetActive(false);
	}
}
