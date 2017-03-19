using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
