using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	// Use this for initialization
	//public GameObject player;
	float width;
	float height;
	float footHeight;
    Transform foot;
	//float delay=0.5f;
//	float totalTime=0;
	//bool movingDown=false;
	void Start () {
	
		//foot=player.transform.GetChild(1);
	
	}
	
	// Update is called once per frame
	void Update () {
	  
		/*if(Input.GetKeyDown(KeyCode.S)){
			
		  transform.collider.isTrigger=true;
		  movingDown=true;
			
		}
		
		if(!movingDown){
		if(foot.position.y>transform.position.y+0.3f)
		{
			transform.collider.isTrigger=false;
		}else{
				transform.collider.isTrigger=true;
			}
		}else{
		  if(totalTime>delay){
			  movingDown=false;
			  totalTime=0;
			}
			totalTime+=Time.deltaTime;
		}
		*/
	}
	
	  void OnTriggerExit(Collider other) {
		if(other.name=="rollingObstacle")
          other.transform.GetComponent<Rigidbody>().useGravity=true;
//		Debug.Log(other.name);
    }
	
	void OnTriggerStay(Collider other) {
		if(other.name=="rollingObstacle"){
            other.transform.GetComponent<Rigidbody>().velocity=new Vector3(other.transform.GetComponent<Rigidbody>().velocity.x,0,other.transform.GetComponent<Rigidbody>().velocity.z);
			other.transform.GetComponent<Rigidbody>().useGravity=false;
		}
		//Debug.Log(other.name);
    }
	
}