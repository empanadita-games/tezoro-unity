using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxFPS = 60;

    void Awake()
    {
        Application.targetFrameRate = maxFPS;
    }

}
