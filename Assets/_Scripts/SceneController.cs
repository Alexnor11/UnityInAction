using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] MemoryCard originalCard;
    [SerializeField] Sprite[] images;

    private void Start()
    {
        int id = Random.Range(0, images.Length);
        originalCard.SerCard(id, images[id]);
    }
}
