using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : ManagerBase
{
    private AudioSource bgmAudioSource;
    private AudioSource audio;
    public override void Init()
    {
        base.Init();
        if (bgmAudioSource == null)
        {
            if (bgmAudioSource == null)
            {
                GameObject bgmObj = new GameObject("bgmObj");
                bgmObj.transform.SetParent(this.transform);
                bgmAudioSource = bgmObj.AddComponent<AudioSource>();
            }
            if (audio == null)
            {
                GameObject audioObj = new GameObject("audioObj");
                audioObj.transform.SetParent(this.transform);
                audio = audioObj.AddComponent<AudioSource>();
            }
        }
    }
    private AudioClip bgm;

    public void SetBgm(AudioClip bgm, bool isPlaye = false)
    {
        this.bgm = bgm;
        if (isPlaye)
        {
            PlayBgm();
        }
    }

    public void PlayBgm(AudioClip audio)
    {
        SetBgm(audio, true);
    }

    public void PlayBgm()
    {
        bgmAudioSource.Play();
    }

    public void StopBgm()
    {
        bgmAudioSource.Stop();
    }

    public void PauseBgm()
    {
        bgmAudioSource.Pause();
    }

    public void PlayAudio(AudioClip clip, bool isLoop = false)
    {
        audio.clip = clip;
        PlayAudio(isLoop);
    }
    public void PlayAudio(bool isLoop = false)
    {
        if (audio.loop != isLoop)
        {
            audio.loop = isLoop;
        }
        audio.Play();
    }
    public void PlayAudio(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos);
    }
    public void StopAudio()
    {
        audio.Stop();
    }
}
