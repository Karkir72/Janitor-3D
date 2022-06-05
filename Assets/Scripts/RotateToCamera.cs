using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private Transform _target;

    private void Start()
    {
        _target = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(_target);
    }
}
