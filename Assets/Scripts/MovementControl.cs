using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public Rigidbody2D playerBody { get; private set; }
    public CircleCollider2D circleCollider { get; private set; }
    public Vector2 direction = Vector2.down;
    public float speed = 5;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public AnimatedSprite spriteUp;
    public AnimatedSprite spriteDown;
    public AnimatedSprite spriteLeft;
    public AnimatedSprite spriteRight;
    public Animator deathAnimator;
    public SpriteRenderer spriteRendererDeath;

    private AnimatedSprite activeSprite;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        circleCollider.isTrigger = false;
        activeSprite = spriteDown;
    }

    private void Update()
    {
        if(Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteDown);
        }
        else if(Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRight);
        }
        else
        {
            SetDirection(Vector2.zero, activeSprite);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = playerBody.position;
        Vector2 translation = speed * Time.fixedDeltaTime * direction;

        playerBody.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection, AnimatedSprite spriteRenderer)
    {
        direction = newDirection;

        spriteUp.enabled = (spriteRenderer == spriteUp);
        spriteDown.enabled = (spriteRenderer == spriteDown);
        spriteLeft.enabled = (spriteRenderer == spriteLeft);
        spriteRight.enabled = (spriteRenderer == spriteRight);

        activeSprite = spriteRenderer;  
        activeSprite.idle = (direction == Vector2.zero);  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            DeathSequence();
        }

    }



    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombControl>().enabled = false;

        spriteUp.enabled = false;
        spriteDown.enabled = false; 
        spriteLeft.enabled = false;
        spriteRight.enabled = false;

        spriteRendererDeath.enabled = true;
        deathAnimator.enabled = true;

        circleCollider.isTrigger = true;

        Invoke(nameof(OnDeathSequenceEnd), 2.5f);

    }

    private void OnDeathSequenceEnd()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameLogic>().CheckGameOver(gameObject);
    }
}
