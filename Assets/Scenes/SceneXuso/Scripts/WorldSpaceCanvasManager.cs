﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCanvasManager : MonoBehaviour
{
    public GameObject ActionButtonDialog, MovementKeysDialog, DialogBox;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowActionButtonDialog();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShowMovementKeysDialog();
        }
    }

    public void ShowActionButtonDialog()
    {
        if (ActionButtonDialog.activeSelf)
        {
            DialogBox.SetActive(false);
            ActionButtonDialog.SetActive(false);
        }
        else
        {
            DialogBox.SetActive(true);
            ActionButtonDialog.SetActive(true);
        }    
    }

    public void ShowMovementKeysDialog()
    {
        if (MovementKeysDialog.activeSelf)
        {
            DialogBox.SetActive(false);
            MovementKeysDialog.SetActive(false);
        }
        else
        {
            DialogBox.SetActive(true);
            MovementKeysDialog.SetActive(true);
        }
    }
}
