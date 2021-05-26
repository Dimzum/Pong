using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public float speed;

    public KeyCode moveUp;
    public KeyCode moveDown;

    private Vector3 position;
    private float height;

    private Vector2 velocity;
    private Rigidbody2D rb;

    private Vector2 screenBounds;
    private float boundsOffset = .15f;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        height = transform.GetComponent<SpriteRenderer>().bounds.size.y;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update() {
        // Check keyboard input
        if (Input.GetKey(moveUp)) {
            velocity.y = speed;
            //position.y += velocity.y * Time.deltaTime;
        } else if (Input.GetKey(moveDown)) {
            velocity.y = -speed;
            //position.y += velocity.y * Time.deltaTime;
        } else {
            velocity.y = 0;
        }

        rb.velocity = velocity;

        position = transform.position;

        // Checking bounds
        if (!isInBounds()) {
            if (position.y + height / 2 + boundsOffset > screenBounds.y) {
                position.y = screenBounds.y - boundsOffset - height / 2;
            } else if (position.y - height / 2 - boundsOffset < -screenBounds.y) {
                position.y = -screenBounds.y + boundsOffset + height / 2;
            }
        }

        transform.position = position;
    }

    public bool isInBounds() {
        if (position.y + height / 2 + boundsOffset < screenBounds.y && position.y - height / 2 - boundsOffset > -screenBounds.y) {
            return true;
        } else {
            return false;
        }
    }
}
