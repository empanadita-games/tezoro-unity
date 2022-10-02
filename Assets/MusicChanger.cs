using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public AudioSource firstAudio;
    public AudioSource secondAudio;
    
    public float maxVolume = 0.5f;

    public void ChangeMusic()
    {
        firstAudio.DOFade(maxVolume, 1f);
        secondAudio.DOFade(maxVolume, 1f);
        secondAudio.Play();
    }
}
