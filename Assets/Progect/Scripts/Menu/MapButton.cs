using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MapButton : MonoBehaviour
{
    [SerializeField] private TMP_Text TMP_Text;
    private Button button;

    public void SetText(string text)
    {
        TMP_Text.text = text;
    }

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(() =>
        {
            PlayerPrefs.SetString("LoadMap", TMP_Text.text);
            SceneManager.LoadScene("Game");
        });
    }
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
