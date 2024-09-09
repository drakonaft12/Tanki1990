using System.Threading.Tasks;
using UnityEngine;



public class Bot : MonoBehaviour, ISetSpawner
{
    [SerializeField] SettingsPawn _settingsPawn;
    [SerializeField] Tank _player;
    [SerializeField] Spawner _spawner;

    bool isWork = true;
    Vector2 _move;
    Vector3 _position;
    float t = 0;
    int randSpeegBran;
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
        randSpeegBran = (int)(Random.Range(0.8f, 1.5f)*500);
        IsStop();
    }

    private async void IsStop()
    {

        while (isWork)
        {
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, _move, 30, 1 | 1 << 6 | 1 << 7 | 1 << 8);
            if (hit2D.collider.TryGetComponent<IDamaget>(out var damaget) && damaget.HP<=_player.ActionBehavior.Damage) { _player.Fire(); Debug.DrawRay(transform.position, _move * 30, Color.red, 0.5f); }
            if (_position == transform.position || t > Random.Range(1, 5))
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
            await Task.Delay(randSpeegBran);
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
