using UnityEngine;
using System.Collections;

public class StopButton : MonoBehaviour {
	
	private bool Paused;
	private float baseScreenWidth=640;
	public GUISkin skin;
	public enum GameState{PLAY,DIE,PAUSE}; 
	private static GameState currentState;
	
	
	// Use this for initialization
	void Start () {
		currentState=GameState.PLAY;
	    Time.timeScale=1;
	}
	
	// Update is called once per frame
	void Update () {
	  
	}
	void OnMouseDown(){
		currentState=GameState.PAUSE;
	}
	
	void OnGUI(){
		GUI.skin=skin;
		float guiScale=Screen.width/baseScreenWidth;
		Matrix4x4 m=Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(guiScale,guiScale, 1));
	    GUI.matrix = m;
		switch(currentState){
		case GameState.PLAY:
			
			break;
		case GameState.PAUSE:
			showPause();
			break;
		case GameState.DIE:
			showDie();
			break;
		}
		
		
	}
	
	public static void TrigerDie(){
	  currentState=GameState.DIE;
	}
	
	void Resume(){
	  // Time.timeScale=1;
	        Time.timeScale=1;
			currentState=GameState.PLAY;
	}
	

	
	void showPause(){
	   Time.timeScale=0;
		DrawPopUp();
	}
	
	void showDie(){
		
		GUILayout.BeginArea(new Rect(baseScreenWidth*0.5f-100,Screen.height*0.5f-100,200,150));
		     GUILayout.BeginVertical();
		     GUIStyle ibox= GUI.skin.GetStyle("InGameBox");
		     GUILayout.Box("I am a box",ibox);
		     GUILayout.BeginHorizontal();
		     if(GUILayout.Button("Play Again")){
		        Application.LoadLevel("Menu");
		     }
		     if(GUILayout.Button("Revive")){
			int GemCount=PlayerPrefs.GetInt("GaryGameGemsCount");
			//Debug.Log(GemCount+" Gems Count");
			   if(GemCount>0){
			     Resume();
		         Player.Revive();
				 PlayerPrefs.SetInt("GaryGameGemsCount",--GemCount);
			   }else{
			     Debug.Log("GaryGameGemsCount less than or equi 0 ##############################################");
			   }
		     }
		     
		     GUILayout.EndHorizontal();
		     GUILayout.EndVertical();
		GUILayout.EndArea();
	}
	
	
	
	void DrawPopUp(){
		 GUIStyle button= GUI.skin.GetStyle("InGame");
		
	    GUILayout.BeginArea(new Rect(baseScreenWidth*0.5f-100,Screen.height*0.5f-100,200,200));
		//GUILayout.FlexibleSpace();
		
		
		if(GUILayout.Button("RESUME",button)){
			Resume();	    
		}
		
		
		
		if(GUILayout.Button("SHOP",button)){
		  Debug.Log("Click Shop");
		}
		
		
		if(GUILayout.Button("MAIN MENU",button)){
		  Application.LoadLevel("Menu");
		}
		
		//GUILayout.FlexibleSpace();
	    GUILayout.EndArea();
		
		
	}
}
