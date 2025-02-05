using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestory : MonoBehaviour
{
    //-----------------------�V�[���`�F���W�̕ۑ�-----------------------//
    public static DontDestory instance;
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
}
