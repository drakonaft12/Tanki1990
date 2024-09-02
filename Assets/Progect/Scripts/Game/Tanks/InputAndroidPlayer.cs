using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputAndroidPlayer : MonoBehaviour, ISetSpawner
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
        _player.MovePawn = InputUIController.MoveInput;

        if (InputUIController.FireInput) { _player.Fire(); }
    }
    private void OnDisable()
    {
        Debug.Log("GAME OVER");
        SceneManager.LoadScene("Menu");
    }
}
