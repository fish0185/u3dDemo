using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
	   if(transform.position.x<-100)
			Destroy(gameObject);
	}
}
