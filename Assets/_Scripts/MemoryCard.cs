using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] GameObject cardBlack;
    [SerializeField] Sprite image;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown()
    {
        if (cardBlack.activeSelf)
        {
            cardBlack.SetActive(false);
        }
    }
}
