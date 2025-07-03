using UnityEngine;

[CreateAssetMenu]
public class TowerItem : ScriptableObject
{
    [Header ("Config tower")]
    public string ID;
    public Tower towerPrefab;
    public Sprite Image;
    public Sprite CoinImg;
    public int cost;
    public int limitTower;
}
