using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Tank : MonoBehaviour, IDamaget
{
    MoveBehavoir _moveBehavoir;
    ActionBehavior _actionBehavior;
    BoxCollider2D boxCollider2D;
    Spawner _spawner;
    Action action;
    int HP = 5;
    float time = 0;

    public Vector2 MovePawn { set { _moveBehavoir.MoveInput = value; _actionBehavior._Move = value; } }
    public ActionBehavior ActionBehavior => _actionBehavior;

    int IDamaget.HP => HP;

    public void Fire()
    {
        _actionBehavior.Fire();
    }

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    public void Craete(SettingsPawn settings, Spawner spawner)
    {
        _moveBehavoir = gameObject.AddComponent<MoveBehavoir>();
        _actionBehavior = gameObject.AddComponent<ActionBehavior>();

        _moveBehavoir.Create(settings);
        _actionBehavior.Create(settings, spawner);

        HP = settings.HP;

        boxCollider2D.size = settings.colliderSize;

        _spawner = spawner;


    }

    private void Update()
    {
        if (action != null)
        {
            action.Invoke();
        }
    }

    public void Damage(Vector2 pointDamage, Vector2 normal, int damage)
    {
        HP -= damage;
        if (HP <= 0) gameObject.SetActive(false);
    }


    public void SetBonusAction(int ID)
    {
        time = 15;
        switch (ID)
        {
            case 0:
                action = () =>
            {
                if (time > 0)
                {
                    time -= Time.deltaTime;
                    _moveBehavoir.MoveSpeed = 10;
                }
                else
                {
                    _moveBehavoir.MoveSpeed = 5;
                    action = null;
                }
            };
                break;
            case 1:
                action = () =>
                {
                    if (time > 0)
                    {
                        time -= Time.deltaTime;
                        _actionBehavior.Damage = 25;
                    }
                    else
                    {
                        _actionBehavior.Damage = 5;
                        action = null;
                    }
                };
                break;

        }
    }



}
