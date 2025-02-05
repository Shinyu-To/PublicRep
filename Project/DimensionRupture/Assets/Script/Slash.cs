using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{

    [SerializeField]public PlayerStatus playerStatus;
    public int attackDamage;

    void Start()
    {
        attackDamage = playerStatus.ATK;

    }

    //�_���[�W��^���锻�f
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("We have Hitted the Enemy");

            EnemyStatus enemy = other.gameObject.GetComponent<EnemyStatus>();

            if (!enemy.isAttacked)
            {
                other.gameObject.GetComponent<EnemyStatus>().TakenDamage(attackDamage);

                //�G���_���[�W�󂯂���̌��
                Vector2 difference = other.transform.position - transform.position;
                other.transform.position = new Vector2(other.transform.position.x + difference.x / 4,
                                                       other.transform.position.y);
                //�G���_���[�W�󂯂���̌��
            }
        }
    }
}
