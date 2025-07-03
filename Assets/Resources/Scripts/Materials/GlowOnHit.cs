using UnityEngine;
using System.Collections;

public class GlowOnHit : MonoBehaviour
{
    public Material glowMaterial;
    private Material originalMaterial;
    private SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        originalMaterial = rend.material;
    }

    public void TriggerGlow(float duration = 0.2f)
    {
        StartCoroutine(DoGlow(duration));
    }

    private IEnumerator DoGlow(float duration)
    {
        rend.material = glowMaterial;
        yield return new WaitForSeconds(duration);
        rend.material = originalMaterial;
    }
}