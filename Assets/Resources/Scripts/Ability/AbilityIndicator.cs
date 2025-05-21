using UnityEditor.Playables;
using UnityEngine;

public class AbilityIndicator : MonoBehaviour
{
    private Ability ability;
    public GameObject abilityAnimation;
    public BoxCollider boxCollider;
    private int mapLayerNumber;
    private void OnEnable()
    {
        abilityAnimation.transform.localScale = gameObject.transform.localScale;
    }
    void Start()
    {
        int mapBitmaskValue = LayerMask.GetMask("Map"); ; // Replace this with the desired bitmask value
        mapLayerNumber = BitmaskToLayerNumber(mapBitmaskValue);
    }
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            // Raycast to detect the object click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10f, mapLayerNumber))//skip Map layer when hit
            {
                if (hit.collider.gameObject == gameObject)
                {
                    AudioManager.Instance.PlaySFX("ki_nang");
                    abilityAnimation.transform.position = gameObject.transform.position;
                    ability.Activate(this);
                    gameObject.SetActive(false); // Hide Indicator to show animation              
                    ActivateAnimation();
                }
            }
        }
        if (Input.GetMouseButtonDown(1)) // Check for right mouse button click
        {
            gameObject.SetActive(false);
        }
    }
	

	void ActivateAnimation()
    {
        abilityAnimation.SetActive(true);
    }

    public void SetAbility(Ability ability)
    {
        this.ability = ability;
    }
    int BitmaskToLayerNumber(int bitmaskValue)
    {
        for (int i = 0; i < 32; i++)
        {
            if ((bitmaskValue & (1 << i)) != 0)
            {
                return i;
            }
        }
        return -1; // Invalid layer
    }
}
