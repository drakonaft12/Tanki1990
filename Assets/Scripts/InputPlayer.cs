using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] SettingsPawn _settingsPawn;
    [SerializeField] Player _player;
    void Start()
    {
        _player.Craete(_settingsPawn);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            _player.MovePawn = Vector2.up * (int)Input.GetAxis("Vertical");
        }
        else if(Input.GetAxis("Horizontal")!= 0) 
        {
            _player.MovePawn = Vector2.right* (int)Input.GetAxis("Horizontal");
        }
        else { _player.MovePawn = Vector2.zero;}
    }
}
