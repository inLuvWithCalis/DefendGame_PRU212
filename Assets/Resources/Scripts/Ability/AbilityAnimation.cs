using UnityEngine;

public class AbilityAnimation : MonoBehaviour
{
    private Animator anim;

    void OnEnable()
    {
        StartAnimation();
    }

    private void StartAnimation()
    {
        anim = gameObject.GetComponent<Animator>();
        string controllerName = anim.runtimeAnimatorController.name;
        anim.CrossFade(controllerName, 0, 0); //change state of animation
    }
    void StopAnimation() //call in last frame of animation
    {
        gameObject.SetActive(false);
    }


}
