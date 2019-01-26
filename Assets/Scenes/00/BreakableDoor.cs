using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableDoor : MonoBehaviour
{
    [SerializeField]
    Sprite undamagedSprite;

    [SerializeField]
    Sprite mildlyDamagedSprite;

    [SerializeField]
    Sprite wreckedSprite;

    SpriteRenderer sr;

    [SerializeField]
    ExitDoor exitDoor;

    int hits = 0;
    int maxHits = 2;

    private void Start()
    {
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
        hits--;
        UpdateSprite();
        if (hits <= 0)
        {
            exitDoor.gameObject.SetActive(true);
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
            sr.sprite = wreckedSprite;
        }
    }
}
