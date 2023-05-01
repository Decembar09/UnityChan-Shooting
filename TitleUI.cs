using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
///�^�C�g��UI�̐���@���mainscene�ւ̑J��
/// </summary>

public class TitleUI : MonoBehaviour
{
    [SerializeField]private Button _gameStartButton;
    [SerializeField]private Animator _playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnimation.SetBool("Opening",true);
        _gameStartButton.onClick.AddListener(() =>
        {
        //mainscene�ւ̑J��
            SceneManager.LoadScene("main");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
