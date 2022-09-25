using System.Collections;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distanceFromDepredator = 1f;
    [SerializeField] private Transform depredator;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;

    private bool inIdle;


    void Start()
    {
        
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, depredator.position) < distanceFromDepredator)
        {
            EscapeToRight();
            inIdle = false;
        }
        else 
        {
            if(!inIdle)
                StartCoroutine(IdleCoroutine());
        }

        anim.SetFloat("vSpeed", rb.velocity.x);
    }

    private void EscapeToRight()
    {
        rb.velocity = transform.right * speed;

        Vector3 theScale = transform.localScale;
        theScale.x = -1;
        transform.localScale = theScale;
    }

    private void Idle()
    {
        rb.velocity = Vector2.zero;
        Vector3 theScale = transform.localScale;
        theScale.x = 1;
        transform.localScale = theScale;
    }

    private IEnumerator IdleCoroutine()
    {
        rb.velocity = Vector2.zero;
        Vector3 theScale = transform.localScale;
        theScale.x = 1;
        transform.localScale = theScale;

        while (inIdle)
        {
            anim.SetBool("Eating", false);
            yield return new WaitForSeconds(2f);
            anim.SetBool("Eating",true);
        }

        anim.SetBool("Eating", false);
    }


}
