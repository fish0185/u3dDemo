using UnityEngine;
using System.Collections;

public class PlateformGenerator : MonoBehaviour {
	private GameObject lastPlateform;
    //Vector3 lastPlateformPosition;
	Vector3 spawnPointPosition;
	public GameObject[] Plateform;
	// Use this for initialization
	void Start () {
	   spawnPointPosition=transform.position;
	   if(!lastPlateform){
			lastPlateform=Instantiate(Plateform[0],transform.position,Quaternion.identity) as GameObject;
			//lastPlateformPosition=lastPlateform.transform.position;
			lastPlateform.transform.parent=GameObject.Find("Level").transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	  float dist=Vector3.Distance(spawnPointPosition,lastPlateform.transform.position);
		//Debug.Log(dist+" @###########");
	  if(dist>40){
	      lastPlateform=Instantiate(Plateform[Random.Range(0,3)],transform.position,Quaternion.identity) as GameObject;
		  lastPlateform.transform.parent=GameObject.Find("Level").transform;
		}
			
	}
}
