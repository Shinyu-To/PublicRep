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

    //20231201会話中、移動不能
    public bool canMove = true;

    //-----------------------シーンチェンジの保存-----------------------//
    public static PlayerController instance;//単列
    public string scenePassword;//シーンチェンジするためのパスワード（一致か確認）

    //-----------------------シーンチェンジの保存-----------------------//
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
    //-----------------------シーンチェンジの保存-----------------------//


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


 

    //地面チェック
    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
                   myFeet.IsTouchingLayers(LayerMask.GetMask("OneWay"));
                   myFeet.IsTouchingLayers(LayerMask.GetMask("SceneObject"));
    }

    //方向
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

    //移動
    void Run()
    {
        float moveDir = Input.GetAxisRaw("Horizontal");
        Vector2 playerVal = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVal;

        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", playerHasXAxisSpeed);
    }

    //ジャンプ
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

    //攻撃
    //void Attack()
    //{
    //    if (Input.GetButtonDown("Attack1") || Input.GetButtonDown("Attack2"))
    //    {
    //        myAnim.SetTrigger("Attack");
    //    }
    //}

    
    void SwitchAnimation()//アニメーション切り替え
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
