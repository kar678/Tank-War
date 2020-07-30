using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    AudioSource sourceSound;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        sourceSound = GetComponent<AudioSource>();

        if(sourceSound)
        {
            sourceSound.clip = explosionSound;
            sourceSound.Play();
        }

        Animation shrink = gameObject.GetComponent<Animation>();
        Destroy(gameObject, shrink.clip.length);
    }

}
