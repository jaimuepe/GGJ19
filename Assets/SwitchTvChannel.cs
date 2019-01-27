using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTvChannel : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer sr;

    private void Start()
    {
        StartCoroutine(SwitchChannel());    
    }

    IEnumerator SwitchChannel()
    {
        while (true)
        {
            Sprite s = sprites[Random.Range(0, sprites.Length)];
            sr.sprite = s;

            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        }
    }
}
