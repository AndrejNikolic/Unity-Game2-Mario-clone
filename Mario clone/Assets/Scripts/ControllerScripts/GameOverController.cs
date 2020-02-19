using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    private Text endScore;

    private void Awake()
    {
        endScore = GameObject.Find("ScoreText").GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        endScore.text = "YOUR SCORE IS " + CoinScript.score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
        CoinScript.score = 0;
    }
}
