using UnityEngine;
using DG.Tweening;
using TriInspector;

public class FadeController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private bool fadeInAtStart = true;

    private bool inTransition;

    private void Start()
    {
        if(fadeInAtStart) PlayFadeIn();
    }

    public void PlayFadeIn(float duration = default(float))
    {
        if (inTransition) return;

        if (duration == default(float)) duration = fadeInDuration;

        inTransition = true;
        canvasGroup.alpha = 1;
        var Tween = canvasGroup.DOFade(0,duration);
        Tween.onComplete += () => inTransition = false;
    }

    public void PlayFadeOut(float duration = default(float))
    {
        if (inTransition) return;

        if (duration == default(float)) duration = fadeOutDuration;

        inTransition = true;
        canvasGroup.alpha = 0;
        var Tween = canvasGroup.DOFade(1, duration);
        Tween.onComplete += () => inTransition = false;
    }
}
