﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodDestroy : MonoBehaviour
{
    public GameObject Food;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(Food);
        }
        if (collision.collider.tag == "PHead")
        {
            Destroy(Food);
        }
    }
}
