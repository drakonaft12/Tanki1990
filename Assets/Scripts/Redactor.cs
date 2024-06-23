
using System.Collections.Generic;
using UnityEngine;

public class Redactor : MonoBehaviour
{
    [SerializeField] Spawner _spawner;
    [SerializeField] SåttingPlanå _såtting;
    PlaneRedactor[][] planes;
    [SerializeField] Vector2Int size;

    SaveAllBlock saveAllBlock;

    DataSave save;
    public SåttingPlanå SåtSåttingPlanå { get => _såtting; }


    void Start()
    {
        save = new DataSave();
        saveAllBlock = new SaveAllBlock();
        saveAllBlock.settingBlocks = new List<SettingPlaneWWW>();
        planes = new PlaneRedactor[size.x][];
        for (int i = 0; i < size.x; i++)
        {
            planes[i] = new PlaneRedactor[size.y];
        }
        transform.localScale = (Vector3Int)size;
    }
    [ContextMenu("Save")]
    void Save()
    {
        save.SaveGame(saveAllBlock, "TestMap");
    }

    [ContextMenu("Load")]
    void Load()
    {
        var s = new SaveAllBlock();
        s.settingBlocks = new List<SettingPlaneWWW>();
        save.LoadGame(ref s, "TestMap");
        saveAllBlock.settingBlocks.Clear();
        saveAllBlock.settingBlocks.AddRange(s.settingBlocks);
        for (int j = 0; j < size.y; j++)
        {
            for (int i = 0; i < size.x; i++)
            {
                foreach (var item in s.settingBlocks)
                {
                    Debug.Log(item.positionX);
                    if (item.positionX == i - size.x / 2 && item.positionY == j - size.y / 2)
                    {
                        if (planes[i][j] == null)
                        {
                            planes[i][j] = _spawner.Spawn<PlaneRedactor>(0, new Vector3(item.positionX, item.positionY));
                            planes[i][j].CreateWWW(item, _såtting.GetMaterial(item.materialID));
                            Debug.Log(new Vector3(item.positionX, item.positionY));
                        }
                        else
                        {
                            planes[i][j].gameObject.SetActive(false);
                            planes[i][j] = _spawner.Spawn<PlaneRedactor>(0, new Vector3(item.positionX, item.positionY));
                            planes[i][j].CreateWWW(item, _såtting.GetMaterial(item.materialID));
                            Debug.Log(new Vector3(item.positionX, item.positionY));
                        }

                    }
                }
            }
        }
    }

    public void ButtonForm(Vector2Int vector, bool b)
    {
        _såtting.x[vector.x].y[vector.y] = b;
    }

    public void SetSettingToPaint(Material material, int damage, LayerMask layer)
    {
        _såtting.material = material;
        _såtting.damage = damage;
        _såtting.layer = layer;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            var v = Vector3Int.CeilToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition) - Vector3.one * 0.5f);
            v -= Vector3Int.forward * v.z;
            Vector3Int vector = Vector3Int.right * (size.x / 2) + Vector3Int.up * (size.y / 2) + v;
            if (vector.x >= 0 && vector.y >= 0 && vector.x < size.x && vector.y < size.y)
            {
                if (planes[vector.x][vector.y] == null)
                {
                    planes[vector.x][vector.y] = _spawner.Spawn<PlaneRedactor>(0, v);
                    planes[vector.x][vector.y].Create(_såtting);
                    var s = planes[vector.x][vector.y].Setting;
                    saveAllBlock.settingBlocks.Add(planes[vector.x][vector.y].Setting);
                }
                else
                {
                    saveAllBlock.settingBlocks.Remove(planes[vector.x][vector.y].Setting);
                    planes[vector.x][vector.y].gameObject.SetActive(false);
                    planes[vector.x][vector.y] = _spawner.Spawn<PlaneRedactor>(0, v);
                    planes[vector.x][vector.y].Create(_såtting);
                    saveAllBlock.settingBlocks.Add(planes[vector.x][vector.y].Setting);
                }
            }
        }
    }
}
