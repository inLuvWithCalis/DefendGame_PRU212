using UnityEngine;

public class Ability : ScriptableObject
{
    public float cooldownTime;

    public virtual void Activate(AbilityIndicator abilityIndicator) { }
}
