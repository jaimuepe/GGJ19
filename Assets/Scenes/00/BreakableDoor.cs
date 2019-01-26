using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableDoor : MonoBehaviour
{
    [SerializeField]
    Sprite undamagedSprite;

    [SerializeField]
    Sprite mildlyDamagedSprite;

    SpriteRenderer sr;

    [SerializeField]
    ExitDoor exitDoor;

    [SerializeField]
    float shakeDurationSeconds;

    [SerializeField]
    float magnitude;

    [SerializeField]
    ParticleSystem explosionParticleSystem;

    Transform mTransform;
    int hits = 0;
    int maxHits = 2;

    public FMODUnity.StudioEventEmitter emitterHit;

    private void Start()
    {
        mTransform = transform;
        sr = GetComponent<SpriteRenderer>();
        hits = maxHits;
        UpdateSprite();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            HeadButt();
        }
    }
#endif

    public void HeadButt()
    {
        // Camera.main.GetComponent<FinalCamera>().ShakeCamera(0.075f, 0.5f);
        hits--;
        UpdateSprite();
        StartCoroutine(IEShake());

        if (hits == 1)
        {
            emitterHit.Play();
        }

        if (hits <= 0)
        {
            explosionParticleSystem.Play();
            exitDoor.gameObject.SetActive(true);
            exitDoor.PlayHitClip();
            gameObject.SetActive(false);
        }
    }

    private void UpdateSprite()
    {
        if (hits == 2)
        {
            sr.sprite = undamagedSprite;
        }
        else if (hits == 1)
        {
            sr.sprite = mildlyDamagedSprite;
        }
        else
        {
            sr.sprite = null;
        }
    }

    public IEnumerator IEShake()
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
