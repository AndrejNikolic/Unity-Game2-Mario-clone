using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    //private AudioSource audioManager;

    private void Awake()
    {
        //audioManager = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Deactivate", 4f);
        //audioManager.Play();
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.PLAYER_TAG)
        {
            collision.GetComponent<PlayerDamage>().DealDamage();
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
