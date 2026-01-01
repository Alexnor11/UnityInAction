using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public const int greidRows = 2;
    public const int greidCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    
    [SerializeField] MemoryCard originalCard;
    [SerializeField] Sprite[] images;

    private void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        for (int i = 0; i < greidCols; i++)
        {
            for (int j = 0; j < greidRows; j++)
            {
                MemoryCard card;
                if(i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }
                int id = Random.Range(0, images.Length);
                originalCard.SerCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY,startPos.z);
            }
        }        
    }
}
