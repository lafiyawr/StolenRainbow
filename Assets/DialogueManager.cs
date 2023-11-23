using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Android.Types;

public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    public String[] dialogue;
    public TMP_Text textBox;
   
   


    public void CurrentLine(int dialogueNum)
    {
        textBox.text= dialogue[dialogueNum];
    }

}
