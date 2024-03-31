using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private Toggle fullScreenTog;
    [SerializeField]
    private Toggle vsyncTog;
    [SerializeField]
    private TMP_Text resolutionLabel;
    //[SerializeField]
    //private AudioMixer theMixer;
    //[SerializeField]
    //private TMP_Text masterLabel;
    //[SerializeField]
    //private TMP_Text musicLabel;
    //[SerializeField]
    //private TMP_Text sfxLabel;
    //[SerializeField]
    //private Slider masterSlider;
    //[SerializeField]
    //private Slider musicSlider;
    //[SerializeField]
    //private Slider sfxSlider;
    [SerializeField]
    private List<Vector2> resolutions = new List<Vector2>();

    int selectedRes;
    private void Start()
    {
        fullScreenTog.isOn = Screen.fullScreen;
        vsyncTog.isOn = QualitySettings.vSyncCount != 0;
        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (resolutions[i].x == Screen.width)
            {
                foundRes = true;
                selectedRes = i;
                break;
            }
        }
        if (!foundRes)
        {
            resolutions.Add(new Vector2(Screen.width, Screen.height));
            selectedRes = resolutions.Count - 1;
        }
        UpdateResolutionLabel();

        //float vol = 0;
        //theMixer.GetFloat("MasterVol", out vol);
        //masterSlider.value = vol;
        //theMixer.GetFloat("MusicVol", out vol);
        //musicSlider.value = vol;
        //theMixer.GetFloat("SFXVol", out vol);
        //sfxSlider.value = vol;

        //masterLabel.text = (masterSlider.value + 80).ToString("F0");
        //musicLabel.text = (musicSlider.value + 80).ToString("F0");
        //sfxLabel.text = (sfxSlider.value + 80).ToString("F0");
    }
    public void ResLeft()
    {
        selectedRes--;
        if (selectedRes < 0)
        {
            selectedRes = resolutions.Count - 1;
        }
        UpdateResolutionLabel();
    }
    public void ResRight()
    {
        selectedRes++;
        if (selectedRes == resolutions.Count)
        {
            selectedRes = 0;
        }
        UpdateResolutionLabel();
    }
    public void UpdateResolutionLabel()
    {
        resolutionLabel.text = resolutions[selectedRes].x.ToString() + "x" + resolutions[selectedRes].y.ToString();
    }
    public void ApplyGraphics()
    {
        QualitySettings.vSyncCount = vsyncTog.isOn == true ? 1 : 0;
        Screen.SetResolution((int)resolutions[selectedRes].x, (int)resolutions[selectedRes].y, fullScreenTog.isOn);
    }
    //public void SetMasterVol()
    //{
    //    masterLabel.text = (masterSlider.value + 80).ToString("F0");
    //    theMixer.SetFloat("MasterVol", masterSlider.value);

    //    PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    //}
    //public void SetMusicVol()
    //{
    //    musicLabel.text = (musicSlider.value + 80).ToString("F0");
    //    theMixer.SetFloat("MusicVol", musicSlider.value);

    //    PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    //}
    //public void SetSFXVol()
    //{
    //    sfxLabel.text = (sfxSlider.value + 80).ToString("F0");
    //    theMixer.SetFloat("SFXVol", sfxSlider.value);

    //    PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    //}
}
