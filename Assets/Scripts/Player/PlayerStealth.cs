using UnityEngine;

public class PlayerStealth : MonoBehaviour
{
    public bool isStealth;
    public Animator animator;
    void Start()
    {
        isStealth = false;
    }

    public void Stealth()
    {
        animator.SetBool("Stealthed", true);
        isStealth = true;
    }

    public void Unstealth()
    {
        animator.SetBool("Stealthed", false);
        isStealth = false;
    }
}
