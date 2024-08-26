using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Redactor : MonoBehaviour
{
    [SerializeField] Spawner _spawner;
    [SerializeField] SettingPlane _såtting;
    PlaneRedactor[][] planes;
    [SerializeField] Vector2Int size;
    [SerializeField] TMP_InputField inputX;
    [SerializeField] TMP_InputField inputY;
    [SerializeField] Transform fon;
    [SerializeField] TMP_Dropdown dropdownMaps;
    [SerializeField] TMP_InputField AddMap;

    string _activeMap = "TestMap";
    List<string> _allMap;

    SaveAllBlock saveAllBlock;
    public TextAsset defaultMap;

    DataSave save;
    public SettingPlane SåtSåttingPlanå { get => _såtting; }

    private void Awake()
    {
        inputX.onEndEdit.AddListener(InputX);
        inputY.onEndEdit.AddListener(InputY); 
    }

    void Start()
    {
        
        save = new DataSave();
        _allMap = new List<string>();
        dropdownMaps.options.Clear();
        saveAllBlock = new SaveAllBlock();
        saveAllBlock.settingBlocks = new List<SettingPlaneWWW>();
        _allMap = save.GetNameMaps();

        if (_allMap.Count == 0)
        {
            saveAllBlock = JsonConvert.DeserializeObject<SaveAllBlock>(defaultMap.text);
            Save();
            _allMap = save.GetNameMaps();
            saveAllBlock = new SaveAllBlock();
            saveAllBlock.settingBlocks = new List<SettingPlaneWWW>();
        }

        foreach (var item in _allMap)
        {
            dropdownMaps.options.Add(new TMP_Dropdown.OptionData(item));
        }
        _activeMap = dropdownMaps.options[0].text;
        dropdownMaps.captionText.text = _activeMap;
        Load();
    }

    private void UpdatePole()
    {
        inputX.text = size.x.ToString();
        inputY.text = size.y.ToString();
        var iny = Mathf.Max(8f / 19f * size.x, 8f / 15f * size.y);
        Camera.main.orthographicSize = iny;
        var tr = Camera.main.pixelRect;
        fon.localScale = new Vector3(tr.width, tr.height) * iny / 256;
        if (saveAllBlock.sizePoleX != size.x || saveAllBlock.sizePoleY != size.y)
        {
            saveAllBlock.sizePoleX = size.x;
            saveAllBlock.sizePoleY = size.y;
            saveAllBlock.sizeCamera = Camera.main.orthographicSize;
            var p = new PlaneRedactor[size.x][];
            for (int i = 0; i < size.x; i++)
            {
                p[i] = new PlaneRedactor[size.y];
            }

            if (planes != null)
                for (int j = 0; j < planes.Length; j++)
                {
                    if (planes[j] != null)
                        for (int i = 0; i < planes[j].Length; i++)
                        {
                            if (planes[j][i] != null)
                                if (((j >= size.x) || (i >= size.y)))
                                {
                                    saveAllBlock.settingBlocks.Remove(planes[j][i].Setting);
                                    planes[j][i].gameObject.SetActive(false);
                                    planes[j][i] = null;
                                }
                                else
                                {
                                    p[j][i] = planes[j][i];
                                    p[j][i].gameObject.transform.position -= Vector3Int.right * Mathf.CeilToInt((size.x - planes.Length) / 2f) + Vector3Int.up * Mathf.CeilToInt((size.y - planes[j].Length) / 2f);
                                    saveAllBlock.settingBlocks.Remove(planes[j][i].Setting);
                                    p[j][i].UpdatePosition();
                                    saveAllBlock.settingBlocks.Add(p[j][i].Setting);

                                }

                        }
                }
            planes = null;
            planes = p;
        }
        transform.localScale = (Vector3Int)size;
    }

    [ContextMenu("Save")]
    public void Save()
    {
        saveAllBlock.sizePoleX = size.x;
        saveAllBlock.sizePoleY = size.y;
        save.SaveGame(saveAllBlock, _activeMap);
    }


    [ContextMenu("Load")]
    public void Load()
    {
        var s = new SaveAllBlock();
        s.settingBlocks = new List<SettingPlaneWWW>();
        save.LoadGame(ref s, _activeMap);
        saveAllBlock.settingBlocks.Clear();
        saveAllBlock.settingBlocks.AddRange(s.settingBlocks);
        size.x = s.sizePoleX;
        size.y = s.sizePoleY;
        DestroyPole();
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
                            planes[i][j].CreateWWW(item, SettingPlane.GetMaterial(item.materialID));
                        }
                        else
                        {
                            planes[i][j].gameObject.SetActive(false);
                            planes[i][j] = _spawner.Spawn<PlaneRedactor>(0, new Vector3(item.positionX, item.positionY));
                            planes[i][j].CreateWWW(item, SettingPlane.GetMaterial(item.materialID));
                        }

                    }
                }
            }
        }
    }

    public void OpenMap()
    {
        Save();
        PlayerPrefs.SetString("LoadMap", _activeMap);
        SceneManager.LoadScene("Game");
    }

    private void DestroyPole()
    {
        if (planes != null)
            for (int j = 0; j < planes.Length; j++)
            {
                if (planes[j] != null)
                    for (int i = 0; i < planes[j].Length; i++)
                    {
                        if (planes[j][i] != null)
                        {
                            planes[j][i].gameObject.SetActive(false);
                            planes[j][i] = null;
                        }

                    }
            }
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

    public void ChangeMap(int v)
    {
        Debug.Log(dropdownMaps.captionText.text);
        _activeMap = dropdownMaps.captionText.text;
    }

    public void AddNewMap()
    {
        _activeMap = AddMap.text;
        _allMap.Add(AddMap.text);
        dropdownMaps.options.Add(new TMP_Dropdown.OptionData(AddMap.text));
        dropdownMaps.value = dropdownMaps.options.Count - 1;
        Save();
    }

    void Update()
    {

        if (Input.GetAxis("Fire1")!=0)
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
        if(Input.GetAxis("Cancel") != 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
