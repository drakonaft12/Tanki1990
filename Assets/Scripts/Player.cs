using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    MoveBehavoir _moveBehavoir;
    ActionBehavior _actionBehavior;
    Spawner _spawner;

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

        _spawner = spawner;
    }
}
