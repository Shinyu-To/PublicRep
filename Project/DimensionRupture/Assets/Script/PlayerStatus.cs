using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    #region player status
    //-------------------------�X�e�[�^�X-------------------------//
    [Header("Player Status")]
    public string playerName, description;

    public int playerLevel, maxLevel;//���̃��x���A�ő僌�x��
    public int currentExp;//���̌o���l
    public int[] nextLevelExp;//���̃��x���܂łɕK�v�Ȍo���l

    public int maxHP, VIT, STR;
    public float currentHP;
    //�L�����N�^�[�̃X�e�[�^�X�FHP�A�ő�HP�AVIT�i�����́j�ASTR�i�p���[�j
    public int ATK, ARM;
    //�L�����N�^�[��ATK�i�U���́j�AARM�i�A�[�}�[�l�j
    public int currentSP;//�X�L���|�C���g

    public float hpRecovery;//HP������
    //-------------------------�X�e�[�^�X-------------------------//

    public Text hpDisplayText, lvDisplayText, spDisplayText;
    public GameObject[] gameObjectWithAnimators;
    private int curHp, preHp, curLv, preLv, curSp, preSp;//���݂̃f�[�^�ƑO�̃��x���̃f�[�^
    //-------------------------�X�e�[�^�X-------------------------//
    #endregion

    #region damage hit
    //---------------�_���[�W�󂯂���̐F�ω�--------//
    [Header("HurtColor")]
    private SpriteRenderer sp;
    public float hurtLength;//�_���[�W�󂯂���̐F�ω��̎�������
    private float hurtCounter;//�v�Z
    //---------------�_���[�W�󂯂���̐F�ω�--------//

    //-------------------------
    [HideInInspector]
    public bool isAttacked;//���U�����畡����̃_���[�W���󂯂�̋֎~
    //public GameObject explosionEffect;//���S����Ƃ��̃G�t�F�N�g
    //-------------------------
    #endregion

    #region player death
    //���S
    private Animator anim;
    private Rigidbody2D rb;
    //���G����
    //public float invisibleTime;
    //private CapsuleCollider2D cap;
    #endregion


    private void Start()//Awake��Start���D�揇�ʂ�����
    {
        nextLevelExp = new int[maxLevel + 1];
        //�O���[�v�ł̃��x���v�Z��0�ł͂Ȃ��A1����v�Z���邩��A���ۂ̍ő僌�x���@���@�������ő僌�x���@-�@1

        nextLevelExp[0] = 100;

        for (int i = 1; i < maxLevel; i++)
        {
            nextLevelExp[i] = Mathf.RoundToInt(nextLevelExp[i - 1] * 1.05f);
            //���̃��x���܂ł̌o���l�̏�Z
        }

        //�Q�[���J�n���̃X�e�[�^�X�ω��̃��Z�b�g
        curHp = maxHP;
        preHp = 0;
        curLv = playerLevel;
        curSp = currentSP;
        preLv = 0;
        preSp = 0;
        //�Q�[���J�n���̃X�e�[�^�X�ω��̃��Z�b�g

        //---------------�_���[�W�󂯂���̐F�ω�--------//
        sp = GetComponent<SpriteRenderer>();
        //---------------�_���[�W�󂯂���̐F�ω�--------//

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //���G����
        //cap = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        //---------------�_���[�W�󂯂���̐F�ω�--------//
        if (hurtCounter <= 0)
        {
            sp.material.SetFloat("_FlashAmount", 0);
        }
        else
        {
            hurtCounter -= Time.deltaTime;
        }
        //---------------�_���[�W�󂯂���̐F�ω�--------//


        //----------------�o���l�e�X�g----------------//
        if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.JoystickButton5))//�o���lUP����{�_�� 5 = RB
        {
            AddExp(50);//���s
        }
 

        //----------------�̗͉񕜃e�X�g--------------//
        if (currentHP < maxHP && Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton8))//HP�񕜂���{�_���@L Stick (��������)
        {
            currentHP = maxHP;
            GetComponent<HealthBar>().hp = maxHP;
            GetComponent<HealthBar>().UpdateHp();
        }
    }

    private void FixedUpdate()
    {
        //----------------����HP��----------------//
        if (currentHP < maxHP)
        {
            currentHP = currentHP + hpRecovery;
            GetComponent<HealthBar>().hp = currentHP;
            GetComponent<HealthBar>().UpdateHp();
        }
        //----------------����HP��----------------//
    }

    public void AddExp(int amount)//(int amount)���ꂼ��|�����G�̌o���l
    {
        currentExp += amount;//�o���l����

        //���x���A�b�v��̃X�e�[�^�X�ω��̒l�̃��Z�b�g
        preHp = curHp;
        preLv = curLv;
        preSp = curSp;
        //���x���A�b�v��̃X�e�[�^�X�ω��̒l�̃��Z�b�g

        if (currentExp >= nextLevelExp[playerLevel] && playerLevel < maxLevel)
        {//���̌o���l�������̃��x���܂ł̌o���@���@�v���C���[���x���@���@�ő僌�x���̏ꍇ
            LevelUp();
        }

        if (playerLevel > 100)
        {
            nextLevelExp[playerLevel] = 12896;
        }

        if (playerLevel >= maxLevel)
        {
            currentExp = 0;
        }

        FindObjectOfType<UIManager>().UpdatePlayerStatus();
    }

    private void LevelUp()
    {
        currentExp -= nextLevelExp[playerLevel];//���̌o���l�@���@���̌o���l�@�|�@���̃��x���܂ł̌o���l
        playerLevel++;//�v���C���[���x���@+�@1

        //���x���A�b�v���̃X�e�[�^�X�̕ω�
        maxHP += 1;
        currentHP += 1;
        currentSP += 1;
        //���x���A�b�v���̃X�e�[�^�X�̕ω�


        //���x���A�b�v���̃X�e�[�^�X�̃A�j���[�V����
        curHp = maxHP;//�X�e�[�^�X�̃A�b�v�f�[�g
        curLv = playerLevel;
        curSp = currentSP;

        MakingAnimation();
    }

    private void MakingAnimation()
    {
        for (int i = 0; i < gameObjectWithAnimators.Length; i++)
        {//�X�e�[�^�X�ω��̌v�Z
            hpDisplayText.text = "+" + (curHp - preHp);
            lvDisplayText.text = "+" + (curLv - preLv);
            spDisplayText.text = "+" + (curSp - preSp);

            gameObjectWithAnimators[i].GetComponent<Animator>().SetTrigger("Levelup");
            //�A�j���[�V�����̌Ăяo��
        }
    }

    //----------�_���[�W���󂯂�
    public void TakenDamage(int attackDamage)
    {
        isAttacked = true;//��񂾂��_���[�W��������
        StartCoroutine(isAttackCo());
        currentHP -= attackDamage;

        //20231221HP�P�[�W�ω�
        GetComponent<HealthBar>().hp -= attackDamage;
        GetComponent<HealthBar>().UpdateHp();
        //20231221HP�P�[�W�ω�

        HurtShader();

        if (currentHP <= 0)
        {
            anim.SetTrigger("Death");//���S�A�j���[�V����
            rb.bodyType = RigidbodyType2D.Static;//���S��Rigidbody2D���ÓI
            gameObject.GetComponent<PlayerDeath>().PlayerDeathCollider2D();
            Destroy(gameObject, 5f);//5�b��player��j�󂷂�
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }

    //---------------�_���[�W�󂯂���̐F�ω�--------// 
    private void HurtShader()
    {
        sp.material.SetFloat("_FlashAmount", 1);
        hurtCounter = hurtLength;
    }
    //---------------�_���[�W�󂯂���̐F�ω�--------//

    IEnumerator isAttackCo()
    {
        yield return new WaitForSeconds(0.2f);
        isAttacked = false;
    }

    private void Restart()//�V�[���̃��Z�b�g
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //pplication.LoadLevel("Menu");
        Application.Quit();
    }
}