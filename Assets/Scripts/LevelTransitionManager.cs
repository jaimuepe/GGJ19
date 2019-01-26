using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionManager : MonoBehaviour
{
    public float cameraSpeed = 6.0f;
    public AnimationCurve cameraSpeedCurve;

    static LevelTransitionManager instance;
    public static LevelTransitionManager Instance
    {
        get
        {
            if (instance == null)
            {
                LevelTransitionManager ltm = FindObjectOfType<LevelTransitionManager>();
                if (ltm)
                {
                    instance = ltm;
                }
                else
                {
                    GameObject go = new GameObject("[LEVEL_TRANSITION_MANAGER]");
                    instance = go.AddComponent<LevelTransitionManager>();
                }
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(AnimateCameraOut());
    }

    IEnumerator AnimateCameraOut()
    {

        yield return new WaitForSeconds(1.0f);
        Scene currentScene = SceneManager.GetActiveScene();
        yield return SceneManager.LoadSceneAsync(currentScene.buildIndex + 1, LoadSceneMode.Additive);

        DisplaceObjects(currentScene.buildIndex + 1);

        Camera mainCamera = Camera.main;

        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = mainCamera.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;

        Transform mainCameraTransform = mainCamera.transform;

        float animationTime = 0.0f;

        while (mainCameraTransform.position.x < camWidth)
        {
            mainCameraTransform.position = Vector3.Lerp(
                mainCameraTransform.position,
                new Vector3(
                    mainCameraTransform.position.x
                        + cameraSpeed * cameraSpeedCurve.Evaluate(animationTime) * Time.deltaTime,
                    mainCameraTransform.position.y,
                    mainCameraTransform.position.z
                ), 35.0f * Time.deltaTime);

            animationTime += Time.deltaTime;
            yield return null;
        }

        mainCameraTransform.position = new Vector3(
                camWidth,
                mainCameraTransform.position.y,
                mainCameraTransform.position.z
                );

        yield return SceneManager.UnloadSceneAsync(currentScene.buildIndex);
        DisplaceEverythingBack();
       
    }

    private void DisplaceCamera()
    {
        Camera mainCamera = Camera.main;

        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = mainCamera.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;

        Transform mainCameraTransform = mainCamera.transform;

        mainCameraTransform.position = new Vector3(
        -camWidth - camHalfWidth * 0.5f,
        mainCameraTransform.position.y,
        mainCameraTransform.position.z
        );
    }

    private void DisplaceObjects(int sceneIndex)
    {
        Scene scene = SceneManager.GetSceneByBuildIndex(sceneIndex);
        Camera mainCamera = Camera.main;
        Transform mainCameraTransform = mainCamera.transform;

        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = mainCamera.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;

        for (int i = 0; i < scene.GetRootGameObjects().Length; i++)
        {
            Transform t = scene.GetRootGameObjects()[i].transform;

            if (t.gameObject.CompareTag("Player"))
            {
                t.position = t.position + new Vector3(
                camWidth,
                0.0f,
                0.0f);

                CharacterController2D cc = t.gameObject.GetComponent<CharacterController2D>();
                cc.WalkRightEndlessly = true;
            }
            else
            {
                t.position = t.position + new Vector3(
                    camWidth,
                    0.0f,
                    0.0f);
            }
        }
    }

    private void DisplaceEverythingBack()
    {
        Scene scene = SceneManager.GetActiveScene();
        Camera mainCamera = Camera.main;
        Transform mainCameraTransform = mainCamera.transform;

        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = mainCamera.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;

        mainCameraTransform.position = new Vector3(
            0.0f,
            mainCameraTransform.position.y,
            mainCameraTransform.position.z
            );

        for (int i = 0; i < scene.GetRootGameObjects().Length; i++)
        {
            Transform t = scene.GetRootGameObjects()[i].transform;
            if (t.gameObject.CompareTag("Player"))
            {
                t.position = t.position - new Vector3(
                camWidth,
                0.0f,
                0.0f);
            }
            else
            {
                t.position = t.position - new Vector3(
                    camWidth,
                    0.0f,
                    0.0f);
            }
        }
    }
}
