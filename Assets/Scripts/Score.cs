using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	float score;
	public static int curentScore;
	
	void Start () {
	  score=0;
	}
	
	// Update is called once per frame
	void Update () {
	  score+=(Time.deltaTime*10);
	  curentScore=((int)score);
	  GetComponent<GUIText>().text=curentScore.ToString();
	}
}
