using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseTimeline : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _playableDirector;
    
    // Start is called before the first frame update


  
    public void PauseScene()
    {
        _playableDirector.Pause();
    }

}
