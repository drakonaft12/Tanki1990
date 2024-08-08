using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bonus : MonoBehaviour
{
    int ID;
    public void Create()
    {
        ID = Random.Range(0,2);
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
