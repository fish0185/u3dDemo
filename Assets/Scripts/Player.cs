using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.GameCenter;
public class Player : MonoBehaviour {

	// Use this for initialization
	public static bool isAlive=true;
	//private bool autoColl=false;
	AutoColl auC;
	bool startCount=false;
	float powerupTime=2;
	float totalAffectTime=0;
	
	static Vector3 RevivePosition=new Vector3(7.428952f,22,0);
	static bool doResetPosition=false;
	public string firstRunAchievementID="com.fish0185.ach1";
	public string leaderboardID="com.fish0185.game";
	
	public bool showAchievementBanners=true;
	
	private bool popUpCountDonw=false;
	private Vector3 Ogravity;
    public float speed;
	public float swingDistance;
	public float swingSpeed;	
	public static bool OnFly;
	void Start () {
		OnFly=false;
	    #if UNITY_IPHONE
		  Social.localUser.Authenticate(
		    result=>{
		      if(result&&showAchievementBanners){
		        GameCenterPlatform.ShowDefaultAchievementCompletionBanner(showAchievementBanners);
		        Debug.Log("Authenticated "+Social.localUser.userName);
		      }else{
		        Debug.Log("Failed to authenticate "+Social.localUser.userName);
		      }
		    }
		  );
		#endif
		isAlive=true;
		Ogravity=Physics.gravity;
	}
	
	
	void OnTriggerEnter(Collider collider){
	    if(collider.tag=="Damage"){
         isAlive=false;
         Debug.Log("Player Die");
        }
		
		if(collider.tag=="autoCollection"){
		  Debug.Log("Collection Star");
			transform.Find("powerup").gameObject.SetActive(true);
			GameObject[] stars=GameObject.FindGameObjectsWithTag("Star"); 
			foreach(GameObject gObject in stars){
			 //gObject.transform.parent=null;
			
			     auC=gObject.GetComponent<AutoColl>();
			     auC.powerupTime=powerupTime;
			     totalAffectTime=powerupTime;
				startCount=true;
			}
			
		}
	}
	// Update is called once per frame
	void Update () {
		
		
	 if(!isAlive){
	  //Time.timeScale=0;
		  
			SetScore(leaderboardID,Score.curentScore);
			Achievement(firstRunAchievementID,100);
			Time.timeScale=0;
			StopButton.TrigerDie();
			//StartCoroutine(PopCountDown(2));
			
		}
		
	
	    if(totalAffectTime>0&&startCount){
			totalAffectTime-=Time.deltaTime;
		  
		}else{
			transform.Find("powerup").gameObject.SetActive(false);
			startCount=false;
		}
		
		if(transform.position.y<-0.5f){
	      isAlive=false;
	    }
		
		if(doResetPosition){
		  transform.position=RevivePosition;
		  doResetPosition=false;
		  isAlive=true;
		  transform.FindChild("fly").gameObject.SetActive(true);
		  Physics.gravity=new Vector3(0,0,0);
		  OnFly=true;
		}
		
		if(OnFly&&Time.timeScale!=0){
		 	//swing
		  GetComponent<Rigidbody>().velocity=Vector3.zero;
		  float DelteY=Mathf.Sin(Time.time*swingSpeed)*swingDistance;
		  float x=transform.position.x;
		  transform.position=new Vector3(x,transform.position.y+DelteY,transform.position.z);
		}else{
		  Physics.gravity=Ogravity;
		  transform.FindChild("fly").gameObject.SetActive(false);
		}
		
	
	 
	}
	IEnumerator PopCountDown(int seconds){
		popUpCountDonw=true;
    	yield return new WaitForSeconds(seconds);
		Application.LoadLevel("Menu");
	}
	
	static void SetScore(string name,int score){
		#if UNITY_IPHONE
		  if(Social.localUser.authenticated){
		    Social.ReportScore(score,name,result=>{
		      if(result){
		       Debug.Log("Posted "+score+" on leaderboard "+name);
		       
		      }else{
		        Debug.Log("Failed to post "+score+" on leaderboard "+name);
		      }
		    });
		  }
		#endif

    }

	
	
	static void Achievement(string name,double progress){
	  #if UNITY_IPHONE
	    if(Social.localUser.authenticated){
				
	      Social.ReportProgress(name,progress,result=>{
	         if(result){
	           Debug.Log("Achievement "+name+" reported successfully");
	         }else{
	           Debug.Log("Failed to report achievement "+name);
	         }
	      });
	        
	      
	    }
	  #endif
   }
	

	
	public static void Revive(){
	  isAlive=true;
	  doResetPosition=true;
	  
      
	}

	
	
}
