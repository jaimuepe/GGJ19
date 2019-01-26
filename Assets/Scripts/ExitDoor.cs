using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{

    [Header("Shake")]
    [SerializeField]
    float shakeDurationSeconds;

    [SerializeField]
    float magnitude;

    Transform mTransform;

    bool usable = true;

    private void OnEnable()
    {
        mTransform = transform;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (!usable)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ExitRoom();
        }
    }
#endif

    public void ExitRoom()
    {
        usable = false;
        LevelTransitionManager.Instance.LoadNextLevel();
    }

    public FMODUnity.StudioEventEmitter emitterHit;

    public void PlayHitClip()
    {
        emitterHit.Play();
    }

    IEnumerator IEShake()
    {
        Vector3 orignalPosition = mTransform.position;
        float elapsed = 0f;

        while (elapsed < shakeDurationSeconds)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            mTransform.position = new Vector3(orignalPosition.x + x, orignalPosition.y + y, orignalPosition.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        mTransform.position = orignalPosition;
    }
}
