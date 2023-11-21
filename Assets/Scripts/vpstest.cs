using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.AR.PersistentAnchors;
using Niantic.Lightship.AR.LocationAR;
using Niantic.Lightship.AR.Loader;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.FilePathAttribute;

public class vpstest : MonoBehaviour
{
    [SerializeField]
    private ARLocationManager _arLocationManager;

    [SerializeField]
    private TMP_Text _AnchorTrackingStateText;

    private ARLocation[] _arLocation;
     

    private void OnEnable()
    {
        _arLocationManager.locationTrackingStateChanged += OnLocationTrackingStateChanged;
    }

    void Start()
    {
        if (string.IsNullOrWhiteSpace(LightshipSettings.Instance.ApiKey))
        {
            if (_AnchorTrackingStateText != null)
            {
                _AnchorTrackingStateText.text = "No API key is set";
            }

            return;
        }

        if (_arLocationManager == null)
        {
            if (_AnchorTrackingStateText != null)
            {
                _AnchorTrackingStateText.text = "No Location Manager to listen to";
            }

            return;
        }
        if (_arLocationManager.ARLocations.Length < 1)
        {
            _AnchorTrackingStateText.text = "Add an AR Location to the AR Location Manager.";
            return;
        }

        if (_AnchorTrackingStateText != null)
        {
            _AnchorTrackingStateText.text = "Select a Location to start tracking";
            //get the list of AR locations
            _arLocation = _arLocationManager.ARLocations;
            
        }
    }

    private void OnDisable()
    {
        _arLocationManager.locationTrackingStateChanged -= OnLocationTrackingStateChanged;
    }

    private void OnLocationTrackingStateChanged(ARLocationTrackedEventArgs args)
    {
        if (args.Tracking)
        {
            if (_AnchorTrackingStateText != null)
            {
                _AnchorTrackingStateText.text = $"Anchor Tracked";
            }
        }
        else
        {
            if (_AnchorTrackingStateText != null)
            {
                _AnchorTrackingStateText.text = $"Anchor Untracked";
            }
        }
    }

    //This targets a particular location based on array number. This will allow me to start tracking different locations at key points in the game
    public void locationChanger(int locNum)
    {
        _arLocationManager.SetARLocations(_arLocation[locNum]);
        _arLocationManager.StartTracking();
        _AnchorTrackingStateText.text = _arLocation[locNum].name;
        print(_AnchorTrackingStateText.name);
    }

    //stop the current tracking so that a new location can be  tracked. It works but gives me an error message. Not sure why.
    public void resetTracking()
    {
        _arLocationManager.StopTracking();
       
        _AnchorTrackingStateText.text = "Finding New Location";

    }


}
