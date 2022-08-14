using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // Variable for movement
    bool canMoveLeft;
    bool canMoveRight;
    bool canMoveUp;
    bool canMoveDown;
    Rigidbody2D player;
    GameObject[] wall;
    //Collider2D bodyCollider;

    // Fail message
    public TextMeshProUGUI failMessage;

    // Start is called before the first frame update
    void Start()
    {
        canMoveLeft = true;
        canMoveRight = true;
        canMoveUp = true;
        canMoveDown = true;
        player = GetComponent<Rigidbody2D>();
        wall = GameObject.FindGameObjectsWithTag("Wall");
        //bodyCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if player collided with wall
        foreach (GameObject w in wall)
        {
            if (player.IsTouching(w.GetComponent<Collider2D>()))
            {
                canMoveLeft = false;
                canMoveRight = false;
                canMoveUp = false;
                canMoveDown = false;
                failMessage.gameObject.SetActive(true);
                player.gameObject.SetActive(false);
            }
        }

        // Movement and restricting player from moving back to their previous position
        if (Input.GetKeyDown(KeyCode.A) && canMoveLeft)
        {
            player.position = new Vector2(player.position.x - 2, player.position.y);
            canMoveRight = false;
            canMoveUp = true;
            canMoveDown = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && canMoveRight)
        {
            player.position = new Vector2(player.position.x + 2, player.position.y);
            canMoveLeft = false;
            canMoveUp = true;
            canMoveDown = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMoveDown)
        {
            player.position = new Vector2(player.position.x, player.position.y - 2);
            canMoveUp = false;
            canMoveRight = true;
            canMoveLeft = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && canMoveUp)
        {
            player.position = new Vector2(player.position.x, player.position.y + 2);
            canMoveDown = false;
            canMoveRight = true;
            canMoveLeft = true;
        }
    }
}
