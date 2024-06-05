using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveBehavoir : MonoBehaviour
{
    float _moveSpeed;
    Vector2 _position;
    Vector2 _moveInput;
    Rigidbody2D _rb;
    public Vector2 MoveInput { set { _moveInput = value; } }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Create(SettingsPawn settings)
    {
        _moveSpeed = settings.moveSpeed;
        _position = transform.position;
        Move();
    }

    private void Move()
    {
        Thread thread = new Thread(MoveCalculate);
        thread.Start();
    }

    private void MoveCalculate()
    {
        while (true)
        {
            _position = _moveInput.normalized * _moveSpeed * 0.01f; Thread.Sleep(10);
        }
    }

    private void Update()
    {
        _rb.MovePosition(transform.position + (Vector3)_position);
        _position = Vector2.zero;
    }
}
