﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SaveSlotsNew : MonoBehaviour
{
    public TextMeshProUGUI slot1Text;
    public TextMeshProUGUI slot2Text;
    public TextMeshProUGUI slot3Text;
    public GameObject slot1Button;
    public GameObject slot2Button;
    public GameObject slot3Button;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveSystem.CheckFileExsits(1))
        {
            string fileTime = SaveSystem.GetFileLastWriteTime(1);
            string buttonText = "Last Played:" + fileTime;
            slot1Text.text = buttonText;
        }

        if (SaveSystem.CheckFileExsits(2))
        {
            string fileTime = SaveSystem.GetFileLastWriteTime(2);
            string buttonText = "Last Played:" + fileTime;
            slot2Text.text = buttonText;
        }

        if (SaveSystem.CheckFileExsits(3))
        {
            string fileTime = SaveSystem.GetFileLastWriteTime(3);
            string buttonText = "Last Played:" + fileTime;
            slot3Text.text = buttonText;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
