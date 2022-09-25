using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Sapotezos : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    
    public void FadeOut()
    {
        sprite.DOFade(0, .5f);
    }
}
