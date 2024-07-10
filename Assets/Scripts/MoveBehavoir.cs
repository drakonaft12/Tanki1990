using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveBehavoir : MonoBehaviour
{
    float _moveSpeed;
    Quaternion _rotation;
    Vector2 _position;
    Vector2 _moveInput;
    Rigidbody2D _rb;
    public Vector2 MoveInput { set { _moveInput = value; } }
    public float MoveSpeed {  set { _moveSpeed = value; } }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Create(SettingsPawn settings)
    {
        _moveSpeed = settings.moveSpeed;
        _rotation = transform.rotation = Quaternion.identity;
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
            var vector = _moveInput.normalized;
            _position = vector * _moveSpeed * 0.01f; Thread.Sleep(10);
            if(vector!=Vector2.zero)
            _rotation.SetEulerAngles(0, 0, (90 * -vector.x - (vector.y- Mathf.Abs(vector.y))*90)*Mathf.Deg2Rad);
        }
    }

    private void Update()
    {
        _rb.MovePosition(transform.position + (Vector3)_position);
        _position = Vector2.zero;
        transform.rotation = _rotation;
    }
}
