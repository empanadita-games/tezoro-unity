using FSM;
using System;
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
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Animator anim;
    
    //waypoints
    public GameObject[] waypoints;
    private int currentWP = 0;
    
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
            if (GetXDistance(player) < distanceTolerance * 2) SendInputToFSM(PlayerInputs.WP_FLEE); 
        };
        
        wpFlee.OnUpdate += () =>
        {
            RunToWP();
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

        _myFsm = new EventFSM<PlayerInputs>(wpIdle);
    }

    private void SendInputToFSM(PlayerInputs inp)
    {
        _myFsm.SendInput(inp);
    }

    private void Update()
    {
        _myFsm.Update();
        anim.SetFloat("vSpeed", rb.velocity.x);
        Debug.Log("state:" +_myFsm.Current.Name);
    }

    private void FixedUpdate()
    {
        _myFsm.FixedUpdate();
        anim.SetFloat("vSpeed", rb.velocity.x);
    }

    private void RunToWP()
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
}
