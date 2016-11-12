using UnityEngine;
using System.Collections;

public class MoveBullet : MonoBehaviour {
	public float speed;
	public float swingDistance;
	public float swingSpeed;
		//Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale!=0){
		  float DelteY=Mathf.Sin(Time.time*swingSpeed)*swingDistance;
		  float x=transform.position.x+speed*Time.deltaTime;
		  transform.position=new Vector3(x,transform.position.y+DelteY,transform.position.z);
		}
	}
}
