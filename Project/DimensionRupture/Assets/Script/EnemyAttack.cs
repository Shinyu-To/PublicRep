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

    //ダメージを与える判断
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("We have Hitted the Player");

            PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();

            if (!player.isAttacked)
            {
                other.gameObject.GetComponent<PlayerStatus>().TakenDamage(attackDamage);

                //敵がダメージ受けた後の後退
                Vector2 difference = other.transform.position - transform.position;
                other.transform.position = new Vector2(other.transform.position.x + difference.x / 4,
                                                       other.transform.position.y);
                //敵がダメージ受けた後の後退
            }
        }
    }
}
