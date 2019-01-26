using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public string sceneToLoad;
    public Text tittleText;
    public GameObject fish;
    public Animator canvasAnim;

    void Start()
    {
        WriteTittle();
    }

    void Update()
    {
        
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    IEnumerator LoadSceneCoroutine()
    {
        fish.GetComponent<Animator>().Play("Headbutt");
        yield return new WaitForSeconds(2f);
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
}
