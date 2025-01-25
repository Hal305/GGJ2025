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
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        speed = 0f;
    }
}
