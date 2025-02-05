using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    //-----------------------�V�[���`�F���W�̕ۑ�-----------------------//
    public static CameraFollow instance;
    //-----------------------�V�[���`�F���W�̕ۑ�-----------------------//
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate() 
    {
        if(target != null)
        {
            if(transform.position != target.position)
            {
                Vector3 targetPos = target.position;
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
