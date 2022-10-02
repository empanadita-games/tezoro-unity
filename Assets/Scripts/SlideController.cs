using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class SlideController : MonoBehaviour
{
    [Header("Slide")]
    [SerializeField] private GameObject[] slides;

    [Header("Controls")]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private UnityEvent SlidesStarted;
    [SerializeField] private UnityEvent SlidesFinished;


    private int index = 0;

    private void Start()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        RestartSlides();

        nextButton.onClick.AddListener(NextSlide);
        previousButton.onClick.AddListener(PreviousSlide);
    }

    private void OnDestroy()
    {
        nextButton.onClick.RemoveListener(NextSlide);
        previousButton.onClick.RemoveListener(PreviousSlide);
    }

    private void CheckButtonStatus()
    {
        nextButton.interactable = index < slides.Length-1;
        previousButton.interactable = index > 0;

        if(index < slides.Length - 1)
        {
            closeButton.gameObject.SetActive(false);
        }
        else
        {
            closeButton.gameObject.SetActive(true);
        }
    }

    private void ReproduceAnim(GameObject Slide, bool fadeIn)
    {
        var canvasGroup = Slide.GetComponent<CanvasGroup>();

        if (fadeIn)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1, 1f);
        }
        else
        {
            canvasGroup.alpha = 1f;
            canvasGroup.DOFade(0, 1f);
        }
    }

    public void RestartSlides()
    {
        foreach (var slide in slides)
        {
            slide.SetActive(false);
        }
        slides[index].SetActive(true);
        index = 0;
        CheckButtonStatus();
    }

    public void NextSlide()
    {
        var currentSlide = slides[index];
        ReproduceAnim(currentSlide, false);
        index++;

        var nextSlide = slides[index];
        nextSlide.SetActive(true);

        CheckButtonStatus();
        ReproduceAnim(nextSlide, true);
    }

    public void PreviousSlide()
    {
        var currentSlide = slides[index];
        ReproduceAnim(currentSlide, false);
        index--;

        var nextSlide = slides[index];
        nextSlide.SetActive(true);

        CheckButtonStatus();
        ReproduceAnim(nextSlide, true);
    }

    public void Show()
    {
        ReproduceAnim(gameObject, true);
        SlidesStarted?.Invoke();
        GameManager.Instance.BlockPlayerInput();
    }

    public void Hide()
    {
        ReproduceAnim(gameObject, false);
        SlidesFinished?.Invoke();
        GameManager.Instance.UnblockPlayerInput();

    }
}
