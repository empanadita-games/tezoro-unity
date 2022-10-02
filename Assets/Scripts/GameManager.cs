using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private int maxFPS = 60;
    public int tezosCollected;

    public UIController UIController;

    private PlayerController playerController;
    private bool IsPlayerInputBlocked => playerController.Input.Blocked;


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
        Application.targetFrameRate = maxFPS;
        playerController = FindObjectOfType<PlayerController>();
    }


    public void AddTezos(int n)
    {
        tezosCollected += n;
        UIController.Instance.tezos.text = tezosCollected.ToString();
    }

    public void StartGetTezos()
    {
        GameController.instance.CallGetTezos(tezosCollected, "tz2W9y2NMFYX8awk6W27q49yaoJoD8uMCBHi");
    }
    
    public void StartWebGetTezos()
    {
        GameController.instance.WebGetTezos(tezosCollected);
    }

    public void BlockPlayerInput()
    {
        playerController.Input.BlockInput();
    }

    public void UnblockPlayerInput()
    {
        playerController.Input.UnblockInput();
    }

}
