using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnMap : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Vector2Int size;
    [SerializeField] private WinEvent win;
    [SerializeField] private Transform fonMap;
    private Plane[][] planes;
    private float timeB = 5;
    private SaveAllBlock saveAllBlock;
    private DataSave save;

    public Vector2Int Size { get => size; }
    // Start is called before the first frame update
    void Start()
    {
        save = new DataSave();
        saveAllBlock = new SaveAllBlock();
        saveAllBlock.settingBlocks = new List<SettingPlaneWWW>();

        Load(PlayerPrefs.GetString("LoadMap"));
    }

    [ContextMenu("Load")]
    void Load(string nameMap)
    {
        var s = new SaveAllBlock();
        s.settingBlocks = new List<SettingPlaneWWW>();
        save.LoadGame(ref s, nameMap);
        saveAllBlock.settingBlocks.Clear();
        saveAllBlock.settingBlocks.AddRange(s.settingBlocks);
        saveAllBlock.sizeCamera = Camera.main.orthographicSize = s.sizeCamera;
        saveAllBlock.sizePoleX = size.x = s.sizePoleX;
        saveAllBlock.sizePoleY = size.y = s.sizePoleY;
        fonMap.GetComponent<SpriteRenderer>().size = new Vector2(saveAllBlock.sizePoleX, saveAllBlock.sizePoleY);
        
        planes = new Plane[size.x][];
        for (int i = 0; i < size.x; i++)
        {
            planes[i] = new Plane[size.y];
        }
        for (int j = 0; j < size.y; j++)
        {
            for (int i = 0; i < size.x; i++)
            {
                foreach (var item in s.settingBlocks)
                {
                    if (item.positionX == i - size.x / 2 && item.positionY == j - size.y / 2)
                    {
                        if (planes[i][j] == null)
                        {
                            planes[i][j] = _spawner.Spawn<Plane>(1, new Vector3(item.positionX, item.positionY));
                            planes[i][j].Create(item, _spawner, win);
                        }
                        else
                        {
                            planes[i][j].gameObject.SetActive(false);
                            planes[i][j] = _spawner.Spawn<Plane>(1, new Vector3(item.positionX, item.positionY));
                            planes[i][j].Create(item, _spawner, win);
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Redactor");
        }

        timeB -= Time.deltaTime;
        if (timeB < 0)
        {
            timeB = Random.Range(5, 10);
            _spawner.Spawn<Bonus>(4, new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2))).Create();
        }
    }
}
