using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    //-----------------------シーンチェンジの保存-----------------------//
    public static CameraFollow instance;
    //-----------------------シーンチェンジの保存-----------------------//
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
