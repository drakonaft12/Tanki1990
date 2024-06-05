using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 _move;
    float _damage;
    float _velosity = 10;
    
    public void Create(Vector2 move, float damage)
    {
        _move = move;
        _damage = damage;
    }
    
    void Update()
    {
        var hit = Physics2D.Raycast(transform.position, _move, Time.deltaTime* _velosity,1);
        if (hit)
        {
            if(hit.collider.gameObject.TryGetComponent<IDamaget>(out var damaget))
            {
            damaget.Damage(transform.position, _damage);
            }
            gameObject.SetActive(false);
        }
        transform.position += (Vector3)_move*Time.deltaTime*_velosity;
    }
}
