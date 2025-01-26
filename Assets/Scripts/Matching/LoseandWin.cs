using Unity.VisualScripting;
using UnityEngine;

public class LoseandWin : MonoBehaviour
{
    private bool end = false;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Tapioka" && !end)
        {
            //Scene Transition to menu
            print("Lost");
            end = true;
        }
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > 60f && !end)
        {
            //Scene transition to machine
            print("Win");
            end = true;
        }
    }
}
