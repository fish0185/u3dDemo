using UnityEngine;
using System.Collections;

public class spinfan : MonoBehaviour {
    public float speed=280;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   transform.Rotate(Vector3.forward,speed*Time.deltaTime,Space.Self);
		//Debug.Log(transform.position);
	}
}
