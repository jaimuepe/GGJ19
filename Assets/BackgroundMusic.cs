using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    FMODUnity.StudioEventEmitter bgEmitter;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        bgEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "ViñetaScene")
        {
            bgEmitter.SetParameter("vineta", 1.0f);
        }
        else if (scene.name == "scene00")
        {
            bgEmitter.SetParameter("ingame", 1.0f);
        }
        else if (scene.name == "scene99")
        {
            bgEmitter.SetParameter("playa", 1.0f);
        }
    }
}
