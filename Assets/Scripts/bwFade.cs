using JetBrains.Annotations;
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
   public  float lerpDuration = 5;
    float startValue = 0;
    float endValue = -100;
    float valueToLerp;
    public bool startFade = false;
    void Start()
    {
        var getVolume = GetComponent<PostProcessVolume>();
        bwVolume = getVolume;
        bwVolume.profile.TryGetSettings(out bwgrading);
      
    }

    // Update is called once per frame
    void Update()
    {

       if(startFade)
        {
            // this trigger the post processing color saturation to fade to black and white after a certain duration. Will eventually be triggered after an animation sequence
            if (timeElapsed < lerpDuration)
            {
                bwgrading.saturation.value = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                //  print(timeElapsed);
               
            }
            else
            {
               startFade= false;
                return;
            }

        }
         
   

    }

    public void fadeStart()
    {
        startFade= true;
    }
}
