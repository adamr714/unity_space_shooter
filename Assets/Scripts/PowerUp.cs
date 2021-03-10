﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	[SerializeField]
	private float _speed = 3.0f;
	
	[SerializeField] //0 = Triple Shot; 1 = Speed; 2 = Shields
	private int _powerUpID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
    	
		transform.Translate(Vector3.down * _speed * Time.deltaTime);
		
		if(transform.position.y <= -6)
		{
			Destroy(this.gameObject);
		}
		
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if(other.tag == "Player") 
		{
			Player player = other.transform.GetComponent<Player>();
			
			if(player != null)
			{
				switch(_powerUpID)
				{
				case 0:
					player.TripleShotActive();
					break;
				case 1:
					Debug.Log("I have a need for speed");					
					break;
				case 2:
					Debug.Log("Shields ON...Ramming Speed!!!");					
					break;
				}
			}
			Destroy(this.gameObject);
		}
	}
}


