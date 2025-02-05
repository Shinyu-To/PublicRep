using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private BoxCollider2D box;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        box.enabled = true;
    }

    public void PlayerDeathCollider2D()
    {
        box.enabled = false;
    }
}
