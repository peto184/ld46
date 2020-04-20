using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton class for persisting the music during the scene change
public class MusicManager : MonoBehaviour
{
    private static MusicManager instance = null;
    public static MusicManager Instance {
        get { return instance; }
    }

    void Awake() {
        Debug.Log($"musicmanager ${this.gameObject.name}");
        if (instance != null && instance != this) {
            Debug.Log("Desotring duplicate Music manager.");
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        //this.Play();
    }

    // Update is called once per frame
    public void Play(){
        var audioSource = this.gameObject.GetComponent<AudioSource>(); 
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
