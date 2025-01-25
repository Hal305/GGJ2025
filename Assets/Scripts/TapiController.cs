using System;
using UnityEngine;

public class TapiController : MonoBehaviour
{
    public float shootSpeed;
    public Vector2 direction = Vector2.down;
    //What color/sprite is on it
    public int variant;
    private float riseSpeed = 0.1f;
    
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * shootSpeed;
        //swap riseSpeed with 
    }
    
    private void Update()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.6f);
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
        
        transform.Translate(Vector2.up * Time.deltaTime * riseSpeed);
    }


    private void OnDestroy()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.7f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Tapioka")
            {
                if (colliders[i].GetComponent<TapiController>().variant == variant) Destroy(colliders[i].gameObject);
            }
        }
    }
}
