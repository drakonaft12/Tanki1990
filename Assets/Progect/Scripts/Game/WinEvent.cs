using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinEvent : MonoBehaviour
{
    private List<GameObject> tanks = new List<GameObject>();

    public List<GameObject> Tanks { get => tanks; }

    public void Win()
    {
        Debug.Log("Win");
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        var value = 0;
        foreach(var tank in tanks)
        {
            if(tank.activeSelf == false)
            {
                value++;
                print(tanks.Count-value);
            }
        }

        if(value == tanks.Count)
        {
            Win();
        }
    }
}
