using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject GameOverMenu;
    public GameObject GameOverDisable;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    public void Freeze()
    {
        Time.timeScale = 0;
        GameOverMenu.SetActive(true);
        GameOverDisable.SetActive(false);
    }
}
