using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private Animator anim;
    private int health = 10;

    private bool canDamage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDamage)
        {
            if (collision.tag == MyTags.BULLET_TAG)
            {
                health--;
                CoinScript.addScore();
                canDamage = false;

                if (health == 0)
                {
                    GetComponent<BossScript>().DeactivateBoss();
                    anim.Play("BossDead");
                    Invoke("BossGoneNew", 2f); // 2 ways to solve this - IEnumerator or Invoke
                    //StartCoroutine(BossGone()); 
                }

                StartCoroutine(WaitForDamage());
            }
        }
    }

    //IEnumerator BossGone()
    //{
    //    yield return new WaitForSeconds(2f);
    //    GetComponent<BossScript>().gameObject.SetActive(false);
    //}
    void BossGoneNew()
    {
        GetComponent<BossScript>().gameObject.SetActive(false);
    }
}
