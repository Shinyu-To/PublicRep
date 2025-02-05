using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    //public int damage;
    public float startTime;
    public float time;

    private Animator anim;
    private new PolygonCollider2D collider2D;

    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack1") || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            anim.SetTrigger("Attack");
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        collider2D.enabled = true;
        StartCoroutine(disableHitBox());
    }

    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;
    }
}
