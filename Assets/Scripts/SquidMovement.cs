using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SquidMovement : MonoBehaviour
{
    public float squidSpeed;
    private Rigidbody2D squidBody;
    private CircleCollider2D circleCollider;
    public LayerMask[] obstacleLayer = new LayerMask[2];
    public List<Vector2> availableDirection { get; private set; }
    private Vector2 squidDirection;

    public SpriteRenderer squidDeadRenderer;
    public Animator squidDeadAnimator;
    public GameObject squidScore;
    public GameLogic logic;
    public Spawner spawner;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();

        squidSpeed = 1.5f;
        availableDirection = new List<Vector2>
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        squidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        squidDirection = Vector2.down;
    }

    private void FixedUpdate()
    {
        DetectWall(squidDirection);
        MoveBot(squidDirection);
    }

    private void MoveBot(Vector2 direction)
    {
        Vector2 position = transform.position;
        Vector2 translation = squidSpeed * Time.fixedDeltaTime * direction;

        squidBody.MovePosition(position + translation);
    }

    private void DetectWall(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, 0.5f, this.obstacleLayer[0] | this.obstacleLayer[1]);

        if (hit.collider != null )
        {
            int index = Random.Range(0, availableDirection.Count);

            if (availableDirection[index] == squidDirection)
            {
                index++;

                if (index >= availableDirection.Count)
                {
                    index = 0;
                }
            }
            squidDirection = availableDirection[index];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        enabled = false;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;

        squidDeadRenderer.enabled = true;
        squidDeadAnimator.enabled = true;

        Invoke(nameof(OnDeadSequence), 2f);

    }

    private void OnDeadSequence()
    {
        Destroy(gameObject);
        spawner.squidNumber--;
        if (spawner.squidNumber == 0)
        {
            FindObjectOfType<GameLogic>().NewRound();
        }

        squidScore = Instantiate(squidScore, transform.position, Quaternion.identity);
        Destroy(squidScore, 2f);
        logic.AddScore(logic.squidScore);
    }

}
