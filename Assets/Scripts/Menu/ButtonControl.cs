using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {

	// Use this for initialization
	GameObject player;
	public string functionName;
	float guiScale;
	private float baseScreenWidth=640;
	void Start () {
    player=GameObject.FindGameObjectWithTag("Player");
		guiScale=Screen.width/baseScreenWidth;
		transform.localScale=new Vector3(guiScale*0.07f,guiScale*0.07f,1);
	}
	
	// Update is called once per frame
	void Update () {
	   
	}
	
	void OnMouseDown(){
		if(functionName=="JumpUp"){
		player.SendMessage(functionName);
		}else if(functionName=="JumpDown"){
		   PathCode.jumpdown=true;
		}
		Player.OnFly=false;
		  
	
	}
}
