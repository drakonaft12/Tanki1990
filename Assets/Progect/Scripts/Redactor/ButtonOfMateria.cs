using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonOfMatria : MonoBehaviour
{
    [SerializeField] BlockSetting settingBlock;
    [SerializeField] SettingPlane _setting;

    public BlockSetting SettingBlock { set => settingBlock = value; }
    public SettingPlane Setting { set => _setting = value; }

    public void Init()
    {
        GetComponent<Image>().sprite = settingBlock.spriteButton;
        GetComponent<Image>().color = settingBlock.color;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        _setting.material = settingBlock.material;
        _setting.damage = settingBlock.damage;
        _setting.layer = settingBlock.layer;
        _setting.Object = settingBlock.Object;
    }

}
