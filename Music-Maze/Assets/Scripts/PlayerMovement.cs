using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // Variable for displaying score and combo and lives remaining
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI comboDisplay;
    public TextMeshProUGUI livesRemaining;

    // Variable for life
    int life;

    // Variable for movement
    bool canMoveLeft;
    bool canMoveRight;
    bool canMoveUp;
    bool canMoveDown;
    bool canMove;
    Rigidbody2D player;
    Vector2 playerPrevPos;
    public LayerMask wall;
    bool canTouchWall;

    // Variable for scoring and combo
    public float score { get; private set; }
    public int combo { get; private set; }

    // Fail message
    public TextMeshProUGUI failMessage;

    // Start is called before the first frame update
    void Start()
    {
        canMoveLeft = true;
        canMoveRight = true;
        canMoveUp = true;
        canMoveDown = true;
        canMove = true;
        player = GetComponent<Rigidbody2D>();
        player.position = new Vector2(-1, -1);
        playerPrevPos = player.position;
        canTouchWall = true;
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement and restricting player from moving back to their previous position
        if (Input.GetKeyDown(KeyCode.A) && canMoveLeft && canMove)
        {
            StartCoroutine(MoveLeft());
        }
        else if (Input.GetKeyDown(KeyCode.D) && canMoveRight && canMove)
        {
            StartCoroutine(MoveRight());
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMoveDown && canMove)
        {
            StartCoroutine(MoveDown());
        }
        else if (Input.GetKeyDown(KeyCode.W) && canMoveUp && canMove)
        {
            StartCoroutine(MoveUp());
        }

        // Checking if player collided with wall
        if (player.IsTouchingLayers(wall) && canTouchWall)
        {
            StartCoroutine(Collide());
        }
    }

    // Bug found: when player collide with wall the direction the player can move is reset so the player can move backwards
    // by making use of this bug
    IEnumerator Collide()
    {
        canTouchWall = false;
        life -= 1;
        player.position = playerPrevPos;
        Debug.Log(player.position);
        score -= (float)(100 + (100 * combo * 0.2));
        combo = 0;
        canMoveLeft = true;
        canMoveRight = true;
        canMoveUp = true;
        canMoveDown = true;
        scoreDisplay.text = "Score: " + score.ToString();
        comboDisplay.text = "Combo: " + combo.ToString();
        livesRemaining.text = "Life: " + life.ToString();

        // Checking if player died
        if (life == 0)
        {
            canMove = false;
            failMessage.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1);

        canTouchWall = true;
    }

    IEnumerator MoveUp()
    {
        canMove = false;
        playerPrevPos = player.position;
        player.position = new Vector2(player.position.x, player.position.y + 2);
        canMoveDown = false;
        canMoveRight = true;
        canMoveLeft = true;
        combo += 1;
        score += (float)(100 + (100 * combo * 0.2));
        scoreDisplay.text = "Score: " + score.ToString();
        comboDisplay.text = "Combo: " + combo.ToString();

        yield return new WaitForSeconds(1);

        canMove = true;
    }

    IEnumerator MoveDown()
    {
        canMove = false;
        playerPrevPos = player.position;
        player.position = new Vector2(player.position.x, player.position.y - 2);
        canMoveUp = false;
        canMoveRight = true;
        canMoveLeft = true;
        combo += 1;
        score += (float)(100 + (100 * combo * 0.2));
        scoreDisplay.text = "Score: " + score.ToString();
        comboDisplay.text = "Combo: " + combo.ToString();

        yield return new WaitForSeconds(1);

        canMove = true;
    }

    IEnumerator MoveLeft()
    {
        canMove = false;
        playerPrevPos = player.position;
        player.position = new Vector2(player.position.x - 2, player.position.y);
        canMoveRight = false;
        canMoveUp = true;
        canMoveDown = true;
        combo += 1;
        score += (float)(100 + (100 * combo * 0.2));
        scoreDisplay.text = "Score: " + score.ToString();
        comboDisplay.text = "Combo: " + combo.ToString();

        yield return new WaitForSeconds(1);

        canMove = true;
    }

    IEnumerator MoveRight()
    {
        canMove = false;
        playerPrevPos = player.position;
        player.position = new Vector2(player.position.x + 2, player.position.y);
        canMoveLeft = false;
        canMoveUp = true;
        canMoveDown = true;
        combo += 1;
        score += (float)(100 + (100 * combo * 0.2));
        scoreDisplay.text = "Score: " + score.ToString();
        comboDisplay.text = "Combo: " + combo.ToString();

        yield return new WaitForSeconds(1);

        canMove = true;
    }
}
