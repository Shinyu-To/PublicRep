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

    //---------------ダメージ受けた後の色変化--------//
    [Header("HurtColor")]
    private SpriteRenderer sp;
    public float hurtLength;//ダメージ受けた後の色変化の持続時間
    private float hurtCounter;//計算
    //---------------ダメージ受けた後の色変化--------//

    [HideInInspector]
    public bool isAttacked;//敵が一回攻撃から複数回のダメージを受けるの禁止
    public GameObject explosionEffect;//敵が死亡するときのエフェクト

    private void Start()
    {
        //ゲーム開始時のステータス変化のリセット
        currentHP = maxHP;
        //ゲーム開始時のステータス変化のリセット

        //---------------ダメージ受けた後の色変化--------//
        sp = GetComponent<SpriteRenderer>();
        //---------------ダメージ受けた後の色変化--------//
    }

    private void Update()
    {
        //---------------ダメージ受けた後の色変化--------//
        if (hurtCounter <= 0)
        {
            sp.material.SetFloat("_FlashAmount", 0);
        }
        else
        {
            hurtCounter -= Time.deltaTime;
        }
        //---------------ダメージ受けた後の色変化--------//
    }

    //ダメージを受ける
    public void TakenDamage(int attackDamage)
    {
        isAttacked = true;//一回だけダメージをうける
        StartCoroutine(isAttackCo());
        currentHP -= attackDamage;
        HurtShader();

        if (currentHP <= 0)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);//死亡エフェクト
            Destroy(gameObject);
        }
    }

    //---------------ダメージ受けた後の色変化--------// 
    private void HurtShader()
    {
        sp.material.SetFloat("_FlashAmount", 1);
        hurtCounter = hurtLength;
    }
    //---------------ダメージ受けた後の色変化--------//

    IEnumerator isAttackCo()
    {
        yield return new WaitForSeconds(0.2f);
        isAttacked = false;
    }
}
