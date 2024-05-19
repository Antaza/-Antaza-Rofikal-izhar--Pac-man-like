using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text _scoreText;
    int _score;
    int _maxScore;

    public void UpdateUI()
    {
        _scoreText.text = "Score : " + _score + "/" + _maxScore;
    }

    public void SetMaxScore(int value)
    {
        _maxScore = value;
        UpdateUI();
    }

    public void AddScore(int value)
    {
        _score += value;
        UpdateUI();
    }
    void Start()
    {
        UpdateUI();
    }
}
