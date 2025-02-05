using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] public EnemyStatus enemyStatus;
    public int attackDamage;

    void Start()
    {
        attackDamage = enemyStatus.ATK;

    }

    //�_���[�W��^���锻�f
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("We have Hitted the Player");

            PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();

            if (!player.isAttacked)
            {
                other.gameObject.GetComponent<PlayerStatus>().TakenDamage(attackDamage);

                //�G���_���[�W�󂯂���̌��
                Vector2 difference = other.transform.position - transform.position;
                other.transform.position = new Vector2(other.transform.position.x + difference.x / 4,
                                                       other.transform.position.y);
                //�G���_���[�W�󂯂���̌��
            }
        }
    }
}
