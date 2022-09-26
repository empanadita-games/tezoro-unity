using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TweenStart : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;

    private void Start()
    {
        image.DOFade(0, 1f);
        text.DOFade(0, 1.2f);
    }
}
