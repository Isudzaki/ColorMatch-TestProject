using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI hightscoreDisplay;

    private void Start()
    {
        hightscoreDisplay.text = PlayerPrefs.GetFloat("hightscore", 0).ToString();
    }
}
