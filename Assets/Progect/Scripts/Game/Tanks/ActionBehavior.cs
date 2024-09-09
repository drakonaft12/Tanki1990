using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActionBehavior : MonoBehaviour
{
    int ID_Bullet;
    int _damage;
    Vector2 _move = Vector2.up;
    Spawner _spawner;
    GameObject _bullet;


    public Vector2 _Move { set { if(value!=Vector2.zero) _move = value; } }
    public int Damage {get { return _damage; } set { _damage = value; } }
    public void Create(SettingsPawn settings, Spawner spawner)
    {
        ID_Bullet = settings.ID_Bullet;
        _damage = settings.damage;
        _spawner = spawner;

    }

    public void Fire()
    {
        if (_bullet==null)
        {
        _bullet = _spawner.Spawn(ID_Bullet,transform.position+ (Vector3)_move*0.54f);
        _bullet.GetComponent<Bullet>().Create(_move, _damage,gameObject);
        }
    }
    public void Update()
    {
        if (_bullet != null && _bullet.activeSelf == false) { _bullet = null; }
    }

}
