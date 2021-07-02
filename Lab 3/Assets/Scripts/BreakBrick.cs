using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    private bool broken = false;
    public GameObject prefab;

    void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.CompareTag("Player") && !broken)
		{
			broken = true;
			prefab.SetActive(true);
			// assume we have 5 debris per box
			for (int x = 0; x < 10; x++)
			{
				Instantiate(prefab, transform.position, Quaternion.identity);
			}
			gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
			gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
			GetComponent<EdgeCollider2D>().enabled = false;
		}
	}
}
