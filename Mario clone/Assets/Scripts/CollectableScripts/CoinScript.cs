using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    private Text coinTextScore;
    private AudioSource audioManager;

    public static int score = 0;

    private void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.COIN_TAG)
        {
            collision.gameObject.SetActive(false);
            score++;
            coinTextScore.text = "x" + score.ToString();
            audioManager.Play();
        }
    }
}
