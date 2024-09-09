using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BazaPlayer : MonoBehaviour, IDamaget
{
    private int HP = 5;

    int IDamaget.HP { get => HP; }

    public void Damage(Vector2 pointDamage, Vector2 normal, int damage)
    {
        HP -= damage;
        if (HP <= 0) EndGame();
    }

    private void EndGame()
    {
        Debug.Log("GAME OVER");
        SceneManager.LoadScene("Menu");
    }

}
