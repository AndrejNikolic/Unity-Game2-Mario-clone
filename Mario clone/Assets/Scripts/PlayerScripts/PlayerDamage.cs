using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    private Text lifeText;
    private int lifeCount;

    private bool canDamage;

    private void Awake()
    {
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

            if (lifeCount >= 0)
            {
                lifeText.text = "x" + lifeCount;
            }
            if (lifeCount == 0)
            {
                //restart
                Time.timeScale = 0f; //stopping game
                StartCoroutine(RestartGame());
            }
            canDamage = false;

            StartCoroutine(WaitForDamage());
        }
        
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
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
