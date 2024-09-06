using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Units/PawnSettings")]
public class SettingsPawn : ScriptableObject
{
    public Sprite tankSprite;
    public float moveSpeed = 5;
    public int damage = 5;
    public int HP = 5;
    public int ID_Bullet = 0;
    public Vector2 colliderSize = new Vector2(0.7f, 0.7f);

}
