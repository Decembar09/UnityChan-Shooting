using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBase : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    /// <summary>
    /// 点数表示の更新
    /// </summary>
    /// <param name="score"></param>
    public void SetScore(int score)
    {
        _scoreText.text = "SCORE:" + score;
    }


}

