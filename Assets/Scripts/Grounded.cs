using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    public bool OnGround = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Floor")
        {
            OnGround = true;
        }
    }
}
