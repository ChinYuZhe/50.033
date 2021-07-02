using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public float upforce;
    public float speed;

    private Vector2 currentPosition;
    private Vector2 currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.AddForce(Vector2.up * upforce, ForceMode2D.Impulse);

        currentDirection = new Vector2(1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = currentDirection * speed + new Vector2(0, rigidBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pipe"))
        {
            currentDirection *= -1.0f; //change direction
        }

        if (col.gameObject.CompareTag("Player"))
        {
            currentDirection *= 0.0f; //stop
        }

    }

    void OnBecomeInvisible() //not workinng?
    {
        Debug.Log("Invisible");
        Destroy(gameObject);
    }
}
