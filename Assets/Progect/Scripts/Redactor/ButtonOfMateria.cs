using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonOfMatria : MonoBehaviour
{
    [SerializeField] BlockSetting settingBlock;
    [SerializeField] SettingPlane _såtting;

    public BlockSetting SettingBlock { set => settingBlock = value; }
    public SettingPlane Såtting { set => _såtting = value; }

    public void Init()
    {
        GetComponent<Image>().sprite = settingBlock.spriteButton;
        GetComponent<Image>().color = settingBlock.color;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        _såtting.material = settingBlock.material;
        _såtting.damage = settingBlock.damage;
        _såtting.layer = settingBlock.layer;
        _såtting.Object = settingBlock.Object;
    }

}
