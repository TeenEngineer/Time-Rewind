using UnityEngine;

public class Friend : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Fall()
    {
        Debug.Log("Fall");
        anim.SetBool("fall", true);
    }

    public void GoAway()
    {
        anim.SetBool("goaway", true);
    }

    public void walkAway()
    {
        anim.SetBool("walkaway", true);
    }

    void Update()
    {
        
    }
}
