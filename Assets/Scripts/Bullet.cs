using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamaget
{
    Vector2 _move;
    int _damage;
    float _velosity = 5;

    bool isDestroi = false;

    public void Create(Vector2 move, int damage)
    {
        isDestroi = false;
        _move = move.normalized;
        _damage = damage;
    }

    public void Damage(Vector2 pointDamage, Vector2 normal, int damage)
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        RaycastHit2D[] rrt = new RaycastHit2D[2];
        var hitcount = gameObject.GetComponent<CircleCollider2D>().Raycast(_move, rrt, Time.deltaTime * _velosity, 1 | 1 << 6 | 1 << 7 | 1 << 8);
        if (hitcount != 0)
        {
            
            foreach (var hit in rrt)
            {
                
                if (hit.collider.gameObject != this)
                {
                    Debug.Log(hit.collider.gameObject.layer);
                    var colliders = Physics2D.OverlapBoxAll(hit.point, new Vector2(0.9f, 0.1f), _move.x != 0 ? 90 : 0);
                    foreach (var collider in colliders)
                    {
                        if (collider.gameObject.TryGetComponent<IDamaget>(out var damaget))
                        {
                            damaget.Damage(hit.point, -_move, _damage);
                            isDestroi = true;
                        }

                    }
                    break;
                }
            }

        }
        transform.position += (Vector3)_move * Time.deltaTime * _velosity;

        if (transform.position.magnitude > 20) isDestroi = true;
        if (isDestroi)
            gameObject.SetActive(false);
    }
}
