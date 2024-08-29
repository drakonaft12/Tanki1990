using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class CameraSize : MonoBehaviour
{
    private TMP_InputField _inputField;
    private string _cameraSizeString = "CameraSize";
    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
        if (!PlayerPrefs.HasKey(_cameraSizeString))
        {
            PlayerPrefs.SetInt(_cameraSizeString, 7);
        }
        _inputField.text = PlayerPrefs.GetInt(_cameraSizeString).ToString();
        _inputField.onEndEdit.AddListener((value) => { PlayerPrefs.SetInt(_cameraSizeString, int.Parse(value)); });
    }
}
