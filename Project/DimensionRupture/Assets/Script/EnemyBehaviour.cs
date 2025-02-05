using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;//�ː��Ɍ��_
    public LayerMask raycastMask;//�ː��̃��C���[
    public float rayCastLength;//�ː��̒���
    public float attackDistance;//�U���𔭓����邽�߂ɕK�v�ȍŒZ����
    public float moveSpeed;//�ړ����x
    public float timer;//�U���̃N�[���_�E��
    public Transform leftLimit;//�G�l�~�[�p�g���[���͈̔�-����
    public Transform rightLimit;//�G�l�~�[�p�g���[���͈̔�-�E��
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance;//�G�ƃv���C���[�Ƃ̋���
    private bool attackMode;//�G���U�����[�h�ɓ��邩�ǂ���
    private bool inRange;//�v���C���[���˒��������ɂ��邩�ǂ���
    private bool cooling;//�U����ɃN�[���_�E���ɓ���
    private float intTimer;//�^�C�}�[
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
            RaycastDebugger();//�V�[���Ŏː���\��
        }


        //-------------------�v���C���[��T�m������------------------------------//
        //
        if(hit.collider != null)//�v���C���[��collider��T�m�����ꍇ
        {
            EnemyLogic();//�G�l�~�[�̍s��
        }
        else if(hit.collider == null)//�v���C���[��collider��T�m���Ȃ������ꍇ
        {
            inRange = false;//�͈͓��ɂȂ�
        }

        if(inRange == false)//�͈͓��ɉ����Ȃ��ꍇ
        {        
            StopAttack();//�G�l�~�[�̍U������~
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

    void EnemyLogic()//�G�l�~�[�̍s��
    {
        distance = Vector2.Distance(transform.position, target.position);//�G�l�~�[�ƃv���C���[�̋�����distance�ɋL�^����
        
        if(distance > attackDistance)
        {//�G�l�~�[�ƃv���C���[�̋����@���@�G�l�~�[�̎˒������@���@�傫���ꍇ
            StopAttack();//�G�l�~�[���ړ����鎞�A�U������~
        }
        else if(attackDistance >= distance�@&& cooling == false)
        {//�G�l�~�[�̎˒������@���@�G�l�~�[�ƃv���C���[�̋����@���@�傫���ꍇ �܂��@�N�[���_�E���ł͂Ȃ��ꍇ
            Attack();//�U��
        }

        if (cooling)//�N�[���_�E����
        {
            Cooldown();
            anim.SetBool("Attack", false);//�U���A�j������~
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
        timer = intTimer;//�^�C�}�[�����Z�b�g
        attackMode = true;//�U�����[�h���I���ɂ���

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
        if(distance > attackDistance)//�G���v���C���[�Ƃ̋������˒��������傫���ꍇ
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if (attackDistance > distance)//�˒��������v���C���[�Ƃ̋������傫���ꍇ
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
 