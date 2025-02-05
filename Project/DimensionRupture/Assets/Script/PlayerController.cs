using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public Animator myAnim;

    private Rigidbody2D myRigidbody;
    private CircleCollider2D myFeet;
    private bool isGround;

    //20231201��b���A�ړ��s�\
    public bool canMove = true;

    //-----------------------�V�[���`�F���W�̕ۑ�-----------------------//
    public static PlayerController instance;//�P��
    public string scenePassword;//�V�[���`�F���W���邽�߂̃p�X���[�h�i��v���m�F�j

    //-----------------------�V�[���`�F���W�̕ۑ�-----------------------//
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    //-----------------------�V�[���`�F���W�̕ۑ�-----------------------//


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.isGameAlive)
        {
            Flip();
            Run();
            Jump();
            //Attack();
            CheckGrounded();
            SwitchAnimation();
        }

    }


 

    //�n�ʃ`�F�b�N
    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
                   myFeet.IsTouchingLayers(LayerMask.GetMask("OneWay"));
                   myFeet.IsTouchingLayers(LayerMask.GetMask("SceneObject"));
    }

    //����
    void Flip()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (myRigidbody.velocity.x < 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    //�ړ�
    void Run()
    {
        float moveDir = Input.GetAxisRaw("Horizontal");
        Vector2 playerVal = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVal;

        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", playerHasXAxisSpeed);
    }

    //�W�����v
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
            }
        }
    }


    //

    //�U��
    //void Attack()
    //{
    //    if (Input.GetButtonDown("Attack1") || Input.GetButtonDown("Attack2"))
    //    {
    //        myAnim.SetTrigger("Attack");
    //    }
    //}

    
    void SwitchAnimation()//�A�j���[�V�����؂�ւ�
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Down", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("Down", false);
            myAnim.SetBool("Idle", true);
        }
    }
}
