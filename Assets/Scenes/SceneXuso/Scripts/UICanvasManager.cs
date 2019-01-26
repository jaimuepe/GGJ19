using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasManager : MonoBehaviour
{

    public GameObject ResumeButton, PauseButton;
    public Image Fader;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PauseGame()
    {
        PauseButton.SetActive(false);
        ResumeButton.SetActive(true);
        Time.timeScale = 0;
        Fader.color = new Color(Fader.color.r, Fader.color.g, Fader.color.b, 0.5f);
    }

    public void MuteGame()
    {

    }

    public void ResumeGame()
    {
        ResumeButton.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1;
        Fader.color = new Color(Fader.color.r, Fader.color.g, Fader.color.b, 0);
    }
}
