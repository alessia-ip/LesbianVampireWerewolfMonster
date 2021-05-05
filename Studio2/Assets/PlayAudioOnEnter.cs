using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnEnter : MonoBehaviour
{
    public AudioSource music;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !music.isPlaying) {
            music.Play();
        }
    }
    void OnTriggerExit2D(Collider2D col) { 
     if (col.tag == "Player") {
            music.Stop();
        }
    }
}
