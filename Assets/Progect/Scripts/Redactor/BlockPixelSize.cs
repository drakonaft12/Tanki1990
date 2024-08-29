using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
public class BlockPixelSize : MonoBehaviour
{
    private TMP_InputField _inputField;
    [SerializeField] private SettingPlane _setting;

    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
        _inputField.text = _setting.sizeVox.x.ToString();
        _inputField.onEndEdit.AddListener(PixelEdit);
    }
    private void PixelEdit(string pixel)
    {
        var value = int.Parse(pixel);
        _setting.sizeVox = Vector2Int.one * value;
        _setting.size = 1f / value;
    }

}
