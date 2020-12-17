using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PHead : MonoBehaviour
{
    randomSpawn Spawner;
    Movement Player;
    [Range(0.0f, 1.0f)]
    public float space = 0.5f;
    private Transform head;
    private Vector3 Velocity;
    [Range(0.0f, 1.0f)]
    public float rotationSpeed;
    StartGame StartGame;
    // Start is called before the first frame update
    void Start()
    {
        Spawner = GameObject.FindWithTag("Spawner").GetComponent<randomSpawn>();
        Player = GameObject.FindWithTag("Player").GetComponent<Movement>();
        head = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        StartGame = GameObject.FindGameObjectWithTag("StartGame").GetComponent<StartGame>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, head.position, ref Velocity, space);
        transform.rotation = Quaternion.Slerp(transform.rotation, head.rotation, rotationSpeed);
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.collider.tag == "Food")
        {
            if (Player.bodyParts.Count > 0)
            {
                int lastIndex = Player.bodyParts.Count - 1;
                Transform lastBodyParts = Player.bodyParts[lastIndex].transform;
                Player.bodyParts.RemoveAt(lastIndex);
                Destroy(lastBodyParts.gameObject);
            }else if (Player.bodyParts.Count <= 0)
            {
                Destroy(head.gameObject);
                StartGame.Freeze();
            }
        }
    }
}
