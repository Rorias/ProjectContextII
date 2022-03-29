using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private static readonly CultureInfo CultUS = new CultureInfo("en-US");

    public AudioMixer mixer;

    [HideInInspector] public IniFile ini { get; private set; }

    #region video settings
    [HideInInspector] public bool fullScreen = true;
    [HideInInspector] public bool vsync = false;
    [HideInInspector] public int fpsLimit = 120;
    [HideInInspector] public int resNumber = 1;
    #endregion
    #region audio settings
    [HideInInspector] public float masterVolume = 1;
    [HideInInspector] public float musicVolume = 1;
    [HideInInspector] public float soundVolume = 1;
    #endregion

    #region const strings
    public const string SfullScreen = "fullScreen";
    public const string Svsync = "vsync";
    public const string SfpsLimit = "fpsLimit";
    public const string SresNumber = "resNumber";
    public const string SmasterVolume = "masterVolume";
    public const string SmusicVolume = "musicVolume";
    public const string SsoundVolume = "soundVolume";
    #endregion

    private void Start()
    {
        ini = new IniFile("saveSettings");

        LoadGameSettings();
        ApplyGameSettings();
    }

    private void LoadGameSettings()
    {
        fullScreen = Convert.ToBoolean(ini.Read(SfullScreen, "True"));
        vsync = Convert.ToBoolean(ini.Read(Svsync, "False"));
        fpsLimit = Convert.ToInt32(ini.Read(SfpsLimit, "120"));
        resNumber = Convert.ToInt32(ini.Read(SresNumber, "1"));

        masterVolume = Convert.ToSingle(ini.Read(SmasterVolume, "1"));
        musicVolume = Convert.ToSingle(ini.Read(SmusicVolume, "1"));
        soundVolume = Convert.ToSingle(ini.Read(SsoundVolume, "1"));
    }

    public void ApplyGameSettings()
    {
        Screen.fullScreenMode = fullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        QualitySettings.vSyncCount = Convert.ToInt32(vsync);
        Application.targetFrameRate = fpsLimit;

        Resolution[] resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (i == resNumber)
            {
                Screen.SetResolution(resolutions[i].width, resolutions[i].height, Screen.fullScreenMode, resolutions[i].refreshRate);
                break;
            }
        }

        AudioListener.volume = masterVolume;
        mixer.SetFloat("SFXvolume", Mathf.Log10(soundVolume) * 20);
        mixer.SetFloat("AmbientVolume", Mathf.Log10(soundVolume) * 20);
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
    }

    public void SaveGameSettings()
    {
        ini.Write(SfullScreen, fullScreen.ToString());
        ini.Write(Svsync, vsync.ToString());
        ini.Write(SfpsLimit, fpsLimit.ToString());
        ini.Write(SresNumber, resNumber.ToString());

        ini.Write(SmasterVolume, masterVolume.ToString());
        ini.Write(SmusicVolume, musicVolume.ToString());
        ini.Write(SsoundVolume, soundVolume.ToString());
    }
}
