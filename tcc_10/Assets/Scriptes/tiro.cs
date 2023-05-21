using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiro : MonoBehaviour
{
   
    private void Start()
    {

        Destroy(gameObject, 2);

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "inimigoR") {

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "DestroiMunicao") {

            Destroy(gameObject);
        }
    }
}
