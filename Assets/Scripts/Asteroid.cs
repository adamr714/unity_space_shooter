﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	[SerializeField]
	private float _rotateSpeed = 3.0f;
	
	[SerializeField]
	private GameObject _explosionPrefab;
	
	private Spawn_Manager _spawnManager;
	
    // Start is called before the first frame update
    void Start()
    {
    	_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        
    }

    // Update is called once per frame
    void Update()
    {
	    transform.Rotate(Vector3.forward *_rotateSpeed * Time.deltaTime);
    }
    
	//check for laser collisison of type trigger
	//instantiate explosion at my position.
	//destroy after 3 seconds
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Laser")
		{
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			Destroy(other.gameObject);
			_spawnManager.StartSpawning();
			Destroy(this.gameObject, 0.25f);

		}
	}
}
