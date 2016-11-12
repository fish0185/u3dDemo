using UnityEngine;
using System.Collections;

public class floatPlatform : MonoBehaviour {

	// Use this for initialization
	
	public GameObject targetA;
	public GameObject targetB;
	public float speed=0.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	  float weight=Mathf.Cos(Time.time*speed*2*Mathf.PI)*0.5f+0.5f;
	  
	  transform.position=targetA.transform.position*weight+targetB.transform.position*(1-weight);
	}
	
	void OnCollisionEnter(Collision collision){
	  if(collision.gameObject.name=="Player")
			Debug.Log("yest");
		  collision.gameObject.transform.parent=gameObject.transform;
	}
	
	void OnCollisionExit(Collision collision){
	  if(collision.gameObject.name=="Player")
			Debug.Log("yest");
		  collision.gameObject.transform.parent=null;
	}
}
