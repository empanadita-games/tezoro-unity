using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TweenTheEnd : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI end1;
    [SerializeField]
    private TextMeshProUGUI end2;
    [SerializeField]
    private TextMeshProUGUI credit;

    private void Start()
    {
        end2.alpha = 0;
        credit.alpha = 0;
        var sq = DOTween.Sequence();
        sq.Append(end1.DOFade(1, 2));
        sq.Append(end2.DOFade(1, 2f));
        sq.AppendInterval(2f);
        sq.Append(credit.DOFade(1,4));
    }
}
