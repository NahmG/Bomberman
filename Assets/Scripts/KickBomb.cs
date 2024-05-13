using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBomb : MonoBehaviour
{
    public Rigidbody2D bomb;
    public float speed = 1.0f;
    public LayerMask layer;

    public BombControl bombControl;

    void Awake()
    {
        bombControl = GameObject.FindGameObjectWithTag("Player").GetComponent<BombControl>();
        Debug.Log(bombControl);
        bombControl.isMoved = false;
        bomb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (bombControl.isMoved)
        {
            bomb.MovePosition((Vector2)transform.position + speed * Time.fixedDeltaTime * bombControl.hitDirection);
        }
        Stop();
    }

    private void Stop()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, bombControl.hitDirection, 0.7f, layer);
        if (hit.collider != null)
        {
            bombControl.isMoved = false;
        }
    }
}
