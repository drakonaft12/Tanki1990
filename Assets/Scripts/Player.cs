using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    MoveBehavoir _moveBehavoir;
    public Vector2 MovePawn { set => _moveBehavoir.MoveInput = value; }

    public void Craete(SettingsPawn settings)
    {
        _moveBehavoir = gameObject.AddComponent<MoveBehavoir>();
        _moveBehavoir.Create(settings);

    }
}
