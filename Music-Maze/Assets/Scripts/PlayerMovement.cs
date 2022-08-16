using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // Variable for displaying score and combo and lives remaining
    public TextMeshProUGUI scoreDisplay;
    public Image feverBar;
    public TextMeshProUGUI feverMessage;
    public TextMeshProUGUI livesRemaining;

    // Variable for life
    int life;

    // Variable for movement
    bool canMoveLeft;
    bool canMoveRight;
    bool canMoveUp;
    bool canMoveDown;
    bool fever;
    public Transform Up;
    public Transform Down;
    public Transform Left;
    public Transform Right;
    bool canMove;
    Rigidbody2D player;
    Vector2 playerPrevPos;
    public LayerMask wall;

    // Variable for scoring and combo
    public float score { get; private set; }
    public float combo { get; private set; }
    float eachNote;

    // Fail message
    public TextMeshProUGUI failMessage;

    // Start is called before the first frame update
    void Start()
    {
        canMoveUp = !Physics2D.OverlapCircle(Up.position, 0.1f, wall);
        canMoveDown = !Physics2D.OverlapCircle(Down.position, 0.1f, wall);
        canMoveLeft = !Physics2D.OverlapCircle(Left.position, 0.1f, wall);
        canMoveRight = !Physics2D.OverlapCircle(Right.position, 0.1f, wall);
        canMove = true;
        fever = false;
        player = GetComponent<Rigidbody2D>();
        player.position = new Vector2(-1, -1);
        playerPrevPos = player.position;
        feverBar.fillAmount = 0f;
        score = 0f;
        combo = 0f;
        eachNote = 100f;
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // Fever
        // Bug: fever does not reset
        if (combo < 10)
        {
            feverBar.fillAmount = combo / 10f;
        }
        else if (combo == 10 && !fever)
        {
            StartCoroutine(Fever());
        }

        // Movement and restricting player from moving back to their previous position
        if (Input.GetKeyDown(KeyCode.A) && canMoveLeft)
        {
            StartCoroutine(MoveLeft());
        }
        else if (Input.GetKeyDown(KeyCode.D) && canMoveRight)
        {
            StartCoroutine(MoveRight());
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMoveDown)
        {
            StartCoroutine(MoveDown());
        }
        else if (Input.GetKeyDown(KeyCode.W) && canMoveUp)
        {
            StartCoroutine(MoveUp());
        }

        // Checking if player collide with wall
        if (Input.GetKeyDown(KeyCode.A) && !canMoveLeft)
        {
            life -= 1;
            score -= eachNote;
            combo = 0;
            scoreDisplay.text = "Score: " + score.ToString();
            feverBar.fillAmount = 0f;
            feverMessage.gameObject.SetActive(false);
            livesRemaining.text = "Life: " + life.ToString();

            // Checking if player died
            if (life == 0)
            {
                canMove = false;
                failMessage.gameObject.SetActive(true);
                player.gameObject.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) && !canMoveRight)
        {
            life -= 1;
            score -= eachNote;
            combo = 0;
            scoreDisplay.text = "Score: " + score.ToString();
            feverBar.fillAmount = 0f;
            feverMessage.gameObject.SetActive(false);
            livesRemaining.text = "Life: " + life.ToString();

            // Checking if player died
            if (life == 0)
            {
                canMove = false;
                failMessage.gameObject.SetActive(true);
                player.gameObject.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) && !canMoveDown)
        {
            life -= 1;
            score -= eachNote;
            combo = 0;
            scoreDisplay.text = "Score: " + score.ToString();
            feverBar.fillAmount = 0f;
            feverMessage.gameObject.SetActive(false);
            livesRemaining.text = "Life: " + life.ToString();

            // Checking if player died
            if (life == 0)
            {
                canMove = false;
                failMessage.gameObject.SetActive(true);
                player.gameObject.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) && !canMoveUp)
        {
            life -= 1;
            score -= eachNote;
            combo = 0;
            scoreDisplay.text = "Score: " + score.ToString();
            feverBar.fillAmount = 0f;
            feverMessage.gameObject.SetActive(false);
            livesRemaining.text = "Life: " + life.ToString();

            // Checking if player died
            if (life == 0)
            {
                canMove = false;
                failMessage.gameObject.SetActive(true);
                player.gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if (Up.position.y == playerPrevPos.y)
        {
            canMoveUp = false;
            canMoveDown = !Physics2D.OverlapCircle(Down.position, 0.1f, wall);
            canMoveLeft = !Physics2D.OverlapCircle(Left.position, 0.1f, wall);
            canMoveRight = !Physics2D.OverlapCircle(Right.position, 0.1f, wall);
        }
        else if (Down.position.y == playerPrevPos.y)
        {
            canMoveDown = false;
            canMoveUp = !Physics2D.OverlapCircle(Up.position, 0.1f, wall);
            canMoveLeft = !Physics2D.OverlapCircle(Left.position, 0.1f, wall);
            canMoveRight = !Physics2D.OverlapCircle(Right.position, 0.1f, wall);
        }
        else if (Left.position.x == playerPrevPos.x)
        {
            canMoveLeft = false;
            canMoveUp = !Physics2D.OverlapCircle(Up.position, 0.1f, wall);
            canMoveDown = !Physics2D.OverlapCircle(Down.position, 0.1f, wall);
            canMoveRight = !Physics2D.OverlapCircle(Right.position, 0.1f, wall);
        }
        else if (Right.position.x == playerPrevPos.x)
        {
            canMoveRight = false;
            canMoveUp = !Physics2D.OverlapCircle(Up.position, 0.1f, wall);
            canMoveDown = !Physics2D.OverlapCircle(Down.position, 0.1f, wall);
            canMoveLeft = !Physics2D.OverlapCircle(Left.position, 0.1f, wall);
        }
        else
        {
            canMoveUp = !Physics2D.OverlapCircle(Up.position, 0.1f, wall);
            canMoveDown = !Physics2D.OverlapCircle(Down.position, 0.1f, wall);
            canMoveLeft = !Physics2D.OverlapCircle(Left.position, 0.1f, wall);
            canMoveRight = !Physics2D.OverlapCircle(Right.position, 0.1f, wall);
        }
    }

    // Bug found: when player collide with wall the direction the player can move is reset so the player can move backwards
    // by making use of this bug

    IEnumerator MoveUp()
    {
        canMove = false;
        playerPrevPos = player.position;
        player.position = new Vector2(player.position.x, player.position.y + 2);
        score += eachNote;
        if (!fever)
        {
            combo += 1;
        }
        scoreDisplay.text = "Score: " + score.ToString();

        yield return new WaitForSeconds(1);

        canMove = true;
    }

    IEnumerator MoveDown()
    {
        canMove = false;
        playerPrevPos = player.position;
        player.position = new Vector2(player.position.x, player.position.y - 2);
        score += eachNote;
        if (!fever)
        {
            combo += 1;
        }
        scoreDisplay.text = "Score: " + score.ToString();

        yield return new WaitForSeconds(1);

        canMove = true;
    }

    IEnumerator MoveLeft()
    {
        canMove = false;
        playerPrevPos = player.position;
        player.position = new Vector2(player.position.x - 2, player.position.y);
        score += eachNote;
        if (!fever)
        {
            combo += 1;
        }
        scoreDisplay.text = "Score: " + score.ToString();

        yield return new WaitForSeconds(1);

        canMove = true;
    }

    IEnumerator MoveRight()
    {
        canMove = false;
        playerPrevPos = player.position;
        player.position = new Vector2(player.position.x + 2, player.position.y);
        score += eachNote;
        if (!fever)
        {
            combo += 1;
        }
        scoreDisplay.text = "Score: " + score.ToString();

        yield return new WaitForSeconds(1);

        canMove = true;
    }

    IEnumerator Fever()
    {
        eachNote = 200f;
        fever = true;
        feverBar.fillAmount = 1f;
        feverMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(10);

        fever = false;
        feverBar.fillAmount = 0f;
        feverMessage.gameObject.SetActive(false);
        eachNote = 100f;
        combo = 0;
    }
}
