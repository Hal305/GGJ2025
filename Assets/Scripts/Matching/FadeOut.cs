using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private float fadeSpeed = 1f;
    private float Opacity = 1;

    void Start()
    {
        var col = GetComponent<SpriteRenderer>().color;
        col.a = Opacity;
    }
    
    void Update()
    {
        Opacity -= fadeSpeed * Time.deltaTime;
    }
}
