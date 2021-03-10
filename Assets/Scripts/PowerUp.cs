using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	[SerializeField]
	private float _speed = 3.0f;
	
	//0 = Triple Shot
	//1 = Speed
	//2 = Shields
	
	[SerializeField]
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
				if(_powerUpID == 0)
				{
					player.TripleShotActive();					
				}
				else if(_powerUpID == 1)
				{
					Debug.Log("I have a need for speed");					
				}
				else if(_powerUpID == 2)
				{
					Debug.Log("Shields ON...Ramming Speed!!!");					
				}
				
		
			}
			Destroy(this.gameObject);
		}
	}
    

}


