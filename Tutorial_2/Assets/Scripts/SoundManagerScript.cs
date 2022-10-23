using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
     public static AudioClip WinSound;

     static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
     //Sound pulled form Resources folder
    {
        WinSound = Resources.Load<AudioClip>("WinMusic");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
         switch (clip)
        {
            case "WinMusic":
                audioSrc.PlayOneShot(WinSound);
                break;
        }
    }
}
