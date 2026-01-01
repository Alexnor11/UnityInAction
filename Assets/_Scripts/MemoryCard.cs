using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] GameObject cardBlack;
    [SerializeField] SceneController controller;

    private int _id;
    public int Id { get { return _id; } }

    public void SerCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown()
    {
        if (cardBlack.activeSelf && controller.canReveral)
        {
            cardBlack.SetActive(false);
            controller.CardRevealed(this);
        }
    }
    public void Unreveal()
    {
        cardBlack.SetActive(true);
    }
}
