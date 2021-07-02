using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private SpriteRenderer marioSprite;
    private Rigidbody2D marioBody;
    private Animator marioAnimator;
    private AudioSource marioAudioSource;

    public float speed;
    public float maxSpeed = 10;
    public float upSpeed;
    private bool faceRightState = true;
    private bool onGroundState = true;
    //public Transform enemyLocation;
    public Text scoreText;
    public Button restartButton;
    //private int score = 0;
    private bool countScoreState = false;

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState); //animator

            countScoreState = false; // reset score state
            //scoreText.text = "Score: " + score.ToString();
        };

        if ((col.gameObject.CompareTag("Obstacles") && Mathf.Abs(marioBody.velocity.y) < 0.01f) || (col.gameObject.CompareTag("Pipe") && Mathf.Abs(marioBody.velocity.y) < 0.01f))
        {
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState); //animator
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");
            Time.timeScale = 0.0f; //Game Over
            restartButton.gameObject.SetActive(true); //so we can click it to reset the scene
        }
    }

    void PlayJumpSound()
    {
        marioAudioSource.PlayOneShot(marioAudioSource.clip);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;

        //mario
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        //marioAnimator
        marioAnimator = GetComponent<Animator>();

        //audio
        marioAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;

            //check velocity
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }


        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;

            //check velocity
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

        // when jumping, and Gomba is near Mario and we haven't registered our score
        //if (!onGroundState && countScoreState)
        //{
        //    if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
        //    {
        //        countScoreState = false;
        //        score++;
        //        Debug.Log(score);
        //    }
        //}
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //dynamic rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
            {
                marioBody.AddForce(movement * speed);
            }
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            //stop
            marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true; //check if Gomba is underneath

            marioAnimator.SetBool("onGround", onGroundState);
        }
    }
}
