using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	
    // Start is called before the first frame update
    void Start()
	{
		_scoreText.text = "Score: " + 0;
		_gameOver.gameObject.SetActive(false);
	    
    }

	public void UpdateScore(int playerScore)
	{
		_scoreText.text = "Score: " + playerScore.ToString();
	}
	
	public void UpdateLives(int currentLives)
	{
		
		_LivesImg.sprite = _liveSprites[currentLives];
		
		if(currentLives == 0)
		{
			StartCoroutine(BlinkingGameOver());
		}
	}
	
	IEnumerator BlinkingGameOver()
	{
		while(true)
		{
			yield return new WaitForSeconds(.5f);
			_gameOver.gameObject.SetActive(true);			
			yield return new WaitForSeconds(.5f);
			_gameOver.gameObject.SetActive(false);			

		}

	}
	
}


