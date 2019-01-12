using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollision : MonoBehaviour
{
    public playerController controller;

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.collider.tag;
        if (tag.Equals("enemy"))
        {
            controller.enabled = false;
            GetComponent<Renderer>().enabled = false;
        }
    }
}
