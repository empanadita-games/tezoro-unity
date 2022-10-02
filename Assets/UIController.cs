using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI walletAddress;
    public TextMeshProUGUI tezos;
    [SerializeField] private FadeController fadeController;

    public static UIController Instance;

    public FadeController Fade => fadeController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Start()
    {
        Fade.PlayFadeIn();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Fade.PlayFadeIn();
    }
}
