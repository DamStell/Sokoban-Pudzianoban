using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider volumeSlider;

    public Dropdown dropdownResolution;

    public Toggle fullScreenToggle;

    private int screenInt;

 
    void Start()
    {
        screenInt = PlayerPrefs.GetInt("togglestate");
        if(screenInt==1)
        {
            fullScreenToggle.isOn = true;
        }
        else
        {
            fullScreenToggle.isOn = false;
        }
        volumeSlider.value = PlayerPrefs.GetFloat("Mvolume", 1f);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Mvolume"));
        dropdownResolution.value = PlayerPrefs.GetInt("resolutionOption");
    }
    
    List<int> widths = new List<int>() {640, 854, 1366, 1920, 2048};
    List<int> heights = new List<int>() {360, 480, 768, 1080, 1152};
 
    public void SetScreenSize (int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);
        PlayerPrefs.SetInt("resolutionOption", index);

    }
    
    public void SetFullScreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; 

        if(isFullscreen == false)
        {
            PlayerPrefs.SetInt("tooglestate", 0);
        }
        else
        {
            isFullscreen = true;
            PlayerPrefs.SetInt("togglestate",1);
        }
    }
    public void setVolume(float volume)
    {
        
        PlayerPrefs.SetFloat("Mvolume", volume);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Mvolume"));
    }
}
