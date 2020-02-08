using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;

    public float jumpPower = 5f;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Physics2D.Raycast (groundCheckPosition.position, Vector2.down, 0.5f)) //draws the line for early collision
        //{
        //    print("Player collided with ground");
        //}
        CheckIfGrounded();
        PlayerJump();
    }

    // called every few frames, change it in Edit > Project Settings > Time > Fixed Timestep
    private void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal"); // prints whole number for left and right (-1 and 1)
        if (h==1)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y); // velocity - speed over time
            ChangeDirection(1);

        } else if (h == -1)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        } else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);
        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                anim.SetBool("Jump", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            print("Player collided with ground");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //works only if one of two game objects is set as trigger in box collider
    {
        //if (collision.tag == "Ground")
        //{
        //    print("Player collided with ground");
        //}
    }

}// end PlayerMovement 
