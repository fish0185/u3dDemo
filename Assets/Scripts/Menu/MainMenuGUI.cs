using UnityEngine;
using System.Collections;
using Facebook.MiniJSON;

//using System.Web.Util;

public class MainMenuGUI : MonoBehaviour {
	//bool enabled;
	string panelText;
	Rect LABELSIZE;
	Rect BUTTONSIZE;
	public GUISkin menuSkin;
	bool callOnce=true;
	private void SetInit(){
	  enabled=true;
	}
	
	private void OnHideUnity(bool isGameShown){
	  if(!isGameShown){
	      Time.timeScale=0;
		}else{
		  Time.timeScale=1;
	    }
	}
	
	// Use this for initialization
	void Start () {
	  panelText="Hello World Runner";
	  LABELSIZE=new Rect(10,10,150,150);
	  BUTTONSIZE=new Rect(10,200,200,200);
	  enabled=false;
	  FB.Init(SetInit,OnHideUnity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
	  GUI.Label(LABELSIZE,panelText,menuSkin.GetStyle("label"));
		
	  if(!FB.IsLoggedIn){
			if(GUI.Button(BUTTONSIZE,"",new GUIStyle())){
			  FB.Login("email,publish_actions",LoginCallback);
			}
	  }
		if(callOnce){
			FB.API("/yongqun.yu/picture?width=128&height=128", Facebook.HttpMethod.GET, LogCallback);
			//FB.API("/me?fields=id,first_name,friends.limit(100).fields(first_name,id)", Facebook.HttpMethod.GET, APICallback);
		    //FB.API("/yongqun.yu/picture?width=128&height=128", Facebook.HttpMethod.GET, MyPictureCallback);
			callOnce=false;
		}
		GUILayout.BeginArea(new Rect(200,200,300,300));
		if (GUILayout.Button ("What about click me")) {
		    FB.AppRequest(
		        message: "Friend Smash is smashing! Check it out.",
		        title: "Play Friend Smash with me!"
		    );
		}
		
		if (GUILayout.Button ("What do you want to said")) {
		    FB.Feed(
		        linkCaption: "I just post this Hello World Test Message on facebook for test!!",
		        picture: "http://www.friendsmash.com/images/logo_large.jpg",
		        linkName: "Check out my hello world",
		        link: "https://apps.facebook.com/friendsmashsampledev/?challenge_brag="
		           
		    );
		}
		if (GUILayout.Button ("Play Game")) {
		    Application.LoadLevel("Game1");
		}
		GUILayout.EndArea();
	}
	
	void LoginCallback(FBResult result){
		Debug.Log("call login: ####################################################"+FB.UserId);
		Debug.Log("login result: "+result.Text);
		//FB.API("/me?fields=id,first_name,friends.limit(100).fields(first_name,id)", Facebook.HttpMethod.GET, APICallback);
		//FB.API("/me/picture", Facebook.HttpMethod.GET, LogCallback);
        FB.API("/me/picture?width=128&height=128", Facebook.HttpMethod.GET, MyPictureCallback);
	}
	
	void LogCallback(FBResult response) {
        Debug.Log(response.Texture != null ? "got profile pic" : "no profile pic");
		GameObject te=GameObject.Find("my");
	
	    te.GetComponent<GUITexture>().texture=response.Texture;
    }
	
	/*
	void APICallback(FBResult result) // handle user profile info
    {
	    if (result.Error != null)
	    {
	        Debug.LogError(result.Error);
	        return;
	    }
	
	    profile = Util.DeserializeJSONProfile(result.Text);
	    GameStateManager.Username = profile["first_name"];
	    friends = Util.DeserializeJSONFriends(result.Text);
    }*/
	
	void APICallback(FBResult result){
	  if (result.Error != null)
	    {
	        Debug.LogError(result.Error);
	        return;
	    }
		//Dictionary dict=Json.Deserialize(result.Text) as Dictionary<string,object>;
		//string username=dict['name'];
		Debug.Log("you are herer");
		
	}

	void MyPictureCallback(FBResult result) // store user profile pic
	{
	    if (result.Error != null)
	    {
	        Debug.LogError(result.Error);
	        return;
	    }
			
		GameObject te=GameObject.Find("my");
		
		 te.GetComponent<GUITexture>().texture=result.Texture;
	    
	}
}
