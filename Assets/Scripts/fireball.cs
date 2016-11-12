using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {
	Hashtable ht=new Hashtable();
	Hashtable ht2=new Hashtable();
	void Awake(){
	  ht.Add("y",10);
	  ht.Add("time",2);
	  ht.Add("easetype",iTween.EaseType.easeInOutSine);
	  ht.Add("oncomplete","down");
	  ht2.Add("y",-10);
	  ht2.Add("time",2);
	  ht2.Add("easetype",iTween.EaseType.easeInOutSine);
	  ht2.Add("oncomplete","up");
	}
	
	void Start () {
	  iTween.MoveBy(gameObject,ht);
	}
	
	void up(){
		 iTween.MoveBy(gameObject,ht);
	}
	
	// Update is called once per frame
	void Update () {
	 
	}
	
	void down(){
     // iTween.MoveBy(gameObject,{"time":3});
		 iTween.MoveBy(gameObject,ht2);
	}
}
