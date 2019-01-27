using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public string sceneToLoad;
    public Text tittleText;
    public GameObject fish;
    public Animator canvasAnim, fishAnim, faderAnim;
    public Button playButton;

    public FMODUnity.StudioEventEmitter clickEmitter;

    void Start()
    {
        canvasAnim.Play("MainMenu_WriteTittle");
    }

    public void LoadScene()
    {
        clickEmitter.Play();
        StartCoroutine(LoadSceneCoroutine());
    }

    IEnumerator LoadSceneCoroutine()
    {
        // fish.GetComponent<Animator>().Play("Headbutt");
        faderAnim.Play("MainMenu_Fade");
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }

    public void WriteTittle()
    {
        PrintDialog();
    }

    public void PrintDialog(string dialog = "Phil's Way Home")
    {
        StartCoroutine(PrintDialogCoroutine(dialog));
    }

    private IEnumerator PrintDialogCoroutine(string textToPrint)
    {
        char[] textToPrintCharArray = textToPrint.ToCharArray();
        for (int i = 0; i < textToPrintCharArray.Length; i++)
        {
            tittleText.text += textToPrintCharArray[i];
            yield return new WaitForSeconds(0.05f);
        }
        canvasAnim.Play("MainMenu_WriteTittle");
    }

    public void ShowFish()
    {
        fishAnim.Play("MainMenu_ShowFish");
        StartCoroutine(SetSelectedGameObjectEndOfFrame(playButton));
    }

    IEnumerator SetSelectedGameObjectEndOfFrame(Button bt)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(bt.gameObject);
    }
}
