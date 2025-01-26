using System.Collections;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private Bubble _bubble = null;
    [SerializeField] private float _spawnInterval = 1.0f;
    [SerializeField] private float _spawnRange = 10;
    
    private void Start()
    {
        StartCoroutine(BubbleSpawn());
    }

    private IEnumerator BubbleSpawn()
    {
        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= _spawnInterval)
            {
                timer = 0;

                Vector3 spawn = new Vector3(transform.position.x + Random.Range(-_spawnRange, _spawnRange), transform.position.y, transform.position.z);
                Instantiate(_bubble, spawn, Quaternion.identity);
            }
            yield return null;
        }
    }
}
