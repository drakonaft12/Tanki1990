using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonOfMatria : MonoBehaviour
{
    [SerializeField] BlockSetting settingBlock;
    [SerializeField] SettingPlane _s�tting;

    public BlockSetting SettingBlock { set => settingBlock = value; }
    public SettingPlane S�tting { set => _s�tting = value; }

    public void Init()
    {
        GetComponent<Image>().sprite = settingBlock.spriteButton;
        GetComponent<Image>().color = settingBlock.color;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        _s�tting.material = settingBlock.material;
        _s�tting.damage = settingBlock.damage;
        _s�tting.layer = settingBlock.layer;
        _s�tting.Object = settingBlock.Object;
    }

}
