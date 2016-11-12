using UnityEngine;
using System.Collections;

public class RenderQuence : MonoBehaviour {

	// Use this for initialization
	public int order=10;
	void Start () {
	  GetComponent<Renderer>().material.renderQueue=order;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
