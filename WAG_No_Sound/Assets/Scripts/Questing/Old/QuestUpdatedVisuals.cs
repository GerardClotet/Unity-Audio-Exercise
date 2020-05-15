////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuestSystem;

public class QuestUpdatedVisuals : MonoBehaviour
{
    public float ShowDuration = 6f;
    private AudioSource audio_source;
    public AudioClip QuestRollOpenSFX;
    public AudioClip QuestRollCloseSFX;

    #region private variables
    [SerializeField]
    private QuestGiver questGiver;

    [SerializeField]
    private Animator questUpdatedAnimator;

    [SerializeField]
    private LocalisedText questTitleText;

    [SerializeField]
    private LocalisedText questDescriptionText; 

    private Quest currentQuest;
    private bool isShowing = false;

    //Cached animator hashes
    private readonly int showTrigger = Animator.StringToHash("Show");
    private readonly int hideTrigger = Animator.StringToHash("Hide");
    #endregion

    private void Start()
    {
        audio_source = GameObject.Find("Menus").GetComponent<AudioSource>();
    }

    public void OnEnable()
    {
        questGiver.OnNewQuest += UpdateQuestInfo;

    }

    public void OnDisable()
    {
        questGiver.OnNewQuest -= UpdateQuestInfo; //TODO: This apparently gives a null reference on exiting the game. Investigate (check script execution order)
    }

    private void UpdateQuestInfo(Quest quest)
    {
        if (currentQuest != null)
        {
            currentQuest.OnDialogueComplete.RemoveListener(ShowAndHideQuestRoll);
        }

        currentQuest = quest;
        currentQuest.OnDialogueComplete.AddListener(ShowAndHideQuestRoll);

        questTitleText.SetKey(currentQuest.TitleKey);
        questDescriptionText.SetKey(currentQuest.DescriptionKey);
    }

    public void ShowQuestRoll()
    {
        if (!isShowing)
        {
            audio_source.clip = QuestRollOpenSFX;
            if (audio_source.clip != null)
                audio_source.Play();
            questUpdatedAnimator.SetTrigger(showTrigger);
            isShowing = true;
        }
    }

    public void HideQuestRoll()
    {
        if (isShowing)
        {
            audio_source.clip = QuestRollCloseSFX;
            if (audio_source.clip != null)
                audio_source.Play();
            questUpdatedAnimator.SetTrigger(hideTrigger);
            isShowing = false;
        }
    }

    public void ShowAndHideQuestRoll()
    {
        StartCoroutine(ShowAndHide());
    }

    private IEnumerator ShowAndHide()
    {
        ShowQuestRoll();

        yield return new WaitForSeconds(ShowDuration);

        HideQuestRoll();
    }
}
