using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    #region player status
    //-------------------------ステータス-------------------------//
    [Header("Player Status")]
    public string playerName, description;

    public int playerLevel, maxLevel;//今のレベル、最大レベル
    public int currentExp;//今の経験値
    public int[] nextLevelExp;//次のレベルまでに必要な経験値

    public int maxHP, VIT, STR;
    public float currentHP;
    //キャラクターのステータス：HP、最大HP、VIT（生命力）、STR（パワー）
    public int ATK, ARM;
    //キャラクターのATK（攻撃力）、ARM（アーマー値）
    public int currentSP;//スキルポイント

    public float hpRecovery;//HP自動回復
    //-------------------------ステータス-------------------------//

    public Text hpDisplayText, lvDisplayText, spDisplayText;
    public GameObject[] gameObjectWithAnimators;
    private int curHp, preHp, curLv, preLv, curSp, preSp;//現在のデータと前のレベルのデータ
    //-------------------------ステータス-------------------------//
    #endregion

    #region damage hit
    //---------------ダメージ受けた後の色変化--------//
    [Header("HurtColor")]
    private SpriteRenderer sp;
    public float hurtLength;//ダメージ受けた後の色変化の持続時間
    private float hurtCounter;//計算
    //---------------ダメージ受けた後の色変化--------//

    //-------------------------
    [HideInInspector]
    public bool isAttacked;//一回攻撃から複数回のダメージを受けるの禁止
    //public GameObject explosionEffect;//死亡するときのエフェクト
    //-------------------------
    #endregion

    #region player death
    //死亡
    private Animator anim;
    private Rigidbody2D rb;
    //無敵時間
    //public float invisibleTime;
    //private CapsuleCollider2D cap;
    #endregion


    private void Start()//AwakeはStartより優先順位が高い
    {
        nextLevelExp = new int[maxLevel + 1];
        //グループでのレベル計算は0ではなく、1から計算するから、実際の最大レベル　＝　書いた最大レベル　-　1

        nextLevelExp[0] = 100;

        for (int i = 1; i < maxLevel; i++)
        {
            nextLevelExp[i] = Mathf.RoundToInt(nextLevelExp[i - 1] * 1.05f);
            //次のレベルまでの経験値の乗算
        }

        //ゲーム開始時のステータス変化のリセット
        curHp = maxHP;
        preHp = 0;
        curLv = playerLevel;
        curSp = currentSP;
        preLv = 0;
        preSp = 0;
        //ゲーム開始時のステータス変化のリセット

        //---------------ダメージ受けた後の色変化--------//
        sp = GetComponent<SpriteRenderer>();
        //---------------ダメージ受けた後の色変化--------//

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //無敵時間
        //cap = GetComponent<CapsuleCollider2D>();
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


        //----------------経験値テスト----------------//
        if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.JoystickButton5))//経験値UPするボダン 5 = RB
        {
            AddExp(50);//実行
        }
 

        //----------------体力回復テスト--------------//
        if (currentHP < maxHP && Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton8))//HP回復するボダン　L Stick (押し込み)
        {
            currentHP = maxHP;
            GetComponent<HealthBar>().hp = maxHP;
            GetComponent<HealthBar>().UpdateHp();
        }
    }

    private void FixedUpdate()
    {
        //----------------自動HP回復----------------//
        if (currentHP < maxHP)
        {
            currentHP = currentHP + hpRecovery;
            GetComponent<HealthBar>().hp = currentHP;
            GetComponent<HealthBar>().UpdateHp();
        }
        //----------------自動HP回復----------------//
    }

    public void AddExp(int amount)//(int amount)それぞれ倒した敵の経験値
    {
        currentExp += amount;//経験値増加

        //レベルアップ後のステータス変化の値のリセット
        preHp = curHp;
        preLv = curLv;
        preSp = curSp;
        //レベルアップ後のステータス変化の値のリセット

        if (currentExp >= nextLevelExp[playerLevel] && playerLevel < maxLevel)
        {//今の経験値＞＝次のレベルまでの経験　か　プレイヤーレベル　＜　最大レベルの場合
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
        currentExp -= nextLevelExp[playerLevel];//今の経験値　＝　今の経験値　－　次のレベルまでの経験値
        playerLevel++;//プレイヤーレベル　+　1

        //レベルアップ時のステータスの変化
        maxHP += 1;
        currentHP += 1;
        currentSP += 1;
        //レベルアップ時のステータスの変化


        //レベルアップ時のステータスのアニメーション
        curHp = maxHP;//ステータスのアップデート
        curLv = playerLevel;
        curSp = currentSP;

        MakingAnimation();
    }

    private void MakingAnimation()
    {
        for (int i = 0; i < gameObjectWithAnimators.Length; i++)
        {//ステータス変化の計算
            hpDisplayText.text = "+" + (curHp - preHp);
            lvDisplayText.text = "+" + (curLv - preLv);
            spDisplayText.text = "+" + (curSp - preSp);

            gameObjectWithAnimators[i].GetComponent<Animator>().SetTrigger("Levelup");
            //アニメーションの呼び出し
        }
    }

    //----------ダメージを受ける
    public void TakenDamage(int attackDamage)
    {
        isAttacked = true;//一回だけダメージをうける
        StartCoroutine(isAttackCo());
        currentHP -= attackDamage;

        //20231221HPケージ変化
        GetComponent<HealthBar>().hp -= attackDamage;
        GetComponent<HealthBar>().UpdateHp();
        //20231221HPケージ変化

        HurtShader();

        if (currentHP <= 0)
        {
            anim.SetTrigger("Death");//死亡アニメーション
            rb.bodyType = RigidbodyType2D.Static;//死亡後Rigidbody2Dが静的
            gameObject.GetComponent<PlayerDeath>().PlayerDeathCollider2D();
            Destroy(gameObject, 5f);//5秒後playerを破壊する
        }
        else
        {
            anim.SetTrigger("Hit");
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

    private void Restart()//シーンのリセット
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //pplication.LoadLevel("Menu");
        Application.Quit();
    }
}