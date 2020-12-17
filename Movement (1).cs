using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    [Range(0.0f, 1.0f)]
    public float rotationSpeed = 0.1f;
    private Rigidbody2D rb;
    Joystick joystick;
    Vector3 rotate;
    public float boost = 5f;
    public Transform Body;
    public Transform PHead;
    public List<Transform> bodyParts = new List<Transform>();
    public List<Transform> HeadParts = new List<Transform>();
    Text Score;
    public int ScoreValue = 0;
    public float normalSpeedForBody;
    public float BoostSpeedForBody;
    SpriteRenderer Renderer;
    private Transform head;
    public float width = 1f;
    public bool GameOver = false;
    public GameObject body;
    StartGame StartGame;
    public Text HighScore;

    void Start()
    {
        joystick = GameObject.FindWithTag("Joystick").GetComponent<Joystick>();
        rb = GetComponent<Rigidbody2D>();
        rotate = Vector3.zero;
        Score = GameObject.FindWithTag("Score").GetComponent<Text>();
        Renderer = body.GetComponent<SpriteRenderer>();
        head = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        StartGame = GameObject.FindGameObjectWithTag("StartGame").GetComponent<StartGame>();
        HighScore.text ="High Score : " + PlayerPrefs.GetInt("HighScores", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score : " + ScoreValue;
        if (ScoreValue > PlayerPrefs.GetInt("HighScores", 0))
        {
            PlayerPrefs.SetInt("HighScores", ScoreValue);
            HighScore.text = "High Score : " + ScoreValue.ToString();
        }
        if (HeadParts.Count > 5)
        {
            if (bodyParts.Count > 0)
            {
                StartCoroutine(LoseBodyParts());
            }else if (bodyParts.Count <= 0)
            {
                Destroy(gameObject);
                StartGame.Freeze();
            }
        }
        if (bodyParts.Count <= 0 && GameOver == true)
        {
            Destroy(gameObject);
            StartGame.Freeze();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * moveSpeed;
        rotate = new Vector3(joystick.Horizontal, joystick.Vertical);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotate);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }
    }
    public void Boost()
    {
        moveSpeed += boost;
        foreach (Transform bodyParts_x in bodyParts)
        {
            bodyParts_x.GetComponent<bodyMovement>().space = BoostSpeedForBody;
        }
    }

    public void normal()
    {
        moveSpeed -= boost;
        foreach (Transform bodyParts_x in bodyParts)
        {
            bodyParts_x.GetComponent<bodyMovement>().space = normalSpeedForBody;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Food")
        {
            if (bodyParts.Count == 0)
            {
                Vector3 currentPos = transform.position;
                Quaternion currentRot = Quaternion.LookRotation(Vector3.forward, rotate);
                Transform newbody = Instantiate(Body, currentPos, currentRot);
                bodyParts.Add(newbody);
                Renderer.sortingOrder = bodyParts.Count;
            }
            else
            {
                Vector3 currentPos = bodyParts[bodyParts.Count - 1].position;
                Quaternion currentRot = bodyParts[bodyParts.Count - 1].rotation;
                Transform newbody = Instantiate(Body, currentPos, currentRot);
                bodyParts.Add(newbody);
                Renderer.sortingOrder = bodyParts.Count * -1;
            }
            ScoreValue++;
        }

        if (collision.collider.tag == "PFood")
        {
            if (HeadParts.Count >= 0)
            {
                Vector3 currentPos = transform.position;
                currentPos += Vector3.right;
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, head.rotation, rotationSpeed);
                Transform newHead = Instantiate(PHead, currentPos, targetRotation);
                HeadParts.Add(newHead);
            }
        }

        if (collision.collider.tag == "HFood")
        {
            int LastIndex = HeadParts.Count - 1;
            Transform lastHeadParts = HeadParts[LastIndex].transform;
            HeadParts.RemoveAt(LastIndex);
            Destroy(lastHeadParts.gameObject);
        }
    }

    IEnumerator LoseBodyParts()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            int LastIndex = bodyParts.Count - 1;
            Transform lastHeadParts = bodyParts[LastIndex].transform;
            bodyParts.RemoveAt(LastIndex);
            Destroy(lastHeadParts.gameObject);
            if (bodyParts.Count < 1)
            {
                GameOver = true;
                break;
            }
        }
    }
}
