using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Units/PawnSettings")]
public class SettingsPawn : ScriptableObject
{
    public float moveSpeed;
    public int damage;
    public int ID_Bullet;

}
