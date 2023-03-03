using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMRabbit : MonoBehaviour
{
    //FSM
    public enum PlayerInputs { WP_FLEE, WP_IDLE, SIMPLE_IDLE, SIMPLE_FLEE, CONVERTING }
    private EventFSM<PlayerInputs> _myFsm;

    //RABBIT
    [SerializeField] private float speed;
    [SerializeField] private float fastSpeed;
    [SerializeField] private float distanceTolerance = 1f;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator anim;
    
    //waypoints
    public GameObject[] waypoints;
    private int currentWP = 0;
    private static readonly int Convert1 = Animator.StringToHash("Convert");
    private static readonly int VSpeed = Animator.StringToHash("vSpeed");

    [SerializeField] int coinzDropped;
    [SerializeField]private float coinzInterval;
    
    public GameObject coin;
    public GameObject sapo;
    [SerializeField] private GameObject magicParticles;
    [SerializeField] private Vector3 sapoSpawnOffset;
    private void Awake()
    {
       //PARTE 1: SETEO INICIAL
       //Creo los estados
        var wpFlee = new State<PlayerInputs>("WaypointFlee");
        var wpIdle = new State<PlayerInputs>("WaypointIdle");
        var simpleIdle = new State<PlayerInputs>("SimpleIdle");
        var simpleFlee = new State<PlayerInputs>("SimpleFlee");
        var converting = new State<PlayerInputs>("Convert");

        //creo las transiciones
        StateConfigurer.Create(wpIdle)
            .SetTransition(PlayerInputs.WP_FLEE, wpFlee)
            .SetTransition(PlayerInputs.SIMPLE_IDLE, simpleIdle)
            .SetTransition(PlayerInputs.SIMPLE_FLEE, simpleFlee)
            .SetTransition(PlayerInputs.CONVERTING, converting)
            .Done();

        StateConfigurer.Create(wpFlee)
            .SetTransition(PlayerInputs.WP_IDLE, wpIdle)
            .SetTransition(PlayerInputs.SIMPLE_IDLE, simpleIdle)
            .SetTransition(PlayerInputs.SIMPLE_FLEE, simpleFlee)
            .SetTransition(PlayerInputs.CONVERTING, converting)
            .Done();

        StateConfigurer.Create(simpleIdle)
            .SetTransition(PlayerInputs.SIMPLE_FLEE, simpleFlee)
            .SetTransition(PlayerInputs.WP_IDLE, wpIdle)
            .SetTransition(PlayerInputs.WP_FLEE, wpFlee)
            .SetTransition(PlayerInputs.CONVERTING, converting)
            .Done();
        StateConfigurer.Create(simpleFlee)
            .SetTransition(PlayerInputs.SIMPLE_IDLE, simpleIdle)
            .SetTransition(PlayerInputs.WP_IDLE, wpIdle)
            .SetTransition(PlayerInputs.WP_FLEE, wpFlee)
            .SetTransition(PlayerInputs.CONVERTING, converting)
            .Done();
        
        StateConfigurer.Create(converting).Done();

        //PARTE 2: SETEO DE LOS ESTADOS
        simpleIdle.OnUpdate += () =>
        {
            if (GetXDistance(player) <= distanceTolerance)
            {
                SendInputToFSM(PlayerInputs.SIMPLE_FLEE);
            }
        };
        
        simpleIdle.OnEnter += x =>
        {
            Idle();
        };

        simpleFlee.OnUpdate += () =>
        {
            EscapeToRight();
            if (GetXDistance(player) > distanceTolerance)
            {
                SendInputToFSM(PlayerInputs.SIMPLE_IDLE);
            }
        };
        
        simpleFlee.OnEnter += x =>
        {
            LookRight(true);
        };

        wpIdle.OnUpdate += () =>
        {
            Idle();
            if (GetXDistance(player) < distanceTolerance * 1.5f) SendInputToFSM(PlayerInputs.WP_FLEE); 
        };

        wpFlee.OnEnter += x =>
        {
            StartCoroutine(Coro_DropCoinz(coinzDropped, coinzInterval));
        };

        wpFlee.OnUpdate += () =>
        {
            RunToWp();
            if (GetXDistance(waypoints[currentWP].transform) <= 0.5f)
            {
                currentWP += 1;
                if (currentWP >= waypoints.Length)
                    //End of waypoints
                {
                    SendInputToFSM(PlayerInputs.SIMPLE_IDLE);
                    currentWP = 0;
                }
                else
                {
                    //Wait to go to next waypoint
                    SendInputToFSM(PlayerInputs.WP_IDLE);
                }
            }
        };

        converting.OnEnter += x =>
        {
            rb.velocity = Vector2.zero;
            LookRight(true);
            anim.SetTrigger(Convert1);
            Instantiate(magicParticles, transform.position, Quaternion.identity);
        };

        _myFsm = new EventFSM<PlayerInputs>(wpIdle);
    }

    private void SendInputToFSM(PlayerInputs inp)
    {
        _myFsm.SendInput(inp);
    }

    private void Update()
    {
        _myFsm.Update();
        anim.SetFloat(VSpeed, rb.velocity.x);
        //Debug.Log("state:" +_myFsm.Current.Name);
    }

    private void FixedUpdate()
    {
        _myFsm.FixedUpdate();
        anim.SetFloat(VSpeed, rb.velocity.x);
    }

    private void RunToWp()
    {
        rb.velocity = transform.right * fastSpeed;
        LookRight(true);
    }
    
    private void EscapeToRight()
    {
        rb.velocity = transform.right * speed ;
    }
    
    private void Idle()
    {
        rb.velocity = Vector2.zero;
        LookRight(false);
    }

    float GetXDistance(Transform target)
    {
        return Mathf.Abs(transform.position.x - target.position.x);
    }

    void LookRight(bool right)
    {
        //renderer.flipX = !right; //Deberia funcionar con esto sólo, pero si se usa esto se producen cortes en los cambios de animacion
        Vector3 scale = transform.localScale;
        scale.x = (right?-1:1);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Conversion"))
        {
            SendInputToFSM(PlayerInputs.CONVERTING);
        }
    }

    public void DestroyAndSpawnSapo()
    {
        Instantiate(sapo, transform.position+sapoSpawnOffset, transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator Coro_DropCoinz(int n, float waitTime)
    {
        for (int i = 0; i < n; i++)
        {
            Instantiate(coin, transform.position, transform.rotation);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
