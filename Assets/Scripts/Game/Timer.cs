using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    Image timerBar;

    [SerializeField]
    private float maTime = 20f;
    private float timeLeft;

    [SerializeField] GameObject fadeInEndGame;
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maTime;
    }

    void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maTime;
        } else
        {
            fadeInEndGame.SetActive(true);
        }
    }
}
