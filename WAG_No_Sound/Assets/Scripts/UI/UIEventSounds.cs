////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventSounds : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    //public AK.Wwise.Event OnPointerDownSound;
    //public AK.Wwise.Event OnPointerUpSound;
    //public AK.Wwise.Event OnPointerEnterSound;
    //public AK.Wwise.Event OnPointerExitSound;

    [Header("Audios")]
    public AudioClip EnterSFX;
    public AudioClip OverSFX;

    private AudioSource audio_source;
    public void Start()
    {
        if(GameObject.Find("Menus") != null)
            audio_source = GameObject.Find("Menus").GetComponent<AudioSource>();

        else audio_source = GameObject.Find("Menu").GetComponent<AudioSource>();

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        audio_source.clip = EnterSFX;
        if (audio_source.clip != null)
            audio_source.Play();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        audio_source.clip = OverSFX;
        if (audio_source.clip != null)
            audio_source.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // OnPointerExitSound.Post(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      //  OnPointerUpSound.Post(gameObject);
    }
}
