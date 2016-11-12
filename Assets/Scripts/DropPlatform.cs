using UnityEngine;
using System.Collections;

public class DropPlatform : MonoBehaviour {

	// Use this for initialization
	float delay=0.05f;
	float time=0;
	bool start=false;
	void Start () {
	   start=false;
	}
	
	// Update is called once per frame
	void Update () {
	   if(transform.position.y<-100){
			//Destroy(this);
		Destroy(gameObject);
		}
		if(start){
		    if(time>delay){
			  if(!GetComponent<Rigidbody>()){
		        gameObject.AddComponent<Rigidbody>();
		      }
			}
			time+=Time.deltaTime;
		}
	}
	
	void OnCollisionExit(Collision collisionInfo) {
        //print("No longer in contact with " + collisionInfo.transform.name);
		start=true;
    }
}
