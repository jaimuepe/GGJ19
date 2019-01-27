using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneAnimation : MonoBehaviour
{
    public GameObject fullyClothedMesh;
    public GameObject withUnicycleMesh;
    public GameObject unicycleMesh;
    public GameObject fishMesh;

    void Start()
    {
        StartCoroutine(Animation1());
    }

    IEnumerator Animation1()
    {
        fullyClothedMesh.GetComponent<Animator>().SetBool("walk", true);

        Vector3 secondPosition = new Vector3(-2.5f, -3.2f, 0.0f);

        Transform fullyClothedTransform = fullyClothedMesh.transform;

        float step = 0.5f * Time.deltaTime;
        while (Vector3.Distance(fullyClothedTransform.position, secondPosition) > 0.01f)
        {
            fullyClothedTransform.position = Vector3.MoveTowards(
                fullyClothedTransform.position,
                secondPosition, 
                step);
            yield return null;
        }

        fullyClothedMesh.GetComponent<Animator>().SetBool("walk", false);

        yield return new WaitForSeconds(1.0f);

        fishMesh.transform.position = fullyClothedMesh.transform.position;

        fullyClothedMesh.gameObject.SetActive(false);
        fishMesh.gameObject.SetActive(true);

        yield return null;
    }
}
