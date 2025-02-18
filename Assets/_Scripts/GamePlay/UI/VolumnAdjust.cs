using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumnAdjust : MonoBehaviour
{
    public Slider volumnSlider;
    // Start is called before the first frame update
    void Start()
    {
        if(SaveSystem.GetVolumn() == -1)
        {
            volumnSlider.value = BgSound.Instance.audioSource.volume;
            //Debug.Log("Volumn: " + volumnSlider.value);
        }
        else
        {
            volumnSlider.value = SaveSystem.GetVolumn();
            BgSound.Instance.SetVolume(SaveSystem.GetVolumn());
            //Debug.Log("Volumn: " + volumnSlider.value);
        }
        volumnSlider.onValueChanged.AddListener(OnSliderChange);
    }
    
    void OnSliderChange(float value)
    {
        Debug.Log("Volumn: " + value);
        BgSound.Instance.SetVolume(value);
        SaveSystem.SaveVolumn(value);
    }

}
