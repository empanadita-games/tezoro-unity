using System.Collections;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distanceFromPlayer = 1f;
    [SerializeField] private Transform player;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;


    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < distanceFromPlayer)
        {
            EscapeToRight();
        }
        else 
        {
            Idle();
        }

        anim.SetFloat("vSpeed", rb.velocity.x);
    }

    private void EscapeToRight()
    {
        rb.velocity = transform.right * speed;

        Vector3 scale = transform.localScale;
        scale.x = -1;
        transform.localScale = scale;
    }

    private void Idle()
    {
        rb.velocity = Vector2.zero;
        Vector3 scale = transform.localScale;
        scale.x = 1;
        transform.localScale = scale;
    }

}
