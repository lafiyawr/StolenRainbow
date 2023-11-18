using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class bwFade : MonoBehaviour
{
    // Start is called before the first frame update
  
   private PostProcessVolume bwVolume;
    private ColorGrading bwgrading;
    float timeElapsed;
    float lerpDuration = 5;
    float startValue = 0;
    float endValue = -100;
    float valueToLerp;
    void Start()
    {
        var getVolume = GetComponent<PostProcessVolume>();
        bwVolume = getVolume;
        bwVolume.profile.TryGetSettings(out bwgrading);
    }

    // Update is called once per frame
    void Update()
    {
      
      // bwgrading.saturation.value = -50f;

        if (timeElapsed < lerpDuration)
        {
            bwgrading.saturation.value = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            print(timeElapsed);
        } else
        {
            return;
        }


    }
}
