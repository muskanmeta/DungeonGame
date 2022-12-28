using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform playerTransform;
    public float boundX = 0.35f;
    public float boundY = 0.15f;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;
        float deltaX = playerTransform.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (playerTransform.position.x > transform.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        float deltaY = playerTransform.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (playerTransform.position.y > transform.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
