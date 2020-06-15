using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level; // Tracks player level.
    public int exp; // Current exp.
    public int levelupExp; // Exp to get to level up.
    public LevelController levelCon;
    public PlayerHealth health;
    public Bar expBar;

    void Start()
    {
        level = 1;
        exp = 0;
        levelupExp = 50;

        levelCon.SetLevel(1);
        expBar.SetMax(levelupExp);
    }

    void Update()
    {
        // FOR TESTING.
        //if (Input.GetMouseButtonDown(1)) GainExp(10);
        //if (Input.GetMouseButtonDown(2)) LoseExp(10);

        expBar.UpdateVal(exp);
    }
    public void GainExp(int amount)
    {
        exp += amount;
        if (exp >= levelupExp)
        {
            LevelUp();
        }
    }

    public void LoseExp(int amount)
    {
        exp -= amount;
        if (exp < 0)
        {
            exp = 0;
        }
    }

    public void LevelUp()
    {
        ++level;
        exp = 0;
        levelupExp = 10 * level * (1 + level); // Next exp target.
        levelCon.SetLevel(level); // Update text.

        // Increase max health.
        health.IncreaseMaxHP((int) (Mathf.Sqrt(level) * 50));
        health.MaxHeal(); // Heal to full.
        expBar.SetMax(levelupExp); // Increase bar's max exp.

        // Level up effect.
        levelCon.LevelExplode();
    }
}
