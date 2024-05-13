using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public AnimatedSprite deathAnimation;
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        PlusSpeed,
    }

    public ItemType Type;

    private void Start()
    {
        deathAnimation = GetComponent<AnimatedSprite>();
        Debug.Log(deathAnimation);
        //deathAnimation.enabled = false;
    }

    private void OnPickupItem(GameObject player)
    {
        
        switch (Type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombControl>().AddBomb();
                break;
            case ItemType.BlastRadius:
                player.GetComponent<BombControl>().explosionRadius++;
                break;
            case ItemType.PlusSpeed:
                player.GetComponent<MovementControl>().speed++;
                break;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            OnPickupItem(collision.gameObject);
        }
    }

}
