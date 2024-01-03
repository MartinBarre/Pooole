using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 offset;

    private Vector3 _velocity;

    private void Update()
    {
        if (GameManager.Instance.cameraFollowPlayer)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref _velocity, timeOffset);
        }
    }
}
