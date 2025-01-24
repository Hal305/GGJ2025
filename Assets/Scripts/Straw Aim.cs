using System;
using UnityEngine;

public class StrawAim : MonoBehaviour
{
    private float rotateSpeed = 20f;
    public GameObject tapioka;
    public Transform tapiSpawn;

    private float shootTimer;
    private float cooldown = 0.5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && shootTimer < Time.timeSinceLevelLoad)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Spawn Tapioka on spawnpoint
            Instantiate(tapioka, tapiSpawn.position, Quaternion.identity);
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
