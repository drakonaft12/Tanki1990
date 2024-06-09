using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour, IDamaget
{
    MoveBehavoir _moveBehavoir;
    ActionBehavior _actionBehavior;
    Spawner _spawner;
    int HP = 5;

    public Vector2 MovePawn { set { _moveBehavoir.MoveInput = value; _actionBehavior._Move = value; } }

    public void Fire()
    {
        _actionBehavior.Fire();
    }
    public void Craete(SettingsPawn settings, Spawner spawner)
    {
        _moveBehavoir = gameObject.AddComponent<MoveBehavoir>();
        _actionBehavior = gameObject.AddComponent<ActionBehavior>();

        _moveBehavoir.Create(settings);
        _actionBehavior.Create(settings, spawner);

        HP = 5;

        _spawner = spawner;
    }

    public void Damage(Vector2 pointDamage, Vector2 normal, int damage)
    {
        HP -= damage;
        if(HP<=0) gameObject.SetActive(false);
    }
}
