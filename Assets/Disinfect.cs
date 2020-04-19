using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disinfect : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"init");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Waypoint") {
            Waypoint wp = (Waypoint) other.gameObject.GetComponent<Waypoint>();
            wp.Disinfect(Time.fixedDeltaTime);
        }
    }

}
