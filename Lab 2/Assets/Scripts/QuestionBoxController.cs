using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject consumablePrefab; //mushroom prefab
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox; //sprite that indicates empty/used question box

    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !hit)
        {
            hit = true;

            spriteRenderer.sprite = usedQuestionBox; // change sprite to be "used-box" sprite

            //spawn mushroom above the box
            Instantiate(consumablePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity);

            //call coroutine
            StartCoroutine(DisableHittable());
        }
    }

    bool ObjectMovedAndStopped()
    {
        return Mathf.Abs(rigidBody.velocity.magnitude) < 0.01;
    }

    IEnumerator DisableHittable()
    {
        if (!ObjectMovedAndStopped()) //check if moved and stopped
        {
            yield return new WaitUntil(() => ObjectMovedAndStopped());
        }

        //code continues here when the ObjectMovedAndStopped() returns true
        rigidBody.bodyType = RigidbodyType2D.Static; // make the box unaffected by Physics

        //reset box position
        this.transform.localPosition = Vector3.zero;
        springJoint.enabled = false; // disable spring
    }
}
