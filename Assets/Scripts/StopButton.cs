using Niantic.ARDK.AR.Scanning;
using Niantic.Lightship.AR.Scanning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StopButton : MonoBehaviour
{

    public ARScanningManager _scanningManager;
    public TextMeshPro _text;
    public async void StopRecordingAndExport()
    {

        // save the recording with SaveScan()
        // use ScanStore() to get a reference to it, then ScanArchiveBuilder() to export it
        // output the path to the playback recording as a debug message
        string scanId = _scanningManager.GetCurrentScanId();
        _text.text = scanId;
        await _scanningManager.SaveScan();
        var savedScan = _scanningManager.GetScanStore().GetSavedScans().Find(scan => scan.ScanId == scanId);
        ScanArchiveBuilder builder = new ScanArchiveBuilder(savedScan, new UploadUserInfo());
        while (builder.HasMoreChunks())
        {
            var task = builder.CreateTaskToGetNextChunk();
            task.Start();
            await task;
          Debug.Log(task.Result);   // <- this is the path to the playback recording.
        }
        _scanningManager.enabled = false;
    }

}