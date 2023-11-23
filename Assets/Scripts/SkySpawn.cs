using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.AR.Semantics;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Playables;

public class SkySpawn : MonoBehaviour
{
    public ARCameraManager _cameraMan;
    public ARSemanticSegmentationManager _semanticMan;
    public TMP_Text _text;
    public RawImage _image;
    public Material _material;
    public GameObject cube;
    public float distance = 0f;
    public bool spawnRainbow = false;
    public GameObject rainbow;
    [SerializeField]
    private PlayableDirector _playableDirector;

    private string _channel = "sky";
    void OnEnable()
    {
        _cameraMan.frameReceived += OnCameraFrameUpdate;
    }

    private void OnDisable()
    {
        _cameraMan.frameReceived -= OnCameraFrameUpdate;
    }

  


    private void OnCameraFrameUpdate(ARCameraFrameEventArgs args)
    {
        if (!_semanticMan.subsystem.running)
        {
            return;
        }

        //get the semantic texture
        Matrix4x4 mat = Matrix4x4.identity;
        var texture = _semanticMan.GetSemanticChannelTexture(_channel, out mat);

        if (texture)
        {
            //the texture needs to be aligned to the screen so get the display matrix
            //and use a shader that will rotate/scale things.
            Matrix4x4 cameraMatrix = args.displayMatrix ?? Matrix4x4.identity;
            _image.material = _material;
            _image.material.SetTexture("_SemanticTex", texture);
            _image.material.SetMatrix("_SemanticMat", mat);
        }
    }



    private float _timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (!_semanticMan.subsystem.running)
        {
            return;
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            var pos = Input.mousePosition;
#else
        if (Input.touches.Length > 0)
            {
            var pos = Input.touches[0].position;
#endif
            // currently not being used, but this will instantiate an object when and where someones touches the sky. I wanted to make sure that the rainbow would show up in a part of the 
            //sky that's most visible. If there's a better way to detect that, please let me know. :)
            if (pos.x > 0 && pos.x < Screen.width)
                if (pos.y > 0 && pos.y < Screen.height)
                {
                    _timer += Time.deltaTime;
                   
                    var newPos = Camera.main.transform.TransformPoint(Vector3.forward * distance);
                    var newRot = Camera.main.transform.rotation;
                    if (_timer > 0.05f)
                    {
                        var list = _semanticMan.GetChannelNamesAt((int)pos.x, (int)pos.y);

                        if (list.Count > 0 && list[0] == _channel && !spawnRainbow)
                        {
                            _playableDirector.Play();
                            _text.text = "sky!";
                          // Instantiate(cube, newPos, newRot);
                          rainbow.transform.position = newPos;
                            rainbow.transform.rotation = newRot;
                            rainbow.SetActive(true);
                                               

                            spawnRainbow = true;

                        }
                        else
                        {
                            _text.text = "not sky!";
                        }

                        _timer = 0.0f;
                    }
                }
        }

    }

}
