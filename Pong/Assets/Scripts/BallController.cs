using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public GameManager GM;

    public string playerTag;
    public string boundaryTag;
    public string lBoundaryName;
    public string rBoundaryName;

    public Vector2 velocity;

    private Rigidbody2D rb;

    private Vector3 position;
    private float width;
    private float height;

    private Vector2 startingVelocity;

    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        position = transform.position;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;

        startingVelocity = velocity;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        Invoke("StartBall", 1);
    }

    // Update is called once per frame
    void Update() {
        //position.x += velocity.x * Time.deltaTime;
        //position.y += velocity.y * Time.deltaTime;

        //position = transform.position;

        //transform.position = position;
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
        velocity = startingVelocity;
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!GM.isGamePaused) {
            if (collision.gameObject.tag == playerTag) {
                velocity.x = rb.velocity.x;
                velocity.y = (rb.velocity.y / 1) + (collision.collider.attachedRigidbody.velocity.y / 2);
                rb.velocity = velocity;
            } else if (collision.gameObject.tag == boundaryTag) {
                if (collision.gameObject.name == rBoundaryName) {
                    GM.Score(2);
                } else if (collision.gameObject.name == lBoundaryName) {
                    GM.Score(1);
                }

                ResetBall();
                Invoke("StartBall", 1);
            }
        }
    }
}
