using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GM : MonoBehaviour
{
    public static GM instanse;

    [SerializeField]
    private TextMeshProUGUI scoreDisplay;
    [SerializeField]
    private TextMeshProUGUI hightscoreDisplay;

    public float score;

    private bool press = false;

    [HideInInspector]
    public bool gameOnPause = false;

    private void Awake()
    {
        instanse = this;
    }

    private void Update()
    {
        scoreDisplay.text = score.ToString();
        hightscoreDisplay.text = PlayerPrefs.GetFloat("hightscore", 0).ToString();

        if (score > PlayerPrefs.GetFloat("hightscore", 0))
        {
            PlayerPrefs.SetFloat("hightscore", score);
        }
    }

    public void PauseGame()
    {
        if (!press)
        {
            press = true;
            gameOnPause = true;
            Time.timeScale = 0f;
        } else {
            press = false;
            gameOnPause = false;
            Time.timeScale = 1f;
        }
    }

}
