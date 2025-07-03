using UnityEngine;

[CreateAssetMenu]
public class SlowAbility : Ability
{
    public float duration;
    public float slowPercent;
    public override void Activate(AbilityIndicator abilityIndicator)
    {
        Vector2 center = abilityIndicator.boxCollider.bounds.center;
        // Size of the BoxCollider
        Vector2 size = abilityIndicator.boxCollider.bounds.size;
        // Rotation of the BoxCollider
        Quaternion rotation = abilityIndicator.boxCollider.transform.rotation;
        // Check for objects within the BoxCollider volume
        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, 0f);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag(("Enemy")))
            {
                //col.gameObject.GetComponent<EnemyController>().takeDamage(abilityDmg);
                col.gameObject.GetComponent<EnemyController>().ApplySlowEffect(duration, slowPercent);
            }
        }
    }
}

