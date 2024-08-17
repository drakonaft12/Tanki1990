using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonsLogic : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button redactorButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private List<Button> retunedButtons;

    [SerializeField] private List<GameObject> panels;

    private int mapActive = 0;

    private void Awake()
    {

        startGameButton.onClick.AddListener(() => { EnablePanel(1); });
        redactorButton.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        settingsButton.onClick.AddListener(() => { EnablePanel(2); });
        exitGameButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });

        foreach (var button in retunedButtons)
        {
            button.onClick.AddListener(() =>
            {
                EnablePanel(0);
            });
        }
    }

    private void EnablePanel(int on)
    {
        panels[on].SetActive(true);
        panels[mapActive].SetActive(false);
        mapActive = on;
    }
}
