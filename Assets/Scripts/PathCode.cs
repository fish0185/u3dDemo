using UnityEngine;
using System.Collections;

public class PathCode : MonoBehaviour {

	// Use this for initialization
	private GameObject player;
	float width;
	float height;
	float footHeight;
    Transform foot;
	float delay=0.3f;
	float totalTime=0;
	bool movingDown=false;
	public static bool jumpdown=false;
	void Start () {
	    player=GameObject.FindGameObjectWithTag("Player");
		foot=player.transform.Find("foot");
	
	}
	
	// Update is called once per frame
	void Update () {
	  
		if(Input.GetKeyDown(KeyCode.S)||jumpdown){
			
		  transform.GetComponent<Collider>().isTrigger=true;
		  movingDown=true;
			
		}
		
		
		
		
		
		
		
#if UNITY_IPHONE
		
		if(Input.touchCount>0){
			Touch touch=Input.GetTouch(0);
			if(touch.phase==TouchPhase.Moved){
			  Vector2 touchPositionDelta=touch.deltaPosition;
			  float tt=Vector2.Dot(touchPositionDelta,Vector2.up);
			  if(tt>0){
					transform.GetComponent<Collider>().isTrigger=true;
		  movingDown=true;
				}
			}
		}
#endif
		
		
		if(!movingDown){
		if(foot.position.y>transform.position.y+0.3f)
		{
			transform.GetComponent<Collider>().isTrigger=false;
		}else{
				transform.GetComponent<Collider>().isTrigger=true;
			}
		}else{
		  if(totalTime>delay){
			  movingDown=false;
				jumpdown=false;
			  totalTime=0;
			  //transform.collider.isTrigger=false;
			}
			totalTime+=Time.deltaTime;
		}
		
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