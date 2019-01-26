using UnityEngine;
using System.Collections;

public class FinalCamera : MonoBehaviour
{

    public GameObject target;

    private Vector2 velocity;

    public float smoothTimeX;
    public float smoothTimeY;

    public bool Bounds;

    private Vector3 minCameraPos;
    private Vector3 maxCameraPos;

    public float minCameraPosX, minCameraPosY, maxCameraPosX, maxCameraPosY;
    //public float RatiominCameraPosX, RatiominCameraPosY, RatiomaxCameraPosX, RatiomaxCameraPosY;

    private float shakeTimer, shakeAmount;

    private float shakeTimer2, shakeAmount2;

    [HideInInspector] public bool BlockShake;

    void Awake()
    {
        BlockShake = false;
        /*
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }*/

        //CALCULAR ASPECT RATIO


        //if (((float)Screen.width/(float)Screen.height) > 1.4f) {
        //  16/9
        minCameraPos = new Vector3(minCameraPosX, minCameraPosY, -10);
        maxCameraPos = new Vector3(maxCameraPosX, maxCameraPosY, -10);
        //}
        /*if (((float)Screen.width/(float)Screen.height) < 1.4f) {
			//  4/3
			minCameraPos = new Vector3 (RatiominCameraPosX,RatiominCameraPosY,-10);
			maxCameraPos = new Vector3 (RatiomaxCameraPosX,RatiomaxCameraPosY,-10);
		}*/
    }

    void Update()
    {
        if (shakeTimer >= 0)
        {
            Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;
            transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);
            shakeTimer -= Time.deltaTime;
        }
        if (shakeTimer2 >= 0)
        {
            Vector2 ShakePos2 = Random.insideUnitCircle * shakeAmount2;
            transform.position = new Vector3(transform.position.x + ShakePos2.x, transform.position.y + ShakePos2.y, transform.position.z);
            shakeTimer2 -= Time.deltaTime;
        }

    }

    void FixedUpdate()
    {
        if (target != null)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref velocity.x, smoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref velocity.y, smoothTimeY);
            transform.position = new Vector3(posX, posY, transform.position.z);
        }
        if (Bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y), Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }

    public void ChangeBounds()
    {
        minCameraPos = new Vector3(minCameraPosX, minCameraPosY, -10);
        maxCameraPos = new Vector3(maxCameraPosX, maxCameraPosY, -10);
    }

    public void ShakeCamera(float shakeForce, float shakeTime)
    {
        //Debug.Log("SHAKECAMERAS");
        if (!BlockShake)
        {
            shakeAmount = shakeForce;
            shakeTimer = shakeTime;
        }
    }

    public void _ShakeFadeOut(int times, float force, float timeUntilNextTime)
    {
        StartCoroutine(ShakeFadeOut(times, force, timeUntilNextTime));
    }

    IEnumerator ShakeFadeOut(int times, float force, float timeUntilNextTime)
    {
        for (int i = 0; i <= times; i++)
        {
            ShakeCamera(force - (i * 0.1f), 1f);
            yield return new WaitForSeconds(timeUntilNextTime);
        }
    }

    public void _ShakeFadeIn(int times, float force, float timeUntilNextTime)
    {
        StartCoroutine(ShakeFadeIn(times, force, timeUntilNextTime));
    }

    IEnumerator ShakeFadeIn(int times, float force, float timeUntilNextTime)
    {
        for (int i = 0; i <= times; i++)
        {
            ShakeCamera(force + (i * 0.1f), 1f);
            yield return new WaitForSeconds(timeUntilNextTime);
        }
    }

    public void ShakeCameraAttack(float shakeForce, float shakeTime)
    {
        if (!BlockShake)
        {
            shakeAmount2 = shakeForce;
            shakeTimer2 = shakeTime;
        }
    }
}