////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    public MaterialChecker materialChecker;
    public AK.Wwise.Event FootstepSound;

    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource footSource;
    [SerializeField]
    private AudioSource footSource2;
    [SerializeField]
    private AudioSource footSource3;

    #region private variables
    private bool inWater;
    #endregion

    public void PlayFootstepSound()
    {
        if (!inWater)
        {
            materialChecker.CheckMaterial(gameObject); //This also sets the material if a SoundMaterial is found!
        }

        Random.InitState((int)Time.realtimeSinceStartup);
        int random = Random.Range(0, 3);
        Debug.Log("Player foot sfx random index: " + random); 

        switch (random)
        {
            case 0:
                footSource.Play();
                break;
            case 1:
                footSource2.Play();
                break;
            case 2:
                footSource3.Play();
                break;
        }
       
        FootstepSound.Post(gameObject);
    }

    public void EnterWaterZone()
    {
        inWater = true;
    }

    public void ExitWaterZone()
    {
        inWater = false;
    }

}
