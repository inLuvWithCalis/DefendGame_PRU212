using UnityEngine;

public class Boom : MonoBehaviour
{
    private Animator boomAnimator;
    private void OnEnable()
    {
        StartAnimation();
    }



    private void StartAnimation()
    {
        boomAnimator = gameObject.GetComponent<Animator>();
        string controllerName = boomAnimator.runtimeAnimatorController.name;
        boomAnimator.CrossFade("Boom", 0, 0); //change state of animation
    }
    void StopAnimation() //call in last frame of animation
    {
        gameObject.SetActive(false);
    }
}
