using UnityEngine;

public class StateController : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerHealth health;
    public PlayerStealth stealth;
    public PlayerExperience exp;
    public void Damage(int amount)
    {
        health.Damage(amount);
    }
    public void Die()
    {
        health.Die();
    }
    public void Heal(int amount)
    {
        health.Heal(amount);
    }
    public void MaxHeal()
    {
        health.MaxHeal();
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
    public void GainExp(int amount)
    {
        exp.GainExp(amount);
    }

    public void LoseExp(int amount)
    {
        exp.LoseExp(amount);
    }

    public void LevelUp()
    {
        exp.LevelUp();
    }
}
