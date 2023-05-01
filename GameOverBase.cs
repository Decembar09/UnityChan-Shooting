using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBase : MonoBehaviour
{

	[SerializeField] private GameObject _gameOverText;
	private float _gameOverTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _gameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_gameOverText.activeSelf){
		_gameOverTimer +=Time.deltaTime;
		    if(_gameOverTimer >= 3.0f)
		    {
			    SceneManager.LoadScene("Title");
		    }
	    }
    }
	public void GameOverStart(){
		_gameOverText.SetActive(true);
	}

}
