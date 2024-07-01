
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Redactor : MonoBehaviour
{
    [SerializeField] Spawner _spawner;
    [SerializeField] SåttingPlanå _såtting;
    PlaneRedactor[][] planes;
    [SerializeField] Vector2Int size;
    [SerializeField] TMP_InputField inputX;
    [SerializeField] TMP_InputField inputY;

    SaveAllBlock saveAllBlock;

    DataSave save;
    public SåttingPlanå SåtSåttingPlanå { get => _såtting; }


    void Start()
    {
        save = new DataSave();
        saveAllBlock = new SaveAllBlock();
        saveAllBlock.settingBlocks = new List<SettingPlaneWWW>();
        UpdatePole();
    }

    private void UpdatePole()
    {
        inputX.text = size.x.ToString();
        inputY.text = size.y.ToString();
        Camera.main.orthographicSize = Mathf.Max(8f / 19f * size.x, 8f / 15f * size.y);
        if (planes != null)
            for (int j = 0; j < planes.Length; j++)
            {
                if (planes[j] != null)
                    for (int i = 0; i < planes[j].Length; i++)
                    {
                        if (planes[j][i] != null)
                            planes[j][i].gameObject.SetActive(false);
                    }
            }
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
        saveAllBlock.sizePoleX = size.x;
        saveAllBlock.sizePoleY = size.y;
        saveAllBlock.sizeCamera = Camera.main.orthographicSize;
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
        saveAllBlock.sizeCamera = Camera.main.orthographicSize = s.sizeCamera;
        saveAllBlock.sizePoleX = size.x = s.sizePoleX;
        saveAllBlock.sizePoleY = size.y = s.sizePoleY;
        UpdatePole();
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
                            planes[i][j] = _spawner.Spawn<PlaneRedactor>(0, new Vector3(item.positionX, item.positionY));
                            planes[i][j].CreateWWW(item, SåttingPlanå.GetMaterial(item.materialID));
                        }
                        else
                        {
                            planes[i][j].gameObject.SetActive(false);
                            planes[i][j] = _spawner.Spawn<PlaneRedactor>(0, new Vector3(item.positionX, item.positionY));
                            planes[i][j].CreateWWW(item, SåttingPlanå.GetMaterial(item.materialID));
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

    public void SetSettingToPaint(Material material, int damage, LayerMask layer, GameObject gameObject)
    {
        _såtting.material = material;
        _såtting.damage = damage;
        _såtting.layer = layer;
        _såtting.Object = gameObject;
    }

    public void InputX(string s)
    {
        var t = int.Parse(inputX.text);
        if (t % 2 == 1)
            size.x = t;
        UpdatePole();
    }
    public void InputY(string s)
    {
        var t = int.Parse(inputY.text);
        if (t % 2 == 1)
            size.y = t;
        UpdatePole();
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
