using UnityEngine;
using System.Collections;

public class rollingObstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	 // transform.Rotate(Vector3.forward*Time.deltaTime*270);
	  //transform.Translate(Vector3.left*Time.deltaTime*3);
		transform.Rotate(Vector3.forward*Time.deltaTime*270);
	}
	
	void OnTriggerEnter(Collider other) {
        if(other.name=="Player"){
			Destroy(gameObject);
		}
		
    }
	/*
	void OnTriggerExit(Collider other) {
		//if(other.tag=="Floor")
        transform.rigidbody.useGravity=true;
		Debug.Log(other.name);
    }
	*/
	
}
