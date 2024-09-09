
using UnityEngine;

public interface IDamaget
{
    public int HP { get;}
    public void Damage(Vector2 pointDamage,Vector2 normal, int damage);
}