using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMan : MonoBehaviour
{   
    public AudioSource audio;
    private static MusicMan internal_instance;
    public static MusicMan instance {
        get {
            if(internal_instance == null) {
                internal_instance = GameObject.FindObjectOfType<MusicMan>();

                DontDestroyOnLoad(internal_instance.gameObject);
            }
            return internal_instance;
        }
    }

    private void Awake() {
        if (internal_instance == null) {

            internal_instance = this;
            DontDestroyOnLoad(this);
        } else {
            if (this != internal_instance) {
                Destroy(this.gameObject); 
            }
        }
    }

    public void Play() {

    }

    // Start is called before the first frame update
    void Start()
    {
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
