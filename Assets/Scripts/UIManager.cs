using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private Text _scoreText;
	
	[SerializeField]
	private Sprite[] _liveSprites;
	
	[SerializeField]
	private Image _LivesImg;
	
	[SerializeField]
	private Text _gameOver;
	
	[SerializeField]
	private Text _restartGame;
	
	[SerializeField]
	private Text _ammoText;
	
	private GameManager _gameManager;
	
    // Start is called before the first frame update
    void Start()
	{
		_scoreText.text = "Score: " + 0;
		_gameOver.gameObject.SetActive(false);
		_restartGame.gameObject.SetActive(false);
		_gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
		_ammoText.text = "Ammo: " + 15;
    }

	public void UpdateScore(int playerScore)
	{
		_scoreText.text = "Score: " + playerScore.ToString();
	}
	
	public void UpdateAmmo(int ammoNumber)
	{
		_ammoText.text = "Ammo: " + ammoNumber.ToString();
	}


	
	public void UpdateLives(int currentLives)
	{
		_LivesImg.sprite = _liveSprites[currentLives];
		
		if(currentLives == 0)
		{
			GameOverSequence();
		}
	}
	
	void GameOverSequence()
	{
		_gameOver.gameObject.SetActive(true);		
		_restartGame.gameObject.SetActive(true);
		StartCoroutine(BlinkingGameOver());
	}
	
	IEnumerator BlinkingGameOver()
	{
		while(true)
		{
			yield return new WaitForSeconds(.5f);
			_gameOver.gameObject.SetActive(true);			
			yield return new WaitForSeconds(.5f);
			_gameOver.gameObject.SetActive(false);
			_gameManager.GameOver();

		}

	}
	
}


