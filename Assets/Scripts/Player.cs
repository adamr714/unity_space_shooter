using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private float _speed = 3.5f;
	private float _speedMultiplier = 4.0f;


	[SerializeField]
	private GameObject _laserPrefab;
	
	[SerializeField]
	private float _fireRate = 0.5f;
	private float _canFire = -1;
	
	[SerializeField]
	private int _lives = 3;
	
	private Spawn_Manager _spawnManager;
	
	//powerups
	private bool _tripleShotEnabled = false;	
	private bool _speedBoostEnabled = false;
	private bool _shieldActive = false;
	

	[SerializeField]
	private GameObject _shieldPrefab;
	
	[SerializeField]
	private GameObject _tripleShotPrefab;
	
	[SerializeField]
	private int _score;
	private UIManager _uiManager;
	
	//damage
	[SerializeField]
	private GameObject _leftEnginePrefab;
	
	[SerializeField]
	private GameObject _rightEnginePrefab;

	
	//audio
	[SerializeField]
	private AudioClip _laserSoundClip;
	
	[SerializeField]
	private AudioSource _audioSource;
	
	
    void Start()
    {
	    transform.position = new Vector3(0,-3.0f, 0);
	    _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
	    _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	    _audioSource = GetComponent<AudioSource>();
	    _audioSource.clip = _laserSoundClip;
	    
	    if(_spawnManager != null)
	    {
	    	Debug.LogError("SpawnManger is Null");
	    }
	    
	    if(_uiManager == null)
	    {
	    	Debug.LogError("UI Manager is Empty");
	    }

	    
    }

    void Update()
	{
		CalculateMovement();
		
		if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
		{
			FireLaser();
		}
		
		if(Input.GetKey(KeyCode.LeftShift))
		{
			
			Debug.Log("Left Shift is Down");
			_speed = 10;
		}
		else 
		{
	
			_speed = 3.5f;
		}
		
	}
    
	void CalculateMovement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticlInput = Input.GetAxis("Vertical");
		
		Vector3 direction = new Vector3(horizontalInput, verticlInput, 0);
		
		if(_speedBoostEnabled == false)
		{
			transform.Translate(direction * _speed * Time.deltaTime);			
		}
		else if(_speedBoostEnabled == true)
		{
			transform.Translate(direction * (_speed * _speedMultiplier) * Time.deltaTime);			
		}

		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 0), 0);
		
		if(transform.position.x >=11)
		{
			transform.position = new Vector3(-11, transform.position.y, 0);
		}
		else if(transform.position.x <= -11)
		{
			transform.position = new Vector3(11, transform.position.y, 0);
		}
	}
	
	void FireLaser()
	{
		_canFire = Time.time + _fireRate;
		
		if(_tripleShotEnabled == true)
		{
			Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
		}
		else 
		{
			Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
		}
		
		_audioSource.Play();
	}

	public void TripleShotActive()
	{
		_tripleShotEnabled = true;
		StartCoroutine(TripleShotPowerDownRoutine());
	}

	IEnumerator TripleShotPowerDownRoutine()
	{
		yield return new WaitForSeconds(5);
		_tripleShotEnabled = false;
	}
	
	public void SpeedBoostActive()
	{
		_speedBoostEnabled = true;
		StartCoroutine(SpeedBoostPowerDownRoutine());
	}
	
	IEnumerator SpeedBoostPowerDownRoutine()
	{
		yield return new WaitForSeconds(5);
		_speedBoostEnabled = false;
	}
	
	
	public void ShieldActive()
	{
		_shieldActive = true;
		_shieldPrefab.SetActive(true);
	}
	
	
	public void AddScore(int points)
	{
		_score += points;
		_uiManager.UpdateScore(_score);
	}

	

	public void Damage()
	{
	
		if(_shieldActive == true)
		{
			_shieldActive = false;
			_shieldPrefab.SetActive(false);
			return;
		}
		else if (_shieldActive == false)
		{
			_lives = _lives - 1;
			_uiManager.UpdateLives(_lives);
		}
		
		if(_lives < 1)
		{
			_spawnManager.OnPlayerDeath();
			Destroy(this.gameObject);
		}
		
		if(_lives == 2)
		{
			_rightEnginePrefab.SetActive(true);
		}
		else if(_lives == 1)
		{
			_leftEnginePrefab.SetActive(true);
		}
	}
	
	public void Booster()
	{
		_speed = 12;
	}
}
