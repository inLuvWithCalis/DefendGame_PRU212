using UnityEngine;

public class TowerUpgradeController : MonoBehaviour
{
    private Tower theTower;
    public Bullet bullet;
    public UpgradeStage[] rangeUpgrades;
    public int currentRangeUpgrade;
    public bool hasRangeUpgrade = true;

    public UpgradeStage[] firerateUpgrades;
    public int currentFirerateUpgrade;
    public bool hasFirerateUpgrade = true;

    public UpgradeStage[] damageUpgrades;
    public int currentDamageUpgrade;
    public bool hasDamageUpgrade = true;

    void Start()
    {
        theTower = GetComponent<Tower>();
        theTower.dmgUpdate = bullet.bulletDamage;
    }

    public void upgradeRange()
    {
        theTower.circleCollider.radius = rangeUpgrades[currentRangeUpgrade].amount;
        currentRangeUpgrade++;
        if (currentRangeUpgrade >= rangeUpgrades.Length)
        {
            hasRangeUpgrade = false;
        }
    }

    public void upgradeFirerate()
    {
        theTower.firerate = firerateUpgrades[currentFirerateUpgrade].amount;
        currentFirerateUpgrade++;
        if (currentFirerateUpgrade >= firerateUpgrades.Length)
        {
            hasFirerateUpgrade = false;
        }
    }

    public void upgradeDamage()
    {
        Debug.Log(bullet != null);
        if (bullet !=null)
        {
            Debug.Log(damageUpgrades[currentDamageUpgrade].amount);
            theTower.dmgUpdate = damageUpgrades[currentDamageUpgrade++].amount;
            //bullet.bulletDamage = damageUpgrades[currentDamageUpgrade++].amount;
            if (currentDamageUpgrade >= damageUpgrades.Length)
            {
                hasDamageUpgrade = false;
            }
        }
    }
}

[System.Serializable]
public class UpgradeStage
{
    public float amount;
    public int cost;
}
