using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int maxHP, hp;
    public int level, exp, levelupExp;
    public int skillAID, skillBID;

    public PlayerData(PlayerHealth health, PlayerExperience exp, PlayerSkills skills)
    {
        maxHP = health.maxHP;
        hp = health.hp;

        level = exp.level;
        this.exp = exp.exp;
        levelupExp = exp.levelupExp;

        skillAID = skills.skillAID;
        skillBID = skills.skillBID;
    }

    override public string ToString()
    {
        return maxHP + " " + hp + " " + level + " " + exp + " " + levelupExp;
    }
}
