using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    private Text lifeText;
    private int lifeCount;
    private Rigidbody2D myBody;

    private bool canDamage;

    private Animator anim;
    private SpriteRenderer sprite;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeCount = 3;
        lifeText.text = "x" + lifeCount;

        canDamage = true;
    }

    public void DealDamage()
    {
        if (canDamage)
        {
            lifeCount--;
            anim.Play("PlayerDamaged");
            myBody.velocity = new Vector2(myBody.velocity.x, 7f);

            if (lifeCount >= 0)
            {
                StartCoroutine(Blink());
                lifeText.text = "x" + lifeCount;
            }
            if (lifeCount == 0)
            {
                //restart
                Time.timeScale = 0f; //stopping game
                StartCoroutine(RestartGame());
            }
            canDamage = false;

            StartCoroutine(WaitForIdle(0.3f));
            StartCoroutine(WaitForDamage());
        }
        
    }

    IEnumerator Blink()
    {
        sprite.color = new Color(1, 1, 1, .5f);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(Blink());
    }

    IEnumerator WaitForDamage()
    {
        StopCoroutine(Blink());
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator WaitForIdle(float time)
    {
        yield return new WaitForSeconds(time);
        anim.Play("PlayerIdle");
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Gameplay");
        
    }
} //end PlayerDamage
