using Unity.VisualScripting;
using UnityEngine;

public class LoseandWin : MonoBehaviour
{
    private bool end = false;
    private bool lost = false;
    private float winTime = 60f;

    public GameObject loseScreen;
    
    
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Tapioka" && !end)
        {
            //Scene Transition to menu
            print("Lost");
            loseScreen.GetComponent<SpriteRenderer>().enabled = true;
            end = true;
            lost = true;
        }
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > winTime && !end)
        {
            //Scene transition to machine
            TransitionUI.Instance.PlayTransition(1);
            print("Win");
            end = true;
        }

        if (lost && Input.GetMouseButtonDown(0))
        {
            TransitionUI.Instance.PlayTransition(1);
        }
    }
}
