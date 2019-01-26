using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;

    private void Start()
    {
#if UNITY_WEBGL
        exitButton.gameObject.SetActive(false);
#endif
        StartCoroutine(SelectDelayed(startButton.gameObject));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator SelectDelayed(GameObject selectedGameObject)
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(selectedGameObject);
    }
}
