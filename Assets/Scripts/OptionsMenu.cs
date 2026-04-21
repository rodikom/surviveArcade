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
}
