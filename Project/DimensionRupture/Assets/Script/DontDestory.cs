using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestory : MonoBehaviour
{
    //-----------------------シーンチェンジの保存-----------------------//
    public static DontDestory instance;
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
}
