using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputPlayer : MonoBehaviour,ISetSpawner
{
    [SerializeField] SettingsPawn _settingsPawn;
    [SerializeField] Tank _player;
    [SerializeField] Spawner _spawner;

    public Spawner SetSpawner { set => _spawner = value; }

    void Start()
    {
        _player.Craete(_settingsPawn, _spawner);
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

        if(Input.GetKeyDown(KeyCode.Space)) { _player.Fire(); }
    }
    private void OnDisable()
    {
        SceneManager.LoadScene("Menu");
    }
}
