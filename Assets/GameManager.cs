using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int coins;
    public Text coinsText;
    public int score;
    public Text scoreText;
    public GameObject restartButton;

    private void Awake()
    {
        Time.timeScale = 1f;
        restartButton.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score = (int)Time.timeSinceLevelLoad;
        scoreText.text = "Score : " + score.ToString();
        coinsText.text = "Coins : " + coins.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
