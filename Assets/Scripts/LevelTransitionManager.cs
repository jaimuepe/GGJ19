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

    float animationTime;

    IEnumerator AnimateCameraIn()
    {

        Camera mainCamera = Camera.main;

        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = mainCamera.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;

        Transform mainCameraTransform = mainCamera.transform;

        while (mainCameraTransform.position.x < 0.0f)
        {
            mainCameraTransform.position = Vector3.Lerp(
                mainCameraTransform.position,
                new Vector3(
                    mainCameraTransform.position.x
                        + cameraSpeed * cameraSpeedCurve.Evaluate(animationTime) * Time.deltaTime,
                    mainCameraTransform.position.y,
                    mainCameraTransform.position.z
                ), 35.0f * Time.deltaTime);

            animationTime = Mathf.Max(animationTime - Time.deltaTime, 0.0f);
            yield return null;
        }
    }

    IEnumerator AnimateCameraOut()
    {

        Camera mainCamera = Camera.main;

        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = mainCamera.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;

        Transform mainCameraTransform = mainCamera.transform;

        animationTime = 0.0f;

        while (mainCameraTransform.position.x < camWidth + camHalfWidth * 0.5f)
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
                camWidth + camHalfWidth * 0.5f,
                mainCameraTransform.position.y,
                mainCameraTransform.position.z
                );

        Scene currentScene = SceneManager.GetActiveScene();

        DisplaceCamera();
        yield return SceneManager.UnloadSceneAsync(currentScene.buildIndex);
        yield return SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);

        StartCoroutine(AnimateCameraIn());
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
}
