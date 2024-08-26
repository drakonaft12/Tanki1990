using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBlockControll : MonoBehaviour
{
    [SerializeField] private List<BlockSetting> settings;
    [SerializeField] private SettingPlane paint;
    [SerializeField] private Spawner spawner;

    private void Start()
    {
        foreach (var setting in settings)
        {
            var button = spawner.Spawn<ButtonOfMatria>(2,Vector3.zero);
            button.transform.SetParent(transform);
            button.SettingBlock = setting;
            button.Såtting = paint;
            button.Init();
        }
    }
}
