using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 _move;
    int _damage;
    float _velosity = 10;

    public void Create(Vector2 move, int damage)
    {
        _move = move.normalized;
        _damage = damage;
    }

    void Update()
    {
        var hit = Physics2D.Raycast(transform.position, _move, Time.deltaTime * _velosity, 1);
        if (hit)
        {
            var colliders = Physics2D.OverlapBoxAll(hit.point,new Vector2(0.9f,0.1f),_move.x!=0?90:0);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<IDamaget>(out var damaget))
                {
                    Debug.Log(1);
                    damaget.Damage(hit.point, -_move, _damage);
                }
                
            }
            gameObject.SetActive(false);
        }
        transform.position += (Vector3)_move * Time.deltaTime * _velosity;
    }
}
