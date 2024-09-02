using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform[] locations; // Array of locations to move to
    public float speed = 5f; // Speed of movement
    public float tiltAngle = 15f; // Tilt angle in degrees
    public int currentLocationIndex = 0; // Current location index
    private Vector3 targetPosition; // Current target position
    private bool destinationReached = false;
    [SerializeField]
    private GameObject lookAtTarget;
    public float lookAtSpeed = 2f; // Speed of looking at the target
    public float idleAmplitude = 0.5f; // Amplitude of the idle floating
    public float idleFrequency = 1f; // Frequency of the idle floating
    private float idleStartTime; // Start time for idle animation
    private Vector3 initialPosition; // Initial position for idle animation

    void Start()
    {
        initialPosition = transform.position;
        idleStartTime = Time.time;

        if (locations.Length > 0)
        {
            targetPosition = locations[currentLocationIndex].position;
        }
    }

    void Update()
    {
        if (!destinationReached)
        {
            MoveTowardsTarget();
        }
        else
        {
            SmoothLookAtTarget();
            IdleFloat();
        }
    }

    public void MoveTowardsTarget()
    {

        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if(currentLocationIndex <locations.Length)
            targetPosition = locations[currentLocationIndex].position;
        }

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion tiltRotation = Quaternion.Euler(tiltAngle, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, tiltRotation, speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            destinationReached = true;
            idleStartTime = Time.time; // Reset idle start time
            initialPosition = transform.position; // Reset initial position for idle
        }
        else
        {
            destinationReached = false;
        }
    }

    void SmoothLookAtTarget()
    {
        Vector3 direction = (lookAtTarget.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, lookAtSpeed * Time.deltaTime);
    }

    void IdleFloat()
    {
        float elapsedTime = Time.time - idleStartTime;
        float newY = initialPosition.y + Mathf.Sin(elapsedTime * idleFrequency) * idleAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
