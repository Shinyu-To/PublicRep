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

    //ダメージを与える判断
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("We have Hitted the Enemy");

            EnemyStatus enemy = other.gameObject.GetComponent<EnemyStatus>();

            if (!enemy.isAttacked)
            {
                other.gameObject.GetComponent<EnemyStatus>().TakenDamage(attackDamage);

                //敵がダメージ受けた後の後退
                Vector2 difference = other.transform.position - transform.position;
                other.transform.position = new Vector2(other.transform.position.x + difference.x / 4,
                                                       other.transform.position.y);
                //敵がダメージ受けた後の後退
            }
        }
    }
}
