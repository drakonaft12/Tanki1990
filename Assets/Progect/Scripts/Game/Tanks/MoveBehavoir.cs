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
    public float MoveSpeed { set { _moveSpeed = value; } }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Create(SettingsPawn settings)
    {
        _moveSpeed = settings.moveSpeed;
        _rotation = transform.rotation = Quaternion.identity;
        _position = transform.position;
    }


    private void MoveCalculate()
    {
        var vector = _moveInput.normalized;
        _position = vector * _moveSpeed * 0.5f;
        if (vector != Vector2.zero)
            _rotation.eulerAngles = new Vector3(0, 0, 90 * -vector.x - (vector.y - Mathf.Abs(vector.y)) * 90);
    }

    private void FixedUpdate()
    {
        MoveCalculate();
        _rb.MovePosition(transform.position + (Vector3)_position * Time.fixedDeltaTime);
        _position = Vector2.zero;
        transform.rotation = _rotation;
    }
}
