using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour {
    public Canvas menu;
    public Canvas note;
    public Toggle fullscreen;
    public Dropdown resolutionDropdown;
    public Slider music;
    public Resolution[] resolutions;
    public AudioSource musicSource;
    public Button accept;
    public Button accept2;
    public Button accept3;
    SettingData gameSetting;
    // Use this for initialization
    void Start () {
        note.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        gameSetting = new SettingData();
        //fullscreen.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        //resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        //music.onValueChanged.AddListener(delegate { OnMusicChange(); });
        accept.onClick.AddListener(delegate{ onClick(); });
        accept2.onClick.AddListener(delegate { onClick2(); });
        accept3.onClick.AddListener(delegate { onClick3(); });
        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            Debug.Log(resolution.ToString());
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
    }

    public void OnFullScreenToggle()
    {
        Debug.Log(fullscreen.isOn);
        gameSetting.fullscreen = Screen.fullScreen = fullscreen.isOn;
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, fullscreen.isOn);
    }

    public void OnResolutionChange()
    {
        Debug.Log(resolutions[resolutionDropdown.value].width);
        Debug.Log(resolutions[resolutionDropdown.value].height);
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, fullscreen.isOn);
    }

    public void OnMusicChange()
    {
        //musicSource.volume = gameSetting.volume = music.value;

        AudioListener.volume = music.value;

    }

    void onClick()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, fullscreen.isOn);
        AudioListener.volume = music.value;
        menu.enabled = false;
    }

    void onClick2()
    {
        menu.enabled = false;
    }

    void onClick3()
    {
        note.enabled = false;
    }
}
