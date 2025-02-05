using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //-------------------------�X�e�[�^�X�e�L�X�g-------------------------//

    public PlayerStatus playerStatus;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText, nextExpText;
    public TextMeshProUGUI maxHpText, vitText, strText, atkText, armText;
    public TextMeshProUGUI spText;

    //-------------------------�X�e�[�^�X�e�L�X�g-------------------------//


    //-------------------------�o���l�X���C�h-------------------------//
    public Slider expSliderl;
    public TextMeshProUGUI sliderText;
    //-------------------------�o���l�X���C�h-------------------------//

    //-------------------------GoldText-------------------------//
    
    //-------------------------GoldText-------------------------//


    //-----------------------�V�[���`�F���W�̕ۑ�-----------------------//
    public static UIManager instance;
    //-----------------------�V�[���`�F���W�̕ۑ�-----------------------//
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
    //-----------------------�V�[���`�F���W�̕ۑ�-----------------------//


    private void Start()
    {
        UpdatePlayerStatus();//�Q�[���J�����Ƃ���񂾂����s����
        
    }

    public void UpdatePlayerStatus()
    {
        //-------------------------�X�e�[�^�X�e�L�X�g-------------------------//

        //�X�e�[�^�X�e�L�X�g�ɂ��ꂼ��l��^����
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

        //-------------------------�X�e�[�^�X�e�L�X�g-------------------------//

        //-------------------------�o���l�X���C�h-------------------------//

        expSliderl.value = playerStatus.currentExp;
        expSliderl.maxValue = playerStatus.nextLevelExp[playerStatus.playerLevel];

        sliderText.text = playerStatus.currentExp + "/" + playerStatus.nextLevelExp[playerStatus.playerLevel];

        //-------------------------�o���l�X���C�h-------------------------//

    }

}
