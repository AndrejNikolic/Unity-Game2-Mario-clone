using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{
    public GameObject fireBullet;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }

    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity); // Creating bullets
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
            anim.Play("PlayerAttack");
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            anim.Play("PlayerIdle");
        }

    }

} //end PlayerShootScript
