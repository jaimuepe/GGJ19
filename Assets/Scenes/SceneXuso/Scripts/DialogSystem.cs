using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public Text dialogTextBox;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PrintDialog();
        }
    }

    public void PrintDialog(string dialog="Escribe algo!")
    {
        StartCoroutine(PrintDialogCoroutine(dialog));
    } 

    private IEnumerator PrintDialogCoroutine(string textToPrint)
    {
        char[] textToPrintCharArray = textToPrint.ToCharArray();
        for(int i = 0; i < textToPrintCharArray.Length; i++)
        {
            dialogTextBox.text += textToPrintCharArray[i];
            yield return new WaitForSeconds(0.01f);
        }
    }
}
