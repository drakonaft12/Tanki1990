using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOfMatria : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] int damage = 5;
    [SerializeField] LayerMask layer;
    [SerializeField] Redactor redactor;
    [SerializeField] GameObject Object;

    public void OnClick()
    {
        redactor.SetSettingToPaint(material,damage,layer, Object);
    }

}
