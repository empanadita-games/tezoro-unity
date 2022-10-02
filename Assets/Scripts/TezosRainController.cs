using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TezosRainController : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnLength = 10f;
    [SerializeField] private float spawnMoveSpeed = 10f;
    [SerializeField] private float spawnInterval;
    [SerializeField, Range(0f,1f)] private float randomLerp = 0f;

    [Header("Tezos")]
    [SerializeField] private GameObject tezoPrefab;
    [SerializeField] private float tezosLimit = 100f;

    [Header("Debug")]
    [SerializeField] private bool showGizmos;

    private Vector2 spawnPosLeft;
    private Vector2 spawnPosRight;
    private bool isRainActive;

    public UnityEvent onRainEnd;
    private void OnDrawGizmos()
    {
        if (!isRainActive)
        {
            spawnPosLeft = new Vector2(transform.position.x - spawnLength / 2, transform.position.y);
            spawnPosRight = new Vector2(transform.position.x + spawnLength / 2, transform.position.y);
            spawnPoint.position = transform.position;
        }

        if (!showGizmos) return;

        Gizmos.DrawLine(spawnPosLeft, spawnPosRight);
        Gizmos.DrawWireSphere(spawnPosLeft, 0.3f);
        Gizmos.DrawWireSphere(spawnPosRight, 0.3f);
        Gizmos.color = new Color(0.2f,0.8f,1f);
        Gizmos.DrawWireSphere(spawnPoint.position, 0.5f);
    }

    private void Update()
    {       
        MoveRainSpawnPoint();

        // if (Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //    StartTezosRain();
        // }
        //
        // if (Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //    StopAllCoroutines();
        // }
    }

    private void MoveRainSpawnPoint()
    {
        if (!isRainActive) return;

        spawnPosLeft = new Vector2(transform.position.x - spawnLength / 2, transform.position.y);
        spawnPosRight = new Vector2(transform.position.x + spawnLength / 2, transform.position.y);

        var randomSpeed = Mathf.Lerp(1f,Random.Range(0.1f,0.5f)/spawnMoveSpeed, randomLerp);
        Vector2 pos = new Vector2(Mathf.Sin(Time.time * spawnMoveSpeed * randomSpeed) * spawnLength / 2 + transform.position.x, transform.position.y);
        spawnPoint.position = pos;
    }

    private IEnumerator TezosSpawner()
    {
        var tezosCounter = 0f;

        while (isRainActive)
        {
            yield return new WaitForSeconds(spawnInterval);

            if(tezoPrefab)
            {
                Instantiate(tezoPrefab, spawnPoint.position,tezoPrefab.transform.rotation);
                tezosCounter++;
            }

            if(tezosCounter >= tezosLimit)
                isRainActive = false;
        }
        onRainEnd.Invoke();
    }


    public void StartTezosRain()
    {
        if (!isRainActive)
        {
            isRainActive = true;
            StartCoroutine(TezosSpawner());
        }
    }

    public void StopTezosRain()
    {
        if (isRainActive)
        {
            isRainActive = false;
        }
    }
}
