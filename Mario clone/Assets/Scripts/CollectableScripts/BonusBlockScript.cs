using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlockScript : MonoBehaviour
{
    public Transform bottom_Collision;

    private Animator anim;
    private AudioSource audioManager;
    public LayerMask playerLayer;

    private Vector3 moveDirection = Vector3.up;
    private Vector3 originPosition;
    private Vector3 animPosition;

    private bool startAnim;
    private bool activeBlock = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioManager = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        animPosition = transform.position;
        animPosition.y += 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForCollision();
        AnimateUpDown();
    }

    void CheckForCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(bottom_Collision.position, Vector2.down, 0.1f, playerLayer);

        if (activeBlock)
        {
            if (hit)
            {
                if (hit.collider.gameObject.tag == MyTags.PLAYER_TAG)
                {

                    audioManager.Play();
                    //increase score
                    CoinScript.addScore();
                    activeBlock = false;
                    //change block
                    anim.Play("BlockCollected");
                    startAnim = true;
                }
            }
        }
        
    }

    void AnimateUpDown()
    {
        if (startAnim)
        {
            transform.Translate(moveDirection * Time.smoothDeltaTime);
            if (transform.position.y >= animPosition.y)
            {
                moveDirection = Vector3.down;
            }
            else if (transform.position.y <= originPosition.y)
            {
                startAnim = false;
                moveDirection = Vector3.zero;
            }
        }
    }

} // end BonusBlockScript
