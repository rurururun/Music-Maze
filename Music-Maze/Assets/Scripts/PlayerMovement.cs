using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variable for movement
    Rigidbody2D player;
    //Collider2D bodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        //bodyCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            player.position = new Vector2(player.position.x - 5, player.position.y);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            player.position = new Vector2(player.position.x + 5, player.position.y);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            player.position = new Vector2(player.position.x, player.position.y - 5);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            player.position = new Vector2(player.position.x, player.position.y + 5);
        }
    }
}
