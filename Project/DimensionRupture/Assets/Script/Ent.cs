using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : MonoBehaviour
{
    public string entrancePassword;

    private void Start()
    {
        if(PlayerController.instance.scenePassword == entrancePassword)
        {
            PlayerController.instance.transform.position = transform.position;
            Debug.Log("ENTER!");
        }
        else
        {
            Debug.Log("WRONG PASSWORD!");
        }
    }
}
