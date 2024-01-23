using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : MonoBehaviour
{
    [SerializeField] AudioSource musica1;
    [SerializeField] AudioSource musica2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(musica2.mute)
            {
                musica1.mute = true;
                musica2.mute = false;
                musica2.Play();
            }

        }
    }
}
