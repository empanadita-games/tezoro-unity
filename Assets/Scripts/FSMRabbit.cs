using FSM;
using System;
using UnityEngine;

public class FSMRabbit : MonoBehaviour
{
    //FSM
    public enum PlayerInputs { WPFLEE, WPIDLE, SIDLE, SFLEE, CONVERTING }
    private EventFSM<PlayerInputs> _myFsm;

    //RABBIT
    [SerializeField] private float speed;
    [SerializeField] private float distanceTolerance = 1f;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Animator anim;
    
    
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
            .SetTransition(PlayerInputs.WPFLEE, wpFlee)
            .SetTransition(PlayerInputs.SIDLE, simpleIdle)
            .SetTransition(PlayerInputs.SFLEE, simpleFlee)
            .SetTransition(PlayerInputs.CONVERTING, converting)
            .Done();

        StateConfigurer.Create(wpFlee)
            .SetTransition(PlayerInputs.WPIDLE, wpIdle)
            .SetTransition(PlayerInputs.SIDLE, simpleIdle)
            .SetTransition(PlayerInputs.SFLEE, simpleFlee)
            .SetTransition(PlayerInputs.CONVERTING, converting)
            .Done();

        StateConfigurer.Create(simpleIdle)
            .SetTransition(PlayerInputs.SFLEE, simpleFlee)
            .SetTransition(PlayerInputs.WPIDLE, wpIdle)
            .SetTransition(PlayerInputs.WPFLEE, wpFlee)
            .SetTransition(PlayerInputs.CONVERTING, converting)
            .Done();
        StateConfigurer.Create(simpleFlee)
            .SetTransition(PlayerInputs.SIDLE, simpleIdle)
            .SetTransition(PlayerInputs.WPIDLE, wpIdle)
            .SetTransition(PlayerInputs.WPFLEE, wpFlee)
            .SetTransition(PlayerInputs.CONVERTING, converting)
            .Done();
        
        StateConfigurer.Create(converting).Done();

        //PARTE 2: SETEO DE LOS ESTADOS
        simpleIdle.OnUpdate += () =>
        {
            if (GetXDistance() <= distanceTolerance)
            {
                SendInputToFSM(PlayerInputs.SFLEE);
            }
        };

        simpleFlee.OnUpdate += () =>
        {
            DoSimpleFlee();
            if (GetXDistance() > distanceTolerance * 1.2f)
            {
                SendInputToFSM(PlayerInputs.SIDLE);
            }
        };

        simpleIdle.OnEnter += x =>
        {
            DoIdle();
        };

        wpIdle.OnUpdate += () =>
        {
            DoIdle();
        };
        _myFsm = new EventFSM<PlayerInputs>(simpleIdle);
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
    
    private void DoSimpleFlee()
    {
        rb.velocity = transform.right * speed;
        LookRight(true);
    }

    private void DoIdle()
    {
        rb.velocity = Vector2.zero;
        LookRight(false);
    }

    float GetXDistance()
    {
        return Mathf.Abs(transform.position.x - player.position.x);
    }

    void LookRight(bool right)
    {
        renderer.flipX = !right;
    }
}
