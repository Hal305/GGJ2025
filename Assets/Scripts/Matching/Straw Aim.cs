using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StrawAim : MonoBehaviour
{
    private float rotateSpeed = 20f;
    //ADD ability to have more/less based on difficulty chosen
    //List of available tapi
    public List<GameObject> possibleTapi;
    public int tapiCount = 3;
    public List<GameObject> tapiVariants;
    public Transform tapiSpawn;
    
    public AudioSource audioSource;
    
    Random rnd = new Random();
    
    public Difficulty difficulty;
    
    public List<GameObject> startTapiPoints;

    //Current + Future tapis shown in the straw
    public List<GameObject> nextTapis;
    public List<GameObject> nextTapisPositions;

    private float shootTimer;
    private float shootSpeed = 1f;
    private float cooldown = 0.5f;
    
    private void Start()
    {
        tapiCount = difficulty.tapiCount;
        //ADD Randomize the tapiVariants
        for (int i = 0; i < tapiCount; i++)
        {
            var n = rnd.Next(possibleTapi.Count-1);
            tapiVariants.Add(possibleTapi[n]);
            possibleTapi.RemoveAt(n);
        }
        //Add a Tapi
        nextTapis.Add(tapiVariants[rnd.Next(tapiVariants.Count)]);
        nextTapis.Add(tapiVariants[rnd.Next(tapiVariants.Count)]);
        //Add a Tapi, then add the tapis to the tapi positions.
        AddNextTapis();

        for (int i = 0; i < startTapiPoints.Count; i++)
        {
            Instantiate(tapiVariants[i], startTapiPoints[i].transform.position, startTapiPoints[i].transform.rotation);
        }
    }

    private void AddNextTapis()
    {
        nextTapis.Add(tapiVariants[rnd.Next(tapiVariants.Count)]);
        for (int i = 0; i < nextTapisPositions.Count; i++)
        {
            nextTapisPositions[i].GetComponent<SpriteRenderer>().sprite = nextTapis[i].GetComponent<SpriteRenderer>().sprite;
            nextTapisPositions[i].GetComponent<SpriteRenderer>().color = nextTapis[i].GetComponent<SpriteRenderer>().color;
        }
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && shootTimer < Time.timeSinceLevelLoad)
        {
            audioSource.Play();
            //Spawn Tapi on spawnpoint
            var tapioka = Instantiate(nextTapis[0], tapiSpawn.position, Quaternion.identity);
            tapioka.GetComponent<TapiController>().direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            tapioka.GetComponent<TapiController>().shootSpeed = shootSpeed;
            nextTapis.RemoveAt(0);
            AddNextTapis();
            shootTimer = Time.timeSinceLevelLoad + cooldown;
        }
    }
    
    private void FixedUpdate()
    {
        //Find vector between this and cursor
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Convert into angles
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        //Rotate object toward cursor
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }
}
