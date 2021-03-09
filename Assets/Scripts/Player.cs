using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class Player : MonoBehaviour
{
	[SerializeField]
	private float _speed = 3.5f;
	
	[SerializeField]
	private GameObject _laserPrefab;
	
	[SerializeField]
	private float _fireRate = 0.5f;
	private float _canFire = -1;
	
	[SerializeField]
	private int _lives = 3;
	
	private Spawn_Manager _spawnManager;
	
	//powerups
	[SerializeField]
	private bool _tripleShotEnabled = false;
	[SerializeField]
	private GameObject _tripleShotPrefab;
	
    void Start()
    {
	    transform.position = new Vector3(0,-3.0f, 0);
	    
	    if(_spawnManager != null)
	    {
	    	Debug.LogError("SpawnManger is Null");
	    }
	    _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
	    
    }

    void Update()
	{
		CalculateMovement();
		
		if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
		{
			FireLaser();
		}
		
	}
    
	void CalculateMovement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticlInput = Input.GetAxis("Vertical");
		
		Vector3 direction = new Vector3(horizontalInput, verticlInput, 0);
		transform.Translate(direction * _speed * Time.deltaTime);
		
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
	
	
	public void Damage()
	{
		_lives = _lives - 1;
		
		if(_lives < 1)
		{
			_spawnManager.OnPlayerDeath();
			Destroy(this.gameObject);
		}
	}
}
