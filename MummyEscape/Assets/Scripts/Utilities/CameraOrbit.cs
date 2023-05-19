using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;

    private Vector3 offset;
    private Vector3 rotatePosition;
    private float angle;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        //calculate rotate position 
        angle = speed * Time.time;
        rotatePosition = target.position + Quaternion.AngleAxis(angle, Vector3.up) * offset;
        rotatePosition.y = transform.position.y;

        //Move the camera to rorate position
        transform.position = rotatePosition;

        //camera look at target
        transform.LookAt(target);
    }
}
