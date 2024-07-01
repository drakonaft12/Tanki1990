using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    [SerializeField] Spawner _spawner;
    Plane[][] planes;
    [SerializeField] Vector2Int size;

    SaveAllBlock saveAllBlock;

    DataSave save;
    // Start is called before the first frame update
    void Start()
    {
        save = new DataSave();
        saveAllBlock = new SaveAllBlock();
        saveAllBlock.settingBlocks = new List<SettingPlaneWWW>();

        Load();
    }

    [ContextMenu("Load")]
    void Load()
    {
        var s = new SaveAllBlock();
        s.settingBlocks = new List<SettingPlaneWWW>();
        save.LoadGame(ref s, "TestMap");
        saveAllBlock.settingBlocks.Clear();
        saveAllBlock.settingBlocks.AddRange(s.settingBlocks);
        saveAllBlock.sizeCamera = Camera.main.orthographicSize = s.sizeCamera;
        saveAllBlock.sizePoleX = size.x = s.sizePoleX;
        saveAllBlock.sizePoleY = size.y = s.sizePoleY;
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
                            planes[i][j].Create(item, _spawner);
                        }
                        else
                        {
                            planes[i][j].gameObject.SetActive(false);
                            planes[i][j] = _spawner.Spawn<Plane>(1, new Vector3(item.positionX, item.positionY));
                            planes[i][j].Create(item, _spawner);
                        }

                    }
                }
            }
        }
    }
}
