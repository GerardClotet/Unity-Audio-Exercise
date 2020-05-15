////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
public class SliderControlledRTPC : MonoBehaviour
{
    public AudioMixer mixer;


    //public void SetMasterVolume(float value);
    //{
    //mixer.Set
    //}
    

    public void SetMasterVolume(float value)
    {
        mixer.SetFloat("MasterVolume", value);
    }


    public void SetAudioVolume(float value)
    {
        mixer.SetFloat("AudioVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", value);
    }

    public void SetVoiceVolume(float value)
    {
        mixer.SetFloat("VoiceVolume", value);
    }
}
