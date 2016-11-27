using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MultiDoor : MonoBehaviour
{
    public Interactive[] activators;
    SpriteRenderer rend;
    int start_layer;
    Color start_color;
    protected bool open;

    void Start()
    {
        open = false;
        rend = GetComponent<SpriteRenderer>();
        start_layer = gameObject.layer;
        start_color = rend.color;
    }

    public virtual void Update()
    {
        // Get Activators
        bool all_active = true;
        for (int i = 0; i < activators.Length; i++)
        {
            if (!activators[i].active)
            {
                all_active = false;
                break;
            }
        }

        if (!open && all_active) // open door
        {
            OpenDoor();
        }
        else if (open && !all_active) // Close Door
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        gameObject.layer = 0;
        rend.color = new Color(start_color.r, start_color.g, start_color.b, 0f);
        open = true;
    }

    public void CloseDoor()
    {
        gameObject.layer = start_layer;
        rend.color = start_color;
        open = false;
    }
}
