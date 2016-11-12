using UnityEngine;
using System.Collections;

public class SprintAddForce : MonoBehaviour {
	public int speed=15;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	  void OnTriggerEnter(Collider collider) {
        if(collider.name=="Player"){
		  //Debug.Log("Jump##########################");
		  collider.GetComponent<Rigidbody>().velocity=new Vector3(0,speed,0);
		}
    }
}
