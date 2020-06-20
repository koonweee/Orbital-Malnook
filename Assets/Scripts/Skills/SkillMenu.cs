using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenu : MonoBehaviour
{
    public Button A, B, close;
    public Button[] skills;
    public PlayerSkills playerSkills;
    public AudioSource choiceAudio, skillAudio;
    private char selectedSkill;

    void Start()
    {
        selectedSkill = 'X';
        A.onClick.AddListener(() => SelectSkill('A'));
        B.onClick.AddListener(() => SelectSkill('B'));
        close.onClick.AddListener(Hide);

        for (int i = 0; i < skills.Length; ++i)
        {
            int ii = i;
            skills[i].onClick.AddListener(() => ChangeSkill(ii));
        }
    }

    void Update()
    {
        A.image.sprite = playerSkills.skillIcons[playerSkills.skillAID];
        B.image.sprite = playerSkills.skillIcons[playerSkills.skillBID];
    }

    void Hide()
    {
        CanvasGroup group = GetComponent<CanvasGroup>();
        group.alpha = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    public void Show()
    {
        CanvasGroup group = GetComponent<CanvasGroup>();
        group.alpha = 1f;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    void SelectSkill(char skill)
    {
        choiceAudio.Play();
        selectedSkill = skill;
    }

    void ChangeSkill(int skill)
    {
        if (selectedSkill == 'X') return;
        Debug.Log(selectedSkill + " " + skill);
        skillAudio.Play();
        playerSkills.LockInSkill(selectedSkill, skill);
        selectedSkill = 'X';
    }
}
