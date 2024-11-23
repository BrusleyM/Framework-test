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
    private GameObject lookAtTarget; // Object to look at
    public float lookAtSpeed = 2f; // Speed of looking at the target

    public float idleAmplitude = 0.5f; // Amplitude of the idle floating
    public float idleFrequency = 1f; // Frequency of the idle floating
    private float idleStartTime; // Start time for idle animation
    private Vector3 initialPosition; // Initial position for idle animation

    [SerializeField]
    private float _minDistToTarget = 5f; // Minimum distance to the lookAtTarget
    [SerializeField]
    private float maxDistToTarget = 10f; // Maximum distance to the lookAtTarget

    public Vector3 positionOffset = new Vector3(-5f, 0f, 5f); // Offset relative to the lookAtTarget

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
            MaintainDistance();
            SmoothLookAtTarget();
            IdleFloat();
        }
    }

    public void MoveTowardsTarget()
    {
        // Calculate the direction to the target position
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Update target position if close enough to the current target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (currentLocationIndex < locations.Length)
                targetPosition = locations[currentLocationIndex].position;
        }

        // Handle rotation
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion tiltRotation = Quaternion.Euler(tiltAngle, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, tiltRotation, speed * Time.deltaTime);
        }

        // Check if destination is reached
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
        // Smoothly rotate to look at the lookAtTarget
        Vector3 direction = (lookAtTarget.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, lookAtSpeed * Time.deltaTime);
    }

    void MaintainDistance()
    {
        Vector3 forwardOffset = lookAtTarget.transform.forward * positionOffset.z;
        Vector3 rightOffset = lookAtTarget.transform.right * positionOffset.x;
        Vector3 upOffset = lookAtTarget.transform.up * positionOffset.y;

        Vector3 desiredPosition = lookAtTarget.transform.position + forwardOffset + rightOffset + upOffset;

        // Ensure the desired position is within the min and max distance range
        float currentDistance = Vector3.Distance(desiredPosition, lookAtTarget.transform.position);
        if (currentDistance < _minDistToTarget)
        {
            desiredPosition = lookAtTarget.transform.position + (desiredPosition - lookAtTarget.transform.position).normalized * _minDistToTarget;
        }
        else if (currentDistance > maxDistToTarget)
        {
            desiredPosition = lookAtTarget.transform.position + (desiredPosition - lookAtTarget.transform.position).normalized * maxDistToTarget;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
    }

    void IdleFloat()
    {
        // Idle floating animation
        float elapsedTime = Time.time - idleStartTime;
        float newY = initialPosition.y + Mathf.Sin(elapsedTime * idleFrequency) * idleAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}