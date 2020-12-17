using System.Collections.Generic;
using UnityEngine;

public class randomSpawn : MonoBehaviour
{
    public GameObject[] Food;
    public float width;
    public float height;
    public float maxTime = 1;
    private float time = 0;
    public int MaxFood = 5;
    public int food = 0;
    Movement Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (food < MaxFood)
        {
            if (time > maxTime)
            {
                int rand = Random.Range(0, Food.Length);
                GameObject newFood = Instantiate(Food[rand]);
                newFood.transform.position = transform.position + new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0);
                time = 0;
                food++;
            }
            time += Time.deltaTime;
        }
    }
}
