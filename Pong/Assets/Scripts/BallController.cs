using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public Vector2 velocity;

    private Rigidbody2D rb;

    private Vector3 position;
    private float width;
    private float height;

    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        position = transform.position;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
        
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        Invoke("StartBall", 1);
    }

    // Update is called once per frame
    void Update() {
        //position.x += velocity.x * Time.deltaTime;
        //position.y += velocity.y * Time.deltaTime;

        position = transform.position;

        // Check top bounds
        if (isInBoundsY()) {
            transform.position = position;
        } else {
            velocity.y *= -1;
            rb.velocity = velocity;
        }
    }

    public void StartBall() {
        float random = Random.Range(0, 2);
        if (random < 1) {
            rb.AddForce(velocity);
        } else {
            rb.AddForce(-velocity);
        }
    }

    public void ResetBall() {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    public bool isInBoundsY() {
        if (position.y + height / 2 < screenBounds.y && position.y - height / 2 > screenBounds.y * -1) {
            return true;
        } else {
            return false;
        }
    }

    public void CheckOutOfBoundsX() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            velocity.x = rb.velocity.x;
            velocity.y = (rb.velocity.y / 2) + (collision.collider.attachedRigidbody.velocity.y / 2);
            rb.velocity = velocity;
        }
    }
}
