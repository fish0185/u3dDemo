using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	public float speed;
		//Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	  float x=transform.position.x+speed*Time.deltaTime;
	  transform.position=new Vector3(x,transform.position.y,transform.position.z);
	}
}
