using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	// Use this for initialization
	private float percent=0f;
	public Texture2D fg;
	public Texture2D bg;
	float FGMaxWidth;
	float PowerUpBarWidth;
	public int gap=10;
	Matrix4x4 origl;
	Matrix4x4 m;
	void Start () {
	  FGMaxWidth=fg.width;
	  origl=GUI.matrix;
		m=Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(0.8f,0.8f, 1));
	}
	
	// Update is called once per frame
	void Update () {
	  percent+=Time.deltaTime*2;
	  if(percent>100){
		percent=100;	
	  }
		
	  PowerUpBarWidth=(percent/100)*FGMaxWidth;
	}
	
	void OnGUI(){
	  
	  //origl=0.8f*origl;
		GUI.matrix=m;
	  GUI.BeginGroup(new Rect(gap,gap,bg.width,bg.height));
	  GUI.DrawTexture(new Rect(0,0,bg.width,bg.height),bg);
	  GUI.BeginGroup(new Rect(5,6,PowerUpBarWidth,fg.height));
	  GUI.DrawTexture(new Rect(0,0,fg.width,fg.height),fg);
	  GUI.EndGroup();
      GUI.EndGroup();
	  GUI.matrix=origl;
	}
}
