using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public bool Key1 = false;
    public bool Key2 = false;
    public bool inLoseMenue = false;
    public bool Inv = false;
    public bool GameIsPause = false;
    bool RPressed = false;
    GameObject LoseMenu;
    GameObject PauseMenu;
    public GameObject PlayerUI;
    GameObject WinnerMenu;

    public GameObject SettingMenu;
    public TextMeshProUGUI Inv_text;
    public List<Slider> Volume_sliders;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            ShowPuaseMenu();
        }
        if (Input.GetKeyDown(KeyCode.Backspace) && SceneManager.GetActiveScene().buildIndex == 0)
        {
            ShowSettingMenu(false);
        }
        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex == 1 && !RPressed)
        {
            RPressed = true;
            PlayerCamera.PlayerCamerMoveDisabler();
        }
        else if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex == 1 && RPressed)
        {
            RPressed = false;
            PlayerCamera.PlayerCamerMoveEnabler();
        }
        if (GameIsPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        if (PlayerUI != null)
        {
            Inv_text = PlayerUI.transform.GetChild(2).GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            if (Key1)
            {
                PlayerUI.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                PlayerUI.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            }
            if (Key2)
            {
                PlayerUI.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                PlayerUI.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            }
            if (Inv)
            {
                PlayerUI.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                PlayerUI.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    public void LoadGame()
    {
        LevelManager.instance.LoadScene(1);
        SceneManager.sceneLoaded += LoadUIGame;
    }
    private void ShowPuaseMenu()
    {
        PlayerUI.SetActive(false);
        GameIsPause = true;
        PauseMenu.SetActive(true);
        PlayerCamera.PlayerCamerMoveDisabler();
        PauseMenu.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => Resume());
        PauseMenu.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => RestartGame(true));
        PauseMenu.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => BackToMainMenu());
        PauseMenu.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(() => ExitTheGame());
    }

    public void ExitTheGame()
    {
        #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
        #else
         Application.Quit();
        #endif
    }

    private void BackToMainMenu()
    {
        if (PlayerUI)
        {
            PlayerUI.SetActive(false);
        }
        LevelManager.instance.LoadScene(0);
        SceneManager.sceneLoaded += LoadMainMenu;
    }

    private void LoadMainMenu(Scene arg0, LoadSceneMode arg1)
    {
        GameObject MainMenu = GameObject.Find("MainMenu");
        if (MainMenu != null)
        {
            MainMenu.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => LoadGame());
            MainMenu.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() => ShowSettingMenu(true));
            MainMenu.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(() => ExitTheGame());
            SettingMenu = MainMenu.transform.GetChild(1).gameObject;
            Volume_sliders.Clear();
            Volume_sliders.Add(SettingMenu.transform.GetChild(0).GetComponentInChildren<Slider>());
            Volume_sliders.Add(SettingMenu.transform.GetChild(1).GetComponentInChildren<Slider>());
            Volume_sliders.Add(SettingMenu.transform.GetChild(2).GetComponentInChildren<Slider>());
            Volume_sliders[0].onValueChanged.AddListener((v) => SetVolume(Volume_sliders[0]));
            Volume_sliders[1].onValueChanged.AddListener((v) => SetVolume(Volume_sliders[1]));
            Volume_sliders[2].onValueChanged.AddListener((v) => SetVolume(Volume_sliders[2]));

            Volume_sliders[0].value = AudioListener.volume;
            Volume_sliders[1].value = AudioManager.instance.sfxSource.volume;
            Volume_sliders[2].value = AudioManager.instance.musicSource.volume;
        }
    }
    public void ShowSettingMenu(bool Show)
    {
        SettingMenu.SetActive(Show);
    }
    public void SetVolume(Slider VolumeSlider)
    {
        if (VolumeSlider.transform.parent.name == "MasterVolume")
        {
            AudioManager.instance.SetMasterVolume(VolumeSlider.value);
        }
        else if (VolumeSlider.transform.parent.name == "SFXVolume")
        {
            AudioManager.instance.SetSFXVolume(VolumeSlider.value);
        }
        else if (VolumeSlider.transform.parent.name == "MusicVolume")
        {
            AudioManager.instance.SetMusicVolume(VolumeSlider.value);
        }
    }
    public void showWinnerMenu()
    {
        PlayerUI.SetActive(false);
        WinnerMenu.SetActive(true);
        GameIsPause = true;
        WinnerMenu.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => BackToMainMenu());
        PlayerCamera.PlayerCamerMoveDisabler();
    }
    private void Resume()
    {
        PlayerCamera.PlayerCamerMoveEnabler();
        GameIsPause = false;
        PauseMenu.SetActive(false);
        PlayerUI.SetActive(true);
    }
    public void ShowText(string text)
    {
        PlayerUI.transform.GetChild(PlayerUI.transform.childCount - 1).gameObject.GetComponent<TextMeshProUGUI>().text = text;
        Invoke("HideText", 2.5f);
    }
    private void HideText()
    {
        PlayerUI.transform.GetChild(PlayerUI.transform.childCount - 1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
    }
    private void LoadUIGame(Scene scene, LoadSceneMode mode)
    {
        GameObject UI = GameObject.Find("UI");
        if (UI != null)
        {
            LoseMenu = UI.transform.GetChild(0).gameObject;
            PauseMenu = UI.transform.GetChild(1).gameObject;
            PlayerUI = UI.transform.GetChild(2).gameObject;
            WinnerMenu = UI.transform.GetChild(3).gameObject;
        }
        //SceneManager.sceneLoaded -= LoadUIGame;
    }
    public IEnumerator ShowHealth()
    {
        GameObject HealthBar = null;
        if (PlayerUI)
        {
            HealthBar = PlayerUI.transform.GetChild(0).gameObject;
        }
        while (HealthBar)
        {
            HealthBar.GetComponent<Slider>().value = PlayerMovment.Health;
            HealthBar.GetComponentInChildren<TextMeshProUGUI>().text = Mathf.Clamp(PlayerMovment.Health, 0, 100).ToString();
            yield return null;
        }
    }
    public void ShowLoseMenu()
    {
        inLoseMenue = true;
        GameIsPause = true;
        HideText();
        PlayerUI.SetActive(false);
        LoseMenu.SetActive(true);
        LoseMenu.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => RestartGame(true));
        LoseMenu.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => RestartGame(false));
        PlayerCamera.PlayerCamerMoveDisabler();
    }
    void RestartGame(bool Monster)
    {
        LevelManager.instance.LoadScene(1);
        SceneManager.sceneLoaded += LoadUIGame;

        if (!Monster)
        {
            SceneManager.sceneLoaded += OffMonster;
        }

    }
    private void OffMonster(Scene scene, LoadSceneMode mode)
    {
        GameObject monsterObject = GameObject.FindGameObjectWithTag("Monster");
        if (monsterObject != null)
        {
            monsterObject.SetActive(false);
        }

        SceneManager.sceneLoaded -= OffMonster;
    }
    public void ShowSafeCracker(bool show)
    {
        if(show)
        {
            PlayerUI.transform.GetChild(3).gameObject.SetActive(true);
            PlayerCamera.PlayerCamerMoveDisabler();
        }
        else
        {
            PlayerUI.transform.GetChild(3).gameObject.SetActive(false);
            PlayerCamera.PlayerCamerMoveEnabler();
        }
    }
}
