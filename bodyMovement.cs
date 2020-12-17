using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class bodyMovement : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float speed = 0.5f;
    private Transform head;
    private int myOrder;
    private Vector3 Velocity;
    [Range(0.0f, 1.0f)]
    public float rotationSpeed;
    SpriteRenderer Renderer;

    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        head = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        for (int i = 0; i < head.GetComponent<Movement>().bodyParts.Count; i++)
        {
            myOrder = i;
        }
    }

    void FixedUpdate()
    {
        if (myOrder == 0)
        {
            transform.position = Vector3.SmoothDamp(transform.position, head.position, ref Velocity, speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, head.rotation, rotationSpeed);
            Renderer.sortingOrder = myOrder;
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, head.GetComponent<Movement>().bodyParts[myOrder - 1].position, ref Velocity, speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, head.GetComponent<Movement>().bodyParts[myOrder - 1].rotation, rotationSpeed);
            Renderer.sortingOrder = myOrder * -1;
        }
    }
}
