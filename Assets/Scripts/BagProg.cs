using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagProg : MonoBehaviour
{
    public GameManager manager;
    public AudioSource nice;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Bad")
        {
            manager.DecreaseLives();
            Destroy(collision.gameObject);

        }


        if (collision.gameObject.tag == "NiceEgg")
        {
            manager.AddScore(1);
            Destroy(collision.gameObject);
            nice.Play();
        }
    }

}
