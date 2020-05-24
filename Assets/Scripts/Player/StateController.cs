using UnityEngine;

public class StateController : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerHealth health;
    public PlayerStealth stealth;
    public void Damage(int amount)
    {
        health.Damage(amount);
    }
    public void Die()
    {

    }
    public void Heal(int amount)
    {
        health.Heal(amount);
    }
    public void SpeedUp(float percent)
    {
        movement.SpeedUp(percent);
    }
    public void SlowDown(float percent)
    {
        movement.SlowDown(percent);
    }
    public void ResetSpeed()
    {
        movement.ResetSpeed();
    }
    public void Stealth()
    {
        stealth.Stealth();
    }
    public void Unstealth()
    {
        stealth.Unstealth();
    }
}
