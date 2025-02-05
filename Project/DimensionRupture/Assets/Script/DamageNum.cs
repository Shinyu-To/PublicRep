using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageNum : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public float lifeTimer;//持続時間
    public float upSpeed;//上に移動する速度

    //-----------------------シーンチェンジの保存-----------------------//
    public static DamageNum instance;
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
