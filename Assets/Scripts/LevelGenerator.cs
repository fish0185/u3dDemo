using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
	private GameObject lastPlateform;
    //Vector3 lastPlateformPosition;
	float spawnPointX;
	public GameObject[] Plateform;
	
	public GameObject[] SpecialLevel;
	bool startSpecialLevel=false;
	// Use this for initialization
	public GameObject bg1;
	public GameObject bg2;
	float switchTime=0;
	float fadeIncre=0.1f;
	float TotalfadeTime=0;
	float alphaValue=0;
	bool activeBg1=false;
	bool startFadeIn=false;
	
	bool startFadeOut=false;
	void Start () {
	  
	   if(!lastPlateform){
			lastPlateform=Instantiate(Plateform[0],transform.position,Quaternion.identity) as GameObject;
			//lastPlateformPosition=lastPlateform.transform.position;
			spawnPointX=transform.position.x;
		}
	}
	
	// Update is called once per frame
	void Update () {
		switchTime+=Time.deltaTime;
		if(switchTime>20){
		  startSpecialLevel=!startSpecialLevel;
			switchTime=0;
		}
//		Debug.Log(transform.position);
	  float dist=spawnPointX-lastPlateform.transform.position.x;
		//Debug.Log(dist+" @###########");
	  if(dist>=205&&!startSpecialLevel){
	      lastPlateform=Instantiate(Plateform[Random.Range(0,3)],transform.position,Quaternion.identity) as GameObject;
			dist=0;
		    //bg1.SetActive(false);
			//bg2.SetActive(false);
			//StartFadein(false);
		  
			//StartCoroutine(Fade(true));
		
			startFadeOut=true;
			
			
		}else if(dist>=205&&startSpecialLevel){
		  lastPlateform=Instantiate(SpecialLevel[Random.Range(0,3)],transform.position,Quaternion.identity) as GameObject;
		  dist=0;
			
			
			//StartCoroutine(Fade(false));
			//StartFadein(true);
			//bg1.SetActive(true);
			//bg2.SetActive(true);
			//Debug.Log("#################OOOOOOPPPPPPPPPP");
			
		  startFadeIn=true;
		}
		if(startFadeIn){
		TotalfadeTime+=Time.deltaTime;
			if(TotalfadeTime>fadeIncre){
				//Debug.Log("#################");
				if(!activeBg1){
				  bg1.SetActive(true);
				  bg2.SetActive(true);
				  activeBg1=true;
					//Debug.Log("#################$$$$$$$$$$$$$$$$$$$");
				}
				Color c = bg1.GetComponent<Renderer>().material.color;
				
				if(alphaValue<1f){
				  alphaValue+=0.1f;
				}
				
				if(alphaValue>=1f){
				 startFadeIn=false;
				}
			    c.a = alphaValue;
			    bg1.GetComponent<Renderer>().material.color = c;
				bg2.GetComponent<Renderer>().material.color = c;
				Debug.Log("HOW MANY TIME COUNT me fadein");
			    TotalfadeTime=0;
			}
		}
		
		if(startFadeOut){
		  TotalfadeTime+=Time.deltaTime;
			if(TotalfadeTime>fadeIncre){
				
				Color c = bg1.GetComponent<Renderer>().material.color;
				
				if(alphaValue>0){
				  alphaValue-=0.1f;
				}
				
				if(alphaValue<=0){
				 startFadeOut=false;
					bg1.SetActive(false);
					bg2.SetActive(false);
					activeBg1=false;
				}
			    c.a = alphaValue;
			    bg1.GetComponent<Renderer>().material.color = c;
				bg2.GetComponent<Renderer>().material.color = c;
				Debug.Log("HOW MANY TIME COUNT me fadeout");
			    TotalfadeTime=0;
			}
		}
		
		
			
	}
	/*
	IEnumerator Fade(bool fade){
	  if(fade){
		bg1.SetActive(true);
		bg2.SetActive(true);
		//bg1.renderer.material.renderQueue=1;
		for (float f = 0; f <= 1f; f += 0.1f) {
			Color c = bg1.renderer.material.color;
			
			c.a = f;
			bg1.renderer.material.color = c;
				bg2.renderer.material.color = c;
			yield return new WaitForSeconds(.1f);
	    }
	  }else{
	     for (float f = 1f; f >= 0f; f -= 0.1f) {
			Color c = bg1.renderer.material.color;
			
			c.a = f;
			bg1.renderer.material.color = c;
				bg2.renderer.material.color = c;
			yield return new WaitForSeconds(.1f);
			
	    }
		bg1.SetActive(false);
		bg2.SetActive(false);
	  }
	}
	*/
	
	
}
