using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActionBehavior : MonoBehaviour
{
    int ID_Bullet;
    float _damage;
    Vector2 _move = Vector2.up;
    Spawner _spawner;

    public Vector2 _Move { set { if(value!=Vector2.zero) _move = value; } }
    public void Create(SettingsPawn settings, Spawner spawner)
    {
        ID_Bullet = settings.ID_Bullet;
        _damage = settings.damage;
        _spawner = spawner;
    }

    public void Fire()
    {
        _spawner.Spawn(ID_Bullet,transform.position+ (Vector3)_move).GetComponent<Bullet>().Create(_move,_damage);
    }

}
