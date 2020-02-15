using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator anim;

    private bool animation_Started;
    private bool animation_Finished;

    private int jumpedTimes;
    private bool jumpedLeft = true;

    private string coroutine_Name = "FrogJump";

    public LayerMask playerLayer;
    private GameObject player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutine_Name);
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
    }

    void LateUpdate() //called at the end of frame
    {
        if (animation_Finished && animation_Started)
        {
            animation_Started = false;

            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer))
        {
            player.GetComponent<PlayerDamage>().DealDamage();
        }
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        animation_Started = true;
        animation_Finished = false;

        jumpedTimes++;

        if (jumpedLeft)
        {
            anim.Play("FrogJumpLeft");
        }
        else
        {
            anim.Play("FrogJumpRight");
            Vector3 tempScale = transform.localScale;
            tempScale.x = -1;
            transform.localScale = tempScale;
        }

        StartCoroutine(coroutine_Name);
    }

    void AnimationFinished()
    {
        animation_Finished = true;
        anim.Play("FrogIdleLeft");

        if (jumpedLeft)
        {
            anim.Play("FrogIdleLeft");
        }
        else
        {
            anim.Play("FrogIdleRight");
        }

        if (jumpedTimes == 3)
        {
            jumpedTimes = 0;
            jumpedLeft = !jumpedLeft;
        }
    }

} //end FrogScript
