using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour {

	// Use this for initialization
	private float offset=(0.5f-0)*100;
	private float startX;
	void Start () {
	   startX=transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
//	  Debug.Log(transform.position);
		if((startX-transform.position.x)>=offset){
		  transform.position=new Vector3(transform.position.x+100,transform.position.y,transform.position.z);
		}
		
	}
}
