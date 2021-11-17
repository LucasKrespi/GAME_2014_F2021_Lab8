 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Player Detection")]
    public LOS enemyLOS;

    [Header("Movement")]
    public float runForce;
    public Transform lookAhead;
    public Transform lookForward;
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    public bool isGroundAhead;


    private Transform temp;
    private Animator animatorController;
    private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
        enemyLOS = GetComponent<LOS>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAhead();
        LookForward();

        if (!hasLOS())
        {
            animatorController.enabled = true;
            MoveEnemy();
        }
        else
        {
            animatorController.enabled = false;
        }
    }

    private bool hasLOS()
    {

        if(enemyLOS.colisionList.Count > 0)
        {
            if(enemyLOS.collidesWith.gameObject.CompareTag("Player") && enemyLOS.colisionList[0].gameObject.CompareTag("Player"))
            {
                return true;
            }
            else
            {
                foreach(var collider in enemyLOS.colisionList)
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        var hit = Physics2D.Linecast(lookForward.transform.position, collider.gameObject.transform.position, enemyLOS.contactFilter.layerMask);

                        if((hit) && (hit.collider.gameObject.CompareTag("Player")))
                        {
                            return true;
                        }
                    }
             
                }   
            }
            
        }

        return false;
    }

    private void LookAhead()
    {
        var hit = Physics2D.Linecast(transform.position, lookAhead.position, groundLayerMask);
        isGroundAhead = hit;
    }

    private void LookForward()
    {
        var hit = Physics2D.Linecast(transform.position, lookForward.position, wallLayerMask);
        if (hit)
        {
            Flip();
        }
    }
    private void MoveEnemy()
    {
        if (isGroundAhead)
        {
            rigidbody.AddForce(Vector2.left * runForce * transform.localScale.x);
            rigidbody.velocity *= 0.90f;
        }
        else
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataform"))
        {
            transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, lookAhead.position);
        Gizmos.DrawLine(transform.position, lookForward.position);
    }
}
