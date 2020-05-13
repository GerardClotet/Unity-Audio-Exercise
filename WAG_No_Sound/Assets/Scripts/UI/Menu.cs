////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void MenuStateEvent(bool state);
public class Menu : MonoBehaviour
{
    public static bool isOpen = false;
    public static MenuStateEvent OnMenuStateChange;

    //[Header("Wwise")]
    //public AK.Wwise.RTPC MenuRTPC;
    //public AK.Wwise.Event MenuOpenSound;
    //public AK.Wwise.Event MenuCloseSound;

    [Header("Other")]
    public AnimatedObjectActiveHandler ControlsBox;
    public AnimatedObjectActiveHandler QuestBox;
    public bool GetMouseWithP = false;

    public MenuEvent OnMenuDown;
    //[SerializeField]
    //private AudioSource MenuOpenSFX;
    //[SerializeField]
    //private AudioSource MenuCloseSFX;

    //[Header("AudioClips")]
    public AudioClip OpenSFX;
    public AudioClip CloseSFX;

    
    private AudioSource audio_source;

    private bool menuOpen = false;


    public void Start()
    {
        audio_source = GameObject.Find("Menus").GetComponent<AudioSource>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && GetMouseWithP) {
            PlayerManager.Instance.cameraScript.FreezeAndShowCursor(true, gameObject);
            
        }
    }

    private void OnEnable()
    {
        
        InputManager.OnMenuDown += ToggleMenu;
    }

    private void OnDisable()
    {
        InputManager.OnMenuDown -= ToggleMenu;
    }

    public void ToggleMenu()
    {
        if (isOpen || !DialogueManager.DialogueActive)
        {
            menuOpen = !menuOpen;
            isOpen = menuOpen;
            if (menuOpen)
            {
               
                //if (MenuOpenSFX != null)
                //    MenuOpenSFX.Play();

                audio_source.clip = OpenSFX;
                if (audio_source.clip != null)
                    audio_source.Play();
                GameManager.Instance.gameSpeedHandler.PauseGameSpeed(gameObject.GetInstanceID());
                GameManager.Instance.BlurCam();

                QuestBox.EnableObject(0.5f);
#if UNITY_STANDALONE
                PlayerManager.Instance.cameraScript.FreezeAndShowCursor(true, gameObject);
                ControlsBox.EnableObject(0.5f);
#endif
            }
            else
            {
                //MenuCloseSound.Post(gameObject);
                //if (MenuCloseSFX != null)
                //    MenuCloseSFX.Play();
                //MenuRTPC.SetGlobalValue(0f);

                audio_source.clip = CloseSFX;
                if (audio_source.clip != null)
                    audio_source.Play();
                GameManager.Instance.gameSpeedHandler.UnPauseGameSpeed(gameObject.GetInstanceID());
                GameManager.Instance.UnBlurCam();
                QuestBox.DisableObject(0.25f);
#if UNITY_STANDALONE
                PlayerManager.Instance.cameraScript.FreezeAndShowCursor(false, gameObject);
                ControlsBox.DisableObject(0.25f);
#endif

            }

            if (OnMenuStateChange != null)
            {
                OnMenuStateChange(menuOpen);
            }

            OnMenuDown.Invoke(menuOpen);
        }
    }

    public void SetCameraSensitivity(float value)
    {
        PlayerManager.Instance.cameraScript.mouseSensitivity = value;
    }
}

[System.Serializable]
public class MenuEvent : UnityEvent<bool> { }
