using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    //------------Enemy Status---------------//
    [Header("Enemy Status")]
    public string enemyName, description;
    public int enemyLevel;
    public int currentHP, maxHP, VIT, STR;
    public int ATK, ARM;
    public int deathEXP;
    //------------Enemy Status---------------//

    //---------------�_���[�W�󂯂���̐F�ω�--------//
    [Header("HurtColor")]
    private SpriteRenderer sp;
    public float hurtLength;//�_���[�W�󂯂���̐F�ω��̎�������
    private float hurtCounter;//�v�Z
    //---------------�_���[�W�󂯂���̐F�ω�--------//

    [HideInInspector]
    public bool isAttacked;//�G�����U�����畡����̃_���[�W���󂯂�̋֎~
    public GameObject explosionEffect;//�G�����S����Ƃ��̃G�t�F�N�g

    private void Start()
    {
        //�Q�[���J�n���̃X�e�[�^�X�ω��̃��Z�b�g
        currentHP = maxHP;
        //�Q�[���J�n���̃X�e�[�^�X�ω��̃��Z�b�g

        //---------------�_���[�W�󂯂���̐F�ω�--------//
        sp = GetComponent<SpriteRenderer>();
        //---------------�_���[�W�󂯂���̐F�ω�--------//
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
    }

    //�_���[�W���󂯂�
    public void TakenDamage(int attackDamage)
    {
        isAttacked = true;//��񂾂��_���[�W��������
        StartCoroutine(isAttackCo());
        currentHP -= attackDamage;
        HurtShader();

        if (currentHP <= 0)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);//���S�G�t�F�N�g
            Destroy(gameObject);
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
}
