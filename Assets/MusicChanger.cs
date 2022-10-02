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
        firstAudio.DOFade(0, 6f);
        secondAudio.DOFade(maxVolume, 4f);
        secondAudio.Play();
    }
}
