using UnityEngine;
using System.Collections;

public class AutoColl : MonoBehaviour {

	// Use this for initialization

	private GameObject player;
	private float speed=4;
	public float powerupTime=0;
	public float totalTime=0;
	private bool movedFlag=false;
	//private Vector3 Oposition;
	void Start () {
	  player=GameObject.FindGameObjectWithTag("Player");
	 // Oposition=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//moved=(Oposition!=transform.position)?false:false;
		//Debug.Log(renderer.isVisible);
	   if(powerupTime>0){
		
			 if(GetComponent<Renderer>().isVisible){
		    
			 
				movedFlag=true;
		 	  
			}	  
		   	
		
			powerupTime-=Time.deltaTime;
		}
		if(movedFlag){
		   if(GetComponent<Renderer>().isVisible){
		    
			  Vector3 direction=player.transform.position-transform.position;
		
		      transform.position+=direction*Time.deltaTime*speed;
				
			}
		}
	 
	}
}
