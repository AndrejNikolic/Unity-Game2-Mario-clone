using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpiderScript : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D myBody;

    private Vector3 moveDirection = Vector3.down;

    private string coroutine_Name = "ChangeMovement";

    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutine_Name);
    }

    // Update is called once per frame
    void Update()
    {
        moveSpider();
    }

    void moveSpider()
    {
        transform.Translate(moveDirection * Time.smoothDeltaTime);
    }

    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));

        if (moveDirection== Vector3.down)
        {
            moveDirection = Vector3.up;
        }
        else
        {
            moveDirection = Vector3.down;
        }
        StartCoroutine(coroutine_Name);
    }

    IEnumerator SpiderDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.BULLET_TAG)
        {
            anim.Play("SpiderDead");
            myBody.bodyType = RigidbodyType2D.Dynamic;

            CoinScript.addScore();
            StartCoroutine(SpiderDead());
            StopCoroutine(coroutine_Name);
        }
        if (collision.tag == MyTags.PLAYER_TAG)
        {
            collision.GetComponent<PlayerDamage>().DealDamage();
        }
    }

} //end SpiderScript
