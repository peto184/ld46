using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disinfect : MonoBehaviour
{
    public AudioClip spray;
    public AudioSource audioSource;

    void Start()
    {
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.clip = spray;
        audioSource.volume = 0.35f;
        audioSource.pitch = 1.2f;
        audioSource.Play();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Waypoint") {
            Waypoint wp = (Waypoint) other.gameObject.GetComponent<Waypoint>();
            wp.Disinfect(Time.fixedDeltaTime);
        }
    }

}
