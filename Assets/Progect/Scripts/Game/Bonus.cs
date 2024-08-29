using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class Bonus : MonoBehaviour
{
    int ID;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public void Create()
    {
        ID = Random.Range(0,2);
        if(ID == 0)
        {
            sprite.color = Color.yellow;
        }
        else
        {
            sprite.color = Color.red;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Tank>(out  var tank))
        {
            tank.SetBonusAction(ID);
            gameObject.SetActive(false);
        }  
    }
}
