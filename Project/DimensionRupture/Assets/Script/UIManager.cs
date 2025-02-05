using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //-------------------------ステータステキスト-------------------------//

    public PlayerStatus playerStatus;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText, nextExpText;
    public TextMeshProUGUI maxHpText, vitText, strText, atkText, armText;
    public TextMeshProUGUI spText;

    //-------------------------ステータステキスト-------------------------//


    //-------------------------経験値スライド-------------------------//
    public Slider expSliderl;
    public TextMeshProUGUI sliderText;
    //-------------------------経験値スライド-------------------------//

    //-------------------------GoldText-------------------------//
    
    //-------------------------GoldText-------------------------//


    //-----------------------シーンチェンジの保存-----------------------//
    public static UIManager instance;
    //-----------------------シーンチェンジの保存-----------------------//
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    //-----------------------シーンチェンジの保存-----------------------//


    private void Start()
    {
        UpdatePlayerStatus();//ゲーム開いたとき一回だけ実行する
        
    }

    public void UpdatePlayerStatus()
    {
        //-------------------------ステータステキスト-------------------------//

        //ステータステキストにそれぞれ値を与える
        nameText.text = playerStatus.playerName;

        levelText.text = playerStatus.playerLevel.ToString();
        maxHpText.text = playerStatus.maxHP.ToString();
        expText.text = playerStatus.currentExp.ToString();
        vitText.text = playerStatus.VIT.ToString();
        strText.text = playerStatus.STR.ToString();
        atkText.text = playerStatus.ATK.ToString();
        armText.text = playerStatus.ARM.ToString();
        spText.text = playerStatus.currentSP.ToString();
        nextExpText.text = playerStatus.nextLevelExp[playerStatus.playerLevel].ToString();

        //-------------------------ステータステキスト-------------------------//

        //-------------------------経験値スライド-------------------------//

        expSliderl.value = playerStatus.currentExp;
        expSliderl.maxValue = playerStatus.nextLevelExp[playerStatus.playerLevel];

        sliderText.text = playerStatus.currentExp + "/" + playerStatus.nextLevelExp[playerStatus.playerLevel];

        //-------------------------経験値スライド-------------------------//

    }

}
