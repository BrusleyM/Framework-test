using System.Collections;
using System.Collections.Generic;
using Framework.CheckPoints;
using UnityEngine;

namespace FrameworkTest
{
    public class SnapObject : MonoBehaviour
    {
        [SerializeField]
        private string _objectName;

        private void OnCollisionEnter(Collision collision)
        {
            var _object = collision.gameObject;
            if (_object.name == _objectName)
            {
                _object.transform.position = transform.position;
                _object.transform.rotation=transform.rotation;
                transform.GetComponent<CheckPoint>().MarkAsChecked();
            }
        }
    }
}
