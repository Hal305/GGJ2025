using UnityEngine;

public class ContinueMusicPlayback : MonoBehaviour
{
    private static ContinueMusicPlayback instance = null;
    private AudioSource audio;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
            Destroy(gameObject);
    }
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
    }
}
