using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D myBody;
    private Animator anim;

    public LayerMask playerLayer;
    public LayerMask groundLayer;

    private bool moveLeft;

    private bool canMove;
    private bool stunned;

    public Transform left_Collision, right_Collision, top_Collision, down_Collision;
    private Vector3 left_Collision_Position, right_Collision_Position;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        left_Collision_Position = left_Collision.position;
        right_Collision_Position = right_Collision.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }
        
        CheckCollision();
    }

    void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.3f, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    // Bounce off player when snail is stunned
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = 
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);

                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                    anim.Play("Stunned");
                    stunned = true;

                    if (tag == MyTags.BEETLE_TAG)
                    {
                        //anim.Play("Stunned");
                        StartCoroutine(Dead(0.5f));
                        CoinScript.addScore();
                    }

                } else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                        CoinScript.addScore();
                    }
                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    //Apply damage to player
                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                        CoinScript.addScore();
                    }
                }
            }
        }
        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    //Apply damage to player
                    rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                        CoinScript.addScore();
                    }
                }
            }
        }

        // Testing if we don't detect collision
        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.2f, groundLayer))
        {
            ChangeDirection();
        }

    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            left_Collision.position = left_Collision_Position;
            right_Collision.position = right_Collision_Position;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
            left_Collision.position = right_Collision_Position;
            right_Collision.position = left_Collision_Position;
        }

        transform.localScale = tempScale;
    }
    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.BULLET_TAG)
        {
            if (tag == MyTags.BEETLE_TAG)
            {
                anim.Play("Stunned");
                canMove = false;
                myBody.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.4f));
                CoinScript.addScore();
            } else if(tag == MyTags.SNAIL_TAG)
            {
                if (!stunned)
                {
                    anim.Play("Stunned");
                    canMove = false;
                    stunned = true;
                    myBody.velocity = new Vector2(0, 0);
                    CoinScript.addScore();
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            ChangeDirection();
        }
    }

} // end SnailScript
