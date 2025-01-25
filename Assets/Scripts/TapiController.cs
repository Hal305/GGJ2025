using System;
using UnityEngine;

public class TapiController : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    //What color/sprite is on it
    public int variant;

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        int bubbleCounter = 0;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Tapioka")
            {
                if (colliders[i].GetComponent<TapiController>().variant == variant)
                {
                    bubbleCounter ++;
                }
            }
        }
        //If this is colliding with 2 or more other bubbles of the same color
        if (bubbleCounter > 2)
        {
            //Ondestroy Pop nearby bubbles of the same color
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        speed = 0.03f;
        direction = Vector2.up;
    }

    private void OnDestroy()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.6f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Tapioka")
            {
                if (colliders[i].GetComponent<TapiController>().variant == variant) Destroy(colliders[i].gameObject);
            }
        }
    }
}
