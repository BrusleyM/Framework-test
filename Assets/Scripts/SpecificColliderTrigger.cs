using UnityEngine;

public class SpecificColliderTrigger : MonoBehaviour
{
    public Collider targetCollider; // Assign the specific collider in the inspector or via script

    private void OnTriggerEnter(Collider other)
    {
        if (other == targetCollider)
        {
            other.GetComponent<Grounded>().OnGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == targetCollider)
        {
            // Handle the trigger exit event for the specific collider
            Debug.Log("Trigger exit event detected by the specific collider!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == targetCollider)
        {
            // Handle the trigger stay event for the specific collider
            Debug.Log("Trigger stay event detected by the specific collider!");
        }
    }
}
