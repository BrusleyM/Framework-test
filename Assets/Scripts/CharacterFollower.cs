using UnityEngine;

public class CharacterFollower : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    public float followSpeed = 5f;

    private void Start()
    {
        offset = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position /*+ offset*/;
        targetPosition.y = transform.position.y;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}