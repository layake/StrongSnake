using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class scoreScript : MonoBehaviour
{
    [HideInInspector]
    public Snake Snake;
    public TextMeshProUGUI textscore;
    // Start is called before the first frame update
    void Start()
    {
        //textscore = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textscore.text = "Score: " + Snake.Score.ToString();
    }
}
