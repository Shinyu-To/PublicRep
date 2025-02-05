using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class EnemyDrop : ScriptableObject
{
    public Sprite DropList;
    public string DropName;
    public int DropChance;

    public EnemyDrop(string DropName, int DropChance)
    {
        this.DropName = DropName;
        this.DropChance = DropChance;
    }
}
