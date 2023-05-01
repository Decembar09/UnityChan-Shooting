using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBase : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    /// <summary>
    /// �_���\���̍X�V
    /// </summary>
    /// <param name="score"></param>
    public void SetScore(int score)
    {
        _scoreText.text = "SCORE:" + score;
    }


}

