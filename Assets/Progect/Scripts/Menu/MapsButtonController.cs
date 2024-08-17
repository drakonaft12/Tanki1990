using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapsButtonController : MonoBehaviour
{
    [SerializeField] private MapButton prefab;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Transform parent;
    private List<MapButton> buttons;

    private void OnEnable()
    {
        buttons = new List<MapButton>();
        var data = new DataSave();
        var maps = data.GetNameMaps();
        for (int i = 0; i < maps.Count; i++)
        {
            var button = spawner.Spawn<MapButton>(0 ,Vector3.zero);
            button.transform.SetParent(parent, false);
            button.SetText(maps[i]);
            buttons.Add(button);
        }
    }

    private void OnDisable()
    {
        foreach (var button in buttons)
        {
            spawner.Destroy(button.gameObject);
        }
    }
}
