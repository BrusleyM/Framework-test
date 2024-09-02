using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkTest
{
    public class TestScript : MonoBehaviour
    {
        public bool TestBool;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Floor")
            {
                Debug.Log("Hit the ground. Collision");
            }  
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Floor")
            {
                Debug.Log("Hit the ground. Collider");
            }
        }
    }
}
