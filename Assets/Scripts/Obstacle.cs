using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	Player player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter(Collider hit){
		Debug.Log(hit.name+" hit Obstacle");
		if(hit.name=="Player")
		Time.timeScale=0.0f;
	}
	
	public void OnCollisionEnter(Collision collision) {
		if(collision.collider.name=="Player"){
		player=collision.gameObject.GetComponent<Player>();
		Player.isAlive=false;
        Debug.Log("Play hit you");
		//Time.timeScale=0.0f;
		}
        
    }
}
