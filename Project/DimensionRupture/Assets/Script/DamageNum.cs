using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageNum : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public float lifeTimer;//��������
    public float upSpeed;//��Ɉړ����鑬�x

    //-----------------------�V�[���`�F���W�̕ۑ�-----------------------//
    public static DamageNum instance;
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
        Destroy(gameObject, lifeTimer);
    }

    private void Update()
    {
        transform.position += new Vector3(0, upSpeed * Time.deltaTime, 0);
    }

    public void ShowUIDamage(int _amount)
    {
        damageText.text = _amount.ToString();
    }

}
