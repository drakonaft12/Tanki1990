using System.Threading.Tasks;
using UnityEngine;



public class Bot : MonoBehaviour,ISetSpawner
{
    [SerializeField] SettingsPawn _settingsPawn;
    [SerializeField] Player _player;
    [SerializeField] Spawner _spawner;

    bool isWork = true;
    Vector2 _move;
    Vector3 _position;
    float t = 0;
    public Spawner SetSpawner { set { _spawner = value; } }
    void Start()
    {
        Create();
    }

    public void Create()
    {
        _player.Craete(_settingsPawn, _spawner);
        _move = Vector2.down;
        _position = transform.position;
        IsStop();
    }

    private async void IsStop()
    {

        while (isWork)
        {
            if (Random.Range(0, 5) == 0) { _player.Fire(); }
            if (_position == transform.position || t > Random.Range(1,5))
            {
                int i = Random.Range(-1, 2);
                switch (i)
                {
                    case 0:
                        _move = -_move;
                        break;

                    case 1:
                        _move = new Vector2(_move.y, _move.x);
                        break;

                    case -1:
                        _move = -new Vector2(_move.y, _move.x);
                        break;
                }
                t = 0;
            }
            _position = transform.position;
            await Task.Delay(500);
        }
    }

    private void OnDisable()
    {
        isWork = false;
    }

    // Update is called once per frame
    void Update()
    {
            _player.MovePawn = _move;
        t += Time.deltaTime;
    }
}
