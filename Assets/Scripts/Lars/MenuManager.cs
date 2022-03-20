using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;
using System;

public class MenuManager : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundSlider;

    public Toggle fullscreenToggle;
    public Toggle vsyncToggle;
    public TMP_Dropdown fpsDD;
    public TMP_Dropdown resDD;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void SaveGame()
    {

    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {

    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Initialize()
    {
        LoadAudioSettings();

        LoadGraphicSettings();

        LoadResSettings();
    }

    private void LoadAudioSettings()
    {
        masterSlider.value = gameManager.masterVolume;
        musicSlider.value = gameManager.musicVolume;
        soundSlider.value = gameManager.soundVolume;
    }

    private void LoadGraphicSettings()
    {
        fullscreenToggle.isOn = gameManager.fullScreen;
        vsyncToggle.isOn = gameManager.vsync;

        for (int i = 0; i < fpsDD.options.Count; i++)
        {
            if (fpsDD.options[i].text == gameManager.fpsLimit.ToString())
            {
                fpsDD.value = i;
                break;
            }
        }
    }

    private void LoadResSettings()
    {
        resDD.ClearOptions();

        Resolution[] resolutions = Screen.resolutions;

        List<string> resOptions = new List<string>();

        foreach (Resolution res in resolutions)
        {
            resOptions.Add(res.width + "x" + res.height + " : " + res.refreshRate);
        }

        resDD.AddOptions(resOptions);

        for (int i = 0; i < resOptions.Count; i++)
        {
            resDD.options[i].text = resOptions[i];
        }

        resDD.value = gameManager.resNumber;
    }

    public void SetMasterVolume()
    {
        Debug.Log(gameManager);
        gameManager.masterVolume = masterSlider.value;

        gameManager.ApplyGameSettings();
        gameManager.SaveGameSettings();
    }

    public void SetMusicVolume()
    {
        gameManager.musicVolume = musicSlider.value;

        gameManager.ApplyGameSettings();
        gameManager.SaveGameSettings();
    }

    public void SetSoundVolume()
    {
        gameManager.soundVolume = soundSlider.value;

        gameManager.ApplyGameSettings();
        gameManager.SaveGameSettings();
    }

    public void SetFullscreen()
    {
        gameManager.fullScreen = fullscreenToggle.isOn;

        gameManager.ApplyGameSettings();
        gameManager.SaveGameSettings();
    }

    public void SetVsync()
    {
        gameManager.vsync = vsyncToggle.isOn;

        gameManager.ApplyGameSettings();
        gameManager.SaveGameSettings();
    }

    public void SetFps()
    {
        gameManager.fpsLimit = Convert.ToInt32(fpsDD.options[fpsDD.value].text);

        gameManager.ApplyGameSettings();
        gameManager.SaveGameSettings();
    }

    public void SetResolution()
    {
        gameManager.resNumber = resDD.value;

        gameManager.ApplyGameSettings();
        gameManager.SaveGameSettings();
    }

    public void QuitToDesktop()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
