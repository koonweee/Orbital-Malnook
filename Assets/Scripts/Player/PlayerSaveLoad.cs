using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveLoad : MonoBehaviour
{
    public PlayerHealth health;
    public PlayerExperience exp;
    public PlayerSkills skills;
    public SceneLoader loader;

    void Start()
    {
        //LoadPlayer();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(health, exp, skills);
        loader.LoadScene("Menu");
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Debug.Log("Loaded " + data);

        if (data == null) return;

        health.hpBarObj.GetComponent<Bar>().SetMax(data.maxHP);
        health.hp = data.hp;

        exp.level = data.level;
        exp.exp = data.exp;
        exp.levelupExp = data.levelupExp;

        skills.LockInSkill('A', data.skillAID);
        skills.LockInSkill('B', data.skillBID);
    }
}
