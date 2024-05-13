using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombControl : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombCount = 1;
    private int bombsRemaining;

    public bool isKicked = false;
    public bool isMoved = false;
    public Vector2 hitDirection;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask[] obstacleLayers;
    public float explosionDuration = 0.5f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public Tilemap destructibleTile;
    public Destructible destructiblePrefab;

    public MovementControl movementControl;

    private void Start()
    {
        isKicked = false;
        isMoved = false;
        movementControl = gameObject.GetComponent<MovementControl>();
    }

    private void OnEnable()
    {
        bombsRemaining = bombCount;
    }

    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }

    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        Destroy(explosion.gameObject, explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombsRemaining++;
        
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if(length <= 0)
        {
            return;
        }

        position += direction;

        var collider1 = Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, obstacleLayers[0]);
        var collider2 = Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, obstacleLayers[1]);

        if (collider1)
        {
            ClearDestructible(position);
            return;
        }
        if (collider2)
        {
            ClearItem(collider2.gameObject, position);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        Destroy(explosion.gameObject, explosionDuration);

        Explode(position, direction, length - 1);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTile.WorldToCell(position);
        TileBase tile = destructibleTile.GetTile(cell);

        if (tile != null)
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            destructibleTile.SetTile(cell, null);
        }
    }

    private void ClearItem(GameObject item, Vector2 position)
    {
        item.GetComponent<AnimatedSprite>().enabled = true;
        Destroy(item.gameObject, 0.75f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            collision.isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bomb") && isKicked == true)
        {
            hitDirection = movementControl.direction;
            isMoved = true;
        }
    }

    public void AddBomb()
    {
        bombCount++;
        bombsRemaining++;
    }

}
