using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;//射線に原点
    public LayerMask raycastMask;//射線のレイヤー
    public float rayCastLength;//射線の長さ
    public float attackDistance;//攻撃を発動するために必要な最短距離
    public float moveSpeed;//移動速度
    public float timer;//攻撃のクールダウン
    public Transform leftLimit;//エネミーパトロールの範囲-左側
    public Transform rightLimit;//エネミーパトロールの範囲-右側
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance;//敵とプレイヤーとの距離
    private bool attackMode;//敵が攻撃モードに入るかどうか
    private bool inRange;//プレイヤーが射程距離内にいるかどうか
    private bool cooling;//攻撃後にクールダウンに入る
    private float intTimer;//タイマー
    #endregion

    private void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if(!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("enm001-attack"))
        {
            SelectTarget();
        }


        if(inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, raycastMask);
            RaycastDebugger();//シーンで射線を表示
        }


        //-------------------プレイヤーを探知した時------------------------------//
        //
        if(hit.collider != null)//プレイヤーのcolliderを探知した場合
        {
            EnemyLogic();//エネミーの行為
        }
        else if(hit.collider == null)//プレイヤーのcolliderを探知しなかった場合
        {
            inRange = false;//範囲内になし
        }

        if(inRange == false)//範囲内に何もない場合
        {        
            StopAttack();//エネミーの攻撃が停止
        }

    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            target = trig.transform;
            inRange = true;
            Flip();
        }
    } 

    void EnemyLogic()//エネミーの行為
    {
        distance = Vector2.Distance(transform.position, target.position);//エネミーとプレイヤーの距離をdistanceに記録する
        
        if(distance > attackDistance)
        {//エネミーとプレイヤーの距離　が　エネミーの射程距離　より　大きい場合
            StopAttack();//エネミーが移動する時、攻撃が停止
        }
        else if(attackDistance >= distance　&& cooling == false)
        {//エネミーの射程距離　が　エネミーとプレイヤーの距離　より　大きい場合 また　クールダウンではない場合
            Attack();//攻撃
        }

        if (cooling)//クールダウン中
        {
            Cooldown();
            anim.SetBool("Attack", false);//攻撃アニメが停止
        }
    }

    void Move()
    {
        anim.SetBool("canRun", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("enm001-attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;//タイマーをリセット
        attackMode = true;//攻撃モードをオンにする

        anim.SetBool("canRun", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    void RaycastDebugger()
    {
        if(distance > attackDistance)//敵がプレイヤーとの距離が射程距離より大きい場合
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if (attackDistance > distance)//射程距離がプレイヤーとの距離より大きい場合
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }
     
    public void TriggerCooling()
    {
        cooling = true;
    }
    
    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }
}
 