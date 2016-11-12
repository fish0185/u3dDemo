using UnityEngine;
using System;
using System.Collections;
using Facebook.MiniJSON;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using UnityEngine.SocialPlatforms.GameCenter;
using System.Xml;
using System.IO;

public class FBUser : IEquatable<FBUser> , IComparable<FBUser>
{
	
	public Texture picture { get; set; }
    public string id { get; set; }
	public string pictureurl { get; set; }
    public string name { get; set; }

    public int score { get; set; }
	
	public FBUser(string pictureurlf){
     
    }
	
    public override string ToString()
    {
        return "ID: " + score + "   Name: " + name;
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        FBUser objAsPart = obj as FBUser;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }
    public int SortByNameAscending(string name1, string name2)
    {

        return name1.CompareTo(name2);
    }

    // Default comparer for Part type. 
    public int CompareTo(FBUser comparePart)
    {
          // A null value means that this object is greater. 
        if (comparePart == null)
            return 1;

        else 
            return this.score.CompareTo(comparePart.score);
    }
    public override int GetHashCode()
    {
        return score;
    }
    public bool Equals(FBUser other)
    {
        if (other == null) return false;
        return (this.score.Equals(other.score));
    }
    // Should also override == and != operators.

}

public class NewMenu : MonoBehaviour {

	public enum Page{None,FBook,Tw,Em,GameC,Main};
	public enum LeftGUI{ALL,FBSUB,TWSUB,GCSUB,EMSUB};
	private LeftGUI currentSub;
	public enum RightGUI{ALL,OPTIONS,UPGRADE,SHOP};  
	private RightGUI currentRight;
	public GUISkin skin;
	private Page currentPage;
	private float savedTimeScale;
	public int menuTop=30;
	public Texture tex;
	public Texture logo;
	private float baseScreenWidth=640;
    AsyncOperation asyncOperation;
	/*
	void PauseGame(){
	  savedTimeScale=Time.timeScale;
	  Time.timeScale=0;
	  AudioListener.pause=true;
	  currentPage=Page.Main;
	}
	
	void UnPauseGame(){
	  Time.timeScale=savedTimeScale;
	  AudioListener.pause=false;
	  currentPage=Page.None;
	}
	static bool isGamePaused(){
	  return Time.timeScale==0;
	}
	*/
	private int position=1;
	private string userid=null;
	private string resultText=null;
	private string friendsList=null;
    private string mydata=null;
	private bool DoGrabDataOnce=true;
	private bool DoOne=true;
	// Use this for initialization
	
	private bool notreadytoShow=false;
	public Vector2 scrollPosition;
	public Vector2 scrollPositionNearBy;
	private Texture tt;
	private bool GrabPictureOnce=true;
	/*[url, https://fbcdn-profile-a.akamaihd.net/hprofile-ak-ash2/187072_577698810_4208498_n.jpg]*/
	
	private string nearByPlayer=null;
	private bool dataReady=false;
	private string gift=null;
	private int score=0;

	private List<FBUser> fbFriends=new List<FBUser>();
	private List<FBUser> fbPlayer=new List<FBUser>();
	//create online game score
	private string url;
	private string action;
	private string parameters;
	private string textFileContents="(still loading file....)";
	//private string friendsScore="(loading...)";
	private bool DoCheckUserAccountOnce=true;
	private bool GrabNearByPlayerDataOnce=true;
	string key="garygamedev";
	string databaseID="01";
	
	string gamecode;
	bool FlagOfAddFriend=true;
	public static List<FBUser> friendsInGame=new List<FBUser>();
	
	bool locationFound;
	//##########
	public Texture2D backG;
	GameObject adBanner;
	private bool showWindow=false;
	string PlayerFBPictureUrl;
	string PlayerFBname=null;
	public static int GemsCount=0;
	public TextAsset scoreDataTextFile;
	private float latitude=0;
	private float longitude=0;
	private Rect windowRect = new Rect(100, 50, 300, 200);
	
	private List<FBUser> nearbyP= new List<FBUser>();
	
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
	
	void OnGUI(){
		
		
//#if UNITY_IPHONE
      float guiScale=Screen.width/baseScreenWidth;
		Matrix4x4 m=Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(guiScale,guiScale, 1));
	  GUI.matrix = m;
		GUI.skin=skin;
//#endif
 		
	  switch (currentPage){
		case Page.Main: showMain();break;
		//case Page.FBook: showFBook();break;
		}
		
	  
	}
	
	void BeginPage(int width,int height){
		
	  GUILayout.BeginArea(new Rect((baseScreenWidth-width)/2,menuTop,width,height));
		
	}
	
	void EndPage(){
		GUILayout.EndArea();
		
	}
	
	void showMain(){
		BeginPage(600,420);
		  GUILayout.BeginHorizontal();
		  GUILayout.Label("High Score");
		  GUILayout.Label("Coins X 1000");
		  
		  GUILayout.Label("Gems X "+GemsCount);
		  GUILayout.EndHorizontal();
		 // GUILayout.Space();
		  GUILayout.BeginHorizontal();
		   
		   GUILayout.BeginArea(new Rect(0,50,300,350));
		     switch (currentSub){
		       case LeftGUI.ALL:
			   showAll();
			   break;
			   case LeftGUI.FBSUB:
			   showFBSub();
			   break;
			   case LeftGUI.EMSUB:
			   showEMSub();
			   break;
		       case LeftGUI.GCSUB:
			   showGCSub();
			   break;
		       case LeftGUI.TWSUB:
			   showTWSub();
			   break;
			   
		     }
		    
		
		   GUILayout.EndArea();
		   //
		
		   DrawRightPartMenu();
		EndPage();
	}
	
	void showAll(){
	          GUILayout.Box(logo);  
		      GUILayout.BeginHorizontal();
		      if(GUILayout.Button("Facebook")){
		         currentSub=LeftGUI.FBSUB;
		      }
		      //if(GUILayout.Button("Twitter")){
		       //  currentSub=LeftGUI.TWSUB;
		      //}
		      if(GUILayout.Button("GameCenter")){
		        //currentSub=LeftGUI.GCSUB;
			   Social.ShowLeaderboardUI();
		      }
		      if(GUILayout.Button("Email")){
			     Application.OpenURL("mailto:1013161484@qq.com?cc=bar@example.com&subject=Greetings%20from%20Cupertino!&body=Wish%20you%20were%20here!");
		        //Application.OpenURL("http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewSoftware?id=294409923&mt=8"); 
			    //currentSub=LeftGUI.EMSUB;
			     //Application.OpenURL("sms:+61433052581");
		      }
		      GUILayout.EndHorizontal();
	}
	
	void showGCSub(){
	  GUILayout.Label("your game center came here");
		BackToLeftAll();
	}
	
	void showTWSub(){
	  GUILayout.Label("your twitter came here");
		BackToLeftAll();
	}
	
	void showFBSub(){
	         if(!FB.IsLoggedIn){		   
					ShowFaceBookUnLogin();
	          }else{
		        
					 ShowFaceBookLogined();
		      }
	}
	
	Void showEMSub(){
		GUILayout.Label("your email came here");
		BackToLeftAll();
	}
	
	void BackToLeftAll(){
		
	   if(GUILayout.Button("Back")){
	      currentSub=LeftGUI.ALL;
	   }
	}
	
	void DrawRightPartMenu(){
	   GUILayout.BeginArea(new Rect(300,50,300,350));
		     
		    switch (currentRight){
		       case RightGUI.ALL:
			   showRightAll();
			   break;
			   case RightGUI.OPTIONS:
			   showOptions();
			   break;
			   case RightGUI.SHOP:
			   showShop();
			   break;
		       case RightGUI.UPGRADE:
			   showUpgrade();
			   break;
		   }
		    GUILayout.EndArea();
	  GUILayout.EndHorizontal();
	}
	
	void BackToRightAll(){
		
	   if(GUILayout.Button("Back")){
	      currentRight=RightGUI.ALL;
	   }
	}
	private int toolbarIndex=0;
	private String[] toolbarStrings={"Audio","Graphics","Credits"};
	void showRightAll(){
		GUILayout.BeginVertical();
		      GUIStyle RightBox= GUI.skin.GetStyle("RightBox");
		      GUILayout.Box(tex,RightBox);
		      GUILayout.BeginHorizontal();
		      if(GUILayout.Button("Upgrades")){
		        currentRight=RightGUI.UPGRADE;
		      }
		      if(GUILayout.Button("Shop")){
		        currentRight=RightGUI.UPGRADE;
		      }
		      if(GUILayout.Button("Options")){
		         
			     currentRight=RightGUI.OPTIONS;
		      }
		
		      GUILayout.EndHorizontal();
		      GUIStyle play= GUI.skin.GetStyle("PlayButton");
		      if(GUILayout.Button("Play",play)){
			    //yield return new WaitForSeconds(0.5f);
			    tex=Resources.Load("element-09") as Texture;
			    StartCoroutine(waitforload());
			// after release check again
			    adBanner.SendMessage("HideIad");
		        //Application.LoadLevel("Game1");
			    StartCoroutine("loadScene","Game1");
		      }
		    
		    if(asyncOperation!=null&&!asyncOperation.isDone){
			  
			   
		       GUILayout.Label("PROGRESS: "+(float)asyncOperation.progress*100+"%");
		    } 
		 GUILayout.EndVertical();
	}
	
	void showOptions(){
		toolbarIndex=GUILayout.Toolbar(toolbarIndex,toolbarStrings);
		switch(toolbarIndex){
			case 0:ShowAudio();break;
			case 1:ShowGraphics();break;
			case 2:ShowCredit();break;
		}
		BackToRightAll();
	}
	
	void ShowAudio(){
	 GUILayout.Label("Volume");
	 AudioListener.volume=GUILayout.HorizontalSlider(AudioListener.volume,0.0f,1.0f);
	}
	
	void ShowGraphics(){
	  GUILayout.Label(QualitySettings.names[QualitySettings.GetQualityLevel()]);
	  GUILayout.Label("Pixel Light Count: "+QualitySettings.pixelLightCount);
	  GUILayout.Label("Shadow Cascades: "+QualitySettings.shadowCascades);
	  GUILayout.Label("Shadow Distance: "+QualitySettings.shadowDistance);
	  GUILayout.Label("Soft Vegetation: "+QualitySettings.softVegetation);
	  GUILayout.BeginHorizontal();
		if(GUILayout.Button("Decrease"))
		{
			QualitySettings.DecreaseLevel();
		}
		if(GUILayout.Button("Increase")){
			QualitySettings.IncreaseLevel();
		}
	  GUILayout.EndHorizontal();
	}
	
	string[] credits={"Program: Gary","UI Design: Lee","Mode: ShiGao Chen"};
	void ShowCredit(){
	  foreach(string lab in credits){
		GUILayout.Label(lab);
	  }
	}
	
	void showShop(){
		BackToRightAll();
	}
	
	void showUpgrade(){
		BackToRightAll();
	}
	
	
	
	void FriendsData(FBResult result){
		friendsList=result.Text;
		//Debug.Log(friendsList);
	}
	
	void MyData(FBResult result){
	  mydata=result.Text;
	}
	
	void LoginCallback(FBResult result){
	  Debug.Log("call login: ####################################################"+FB.UserId);
		Debug.Log("login result: "+result.Text);
		userid=FB.UserId;
		resultText=result.Text;
	}
	
	IEnumerator loadScene(string sceneName){
	  yield return asyncOperation=Application.LoadLevelAsync(sceneName);
	}
	
	IEnumerator waitforload(){
	  yield return new WaitForSeconds(0.2f);
	}
	
	void ShowFaceBookUnLogin(){
		if(GUILayout.Button("Login Facebook")){
			           FB.Login("email,publish_actions",LoginCallback);
			        }
				
				    if(GUILayout.Button("Back")){
			           currentSub=LeftGUI.ALL;
			        }
				    
	}
	
	void ShowFaceBookLogined(){
	                if(DoGrabDataOnce){
						DoGrabDataOnce=false;
			//https://graph.facebook.com/zuck/picture?width=150&height=150
						FB.API("/me/friends?access_token="+FB.AccessToken+"&fields=name,id,picture.type(square)",Facebook.HttpMethod.GET,FriendsData);
			            //FB.API("/me/friends?access_token="+FB.AccessToken+"&fields=name,id,picture?width=150&height=150",Facebook.HttpMethod.GET,FriendsData);
					    //FB.API("/me/picture?width=128&height=128", Facebook.HttpMethod.GET, MyData);
			            FB.API("/me?access_token="+FB.AccessToken+"&fields=name,picture.type(small)", Facebook.HttpMethod.GET, MyData);
			            
					 }
					
					  if(friendsList!=null&&DoOne){
					    GrabDataOnce(friendsList);
						DoOne=false;
					  }
				     
				    GUIStyle back= GUI.skin.GetStyle("backButton");
				    GUIStyle GS= GUI.skin.GetStyle("SubmitButton");
				    if(friendsList!=null){
					  GUILayout.Label("View Your Friends' Score");
					  scrollPosition=GUILayout.BeginScrollView(scrollPosition,GUILayout.Width(300),GUILayout.Height(150));
					  //GUILayout.Label(tt);
					
					   fbFriends.Sort();
			           fbFriends.Reverse();
			
			          foreach(FBUser fuser in fbFriends){
			            if(fuser.picture==null){
				           notreadytoShow=false;
					       break;
				        }
				        notreadytoShow=true;
				         
			          }
			           
			            if(notreadytoShow){
						foreach(FBUser fuser in fbFriends){
						    
				           if(fuser.score>0){
						        friendsInGame.Add(fuser);
						         GUILayout.BeginHorizontal();
					            //GUILayout.Label("No."+position);
						         GUILayout.Label("No."+position);
							    if(fuser.picture!=null){
							      GUILayout.Label(fuser.picture,GUILayout.ExpandWidth(true));
						          //GUI.DrawTexture(Rect(10,10,60,60), fuser.picture, ScaleMode.ScaleToFit, true, 10.0f);
								}
							   GUILayout.Label(fuser.name);
					           //float score=0;
					           
							   GUILayout.Label(fuser.score.ToString());
							  GUILayout.EndHorizontal();
					          position++;
				          }
						}
				      
			          position=1;
			       }else{
			          GUILayout.Label("Loading your score");
			       }
		
			
						
					
					 GUILayout.EndScrollView();
			         GUILayout.BeginHorizontal();
			         if(GUILayout.Button("Near By Player",GS)){
				         getNearBy();
				         showWindow=true;
				         
				 
			         }
			         if(GUILayout.Button("Every Day Gift",back)){
				         getGift();
			         }
			          GUILayout.EndHorizontal();
				    }
		             GUILayout.BeginHorizontal();
		            
				
				    if (GUILayout.Button ("Invite Friends")) {
				    FB.AppRequest(
				        message: "Friend Smash is smashing! Check it out.",
				        title: "Play Friend Smash with me!"
				    );
					}
				
					if (GUILayout.Button ("Share With Facebook")) {
					    FB.Feed(
					        linkCaption: "I just post this Hello World Test Message on facebook for test!!",
					        picture: "http://www.friendsmash.com/images/logo_large.jpg",
					        linkName: "Check out my hello world",
					        link: "https://apps.facebook.com/friendsmashsampledev/?challenge_brag="
					           
					    );
					}
		
		            if (GUILayout.Button ("Logout FB")) {
				    FB.Logout();
					}
				
				    GUILayout.EndHorizontal();
		
		        string prettyText=textFileContents.Replace("</","?@?");
		
		        prettyText=prettyText.Replace("<","\n<");
		 
		        prettyText=prettyText.Replace("?@?","</");
		
		  GUILayout.BeginHorizontal(); 
	            //GUILayout.Label("your score online is "+prettyText);
		        
		        if(DoCheckUserAccountOnce){
			       Debug.Log("Run CheckWebUser");
			    StartCoroutine( CheckWebUser());
			        DoCheckUserAccountOnce=false;
		           
		        }
		       
				WebButtons();
		       
				if(GUILayout.Button("Back",back)){
			           currentSub=LeftGUI.ALL;
			    }
		GUILayout.EndHorizontal();
		if(nearByPlayer!=null&&GrabNearByPlayerDataOnce){
		  Debug.Log(nearByPlayer);
			ParseNearByPlayer(nearByPlayer);
			//string textData=scoreDataTextFile.text;
			//Debug.Log(textData);
			
			//ParseNearByPlayer(textData);
			
			GrabNearByPlayerDataOnce=false;
		}
		if(showWindow){
		windowRect=GUILayout.Window(0,windowRect,MakeWindow,"Near By Player");
		}
		
		
	}
	
	void ParseNearByPlayer(string xmlData){
		//byte[] encodedString=Encoding.UTF8.GetBytes(xmlData);
			//MemoryStream ms=new MemoryStream(encodedString);
			//ms.Flush();
			//ms.Position=0;
			
			
		XmlDocument xmlDoc=new XmlDocument();
		xmlDoc.Load(new StringReader(xmlData));
		string xmlPathPattern="/playerList/player";
		XmlNodeList myNodeList=xmlDoc.SelectNodes(xmlPathPattern);
		foreach(XmlNode node in myNodeList)
			PlayerString(node);
	}
	
	private void PlayerString(XmlNode node){
		XmlNode pNode=node.FirstChild;
		XmlNode scoreNode=pNode.NextSibling;
		XmlNode palyerUIDNode=scoreNode.NextSibling;
		FBUser nearPlayer=new FBUser("Default");
		nearPlayer.name=pNode.InnerXml;
		string nearbyscore=scoreNode.InnerXml;
		int nearbyscoreInt=0;
		
		
		try{
			nearbyscoreInt=Int32.Parse(nearbyscore);
		}catch(Exception p){
		}
		
		nearPlayer.score=nearbyscoreInt;
		nearPlayer.pictureurl="https://graph.facebook.com/"+palyerUIDNode.InnerXml+"/picture?width=60&height=60";
		StartCoroutine("LoadPicture",nearPlayer);
		
		//return "player = "+pNode.InnerXml+", score = "+scoreNode.InnerXml+", palyerUID = "+palyerUIDNode.InnerXml;
	}
	
	IEnumerator LoadPicture(FBUser nearPlayer){
		WWW www=new WWW(nearPlayer.pictureurl);
		yield return www;
		nearPlayer.picture=www.texture;
		nearbyP.Add(nearPlayer);
	}
	
	void MakeWindow(int windowID){
		if(GUILayout.Button("Close")){
		  showWindow=false;
		}
		scrollPositionNearBy=GUILayout.BeginScrollView(scrollPositionNearBy,GUILayout.Width(300),GUILayout.Height(150));
		if(nearbyP.Count>0){
		foreach(FBUser fbu in nearbyP){
	     GUILayout.BeginHorizontal();
		 GUILayout.Label(fbu.picture);
		 GUILayout.Label(fbu.name);
		 GUILayout.Label(fbu.score.ToString());
		 GUILayout.EndHorizontal();
		}
		}else{
		  GUILayout.Label("Loading Data...");
		}
		GUILayout.EndScrollView();
		
	}
	
	
	IEnumerator CheckWebUser(){
		Debug.Log("CheckWebUser before while"+PlayerFBname);
		while(PlayerFBname==null){
			Debug.Log("CheckWebUser inside while"+PlayerFBname);
	     yield return new WaitForSeconds(1);
		}
		Debug.Log("CheckWebUser"+PlayerFBname);
		
		action="create";
		string name=Uri.EscapeUriString(PlayerFBname);
	     parameters="&player="+name+"&score=0"+"&userUid="+FB.UserId;
	     StartCoroutine(LoadWWW());
	}
	
	private void WebButtons(){
	  GUIStyle GS= GUI.skin.GetStyle("SubmitButton");
	  bool submityournewScore=GUILayout.Button("Submit Score",GS);
	  //bool htmlButtonWasClicked=GUILayout.Button("Get html for all players");
	 // bool xmlButtonWasClicked=GUILayout.Button("Get xml for all players");
	  
	  if(submityournewScore){
	     SubmitAction();
		
		}
	  //if(htmlButtonWasClicked){
		//HTMLAction();
		//}
	  //if(xmlButtonWasClicked){
		//XMLAction();
		//}
	}
	
	private string StringToInt(string s){
		string intMessage="integer received = ";
		try{
          int integerReturned=Int32.Parse(s);
		  intMessage+=integerReturned;
		}catch(System.Exception e){
			intMessage+=" (not an integer)";
			print(e);
		}
		return intMessage;
	}
	
	private void SubmitAction(){
	  action="setScoreByUid";
	  parameters="&userUid="+FB.UserId+"&score="+Score.curentScore;
	  StartCoroutine(LoadWWW());
	}
	
	
	private void getNearBy(){
	  action="getNearBy";
	  parameters="&userUid="+FB.UserId+"&lat="+latitude+"&lng="+longitude;
	  StartCoroutine(LoadNearBy());
	}
	
	private IEnumerator LoadNearBy(){
	  string baseUrl="http://cnhotdeal.com/LeaderBoard/index.php?action=";
	  url=baseUrl+action+parameters+"&game_code="+gamecode+"&database_id="+databaseID;
	  WWW www=new WWW(url);
	  yield return www;
	  nearByPlayer=www.text;
	  
	}
	
	private void getGift(){
	  action="getGift";
	  parameters="&userUid="+FB.UserId;
	  StartCoroutine(LoadGift());
	}
	
	private void GetAction(){
	  action="get";
	  parameters="&player=GameMaker";
	  StartCoroutine(LoadWWW());
	}
	
	private IEnumerator LoadGift(){
	  string baseUrl="http://cnhotdeal.com/LeaderBoard/index.php?action=";
	  url=baseUrl+action+parameters+"&game_code="+gamecode+"&database_id="+databaseID;
	  WWW www=new WWW(url);
	  yield return www;
	  gift=www.text;
		Debug.Log(gift);
		Debug.Log("GemsCount before action"+GemsCount);
	    int coun=0;
		try{
		  coun=Int32.Parse(gift);
		}catch(Exception baddata){
		  Debug.Log(baddata.Message);
		  
		}
		if(coun==1){
		  GemsCount=PlayerPrefs.GetInt("GaryGameGemsCount");
		  GemsCount=GemsCount+coun;
		  PlayerPrefs.SetInt("GaryGameGemsCount",GemsCount);
		  PlayerPrefs.Save();
		}
		
		
	}
	
	
	private void HTMLAction(){
	  action="html";
	  parameters="";
	  StartCoroutine(LoadWWW());
	}
	
	private void XMLAction(){
	  action="xml";
	  parameters="";
	  StartCoroutine(LoadWWW());
	}
	
	private IEnumerator GetScoreAction(FBUser user){
	  action="getScore";
	  parameters="&userUid="+user.id;
	  string baseUrl="http://cnhotdeal.com/LeaderBoard/index.php?action=";
	  url=baseUrl+action+parameters+"&game_code="+gamecode+"&database_id="+databaseID;
	  WWW www=new WWW(url);
	  yield return www;
	  try{
	  user.score=Int32.Parse(www.text);
		}catch(Exception ee){
			user.score=0;
			Debug.Log(ee.StackTrace);
		}
	}
	

	
	private IEnumerator LoadWWW(){
	  string baseUrl="http://cnhotdeal.com/LeaderBoard/index.php?action=";
	  url=baseUrl+action+parameters+"&game_code="+gamecode+"&database_id="+databaseID;
	  WWW www=new WWW(url);
	  yield return www;
	  textFileContents=www.text;
	}
	
	
	void GrabDataOnce(string friendslist){
   
		    try{
		    Dictionary<string,object> dict=Json.Deserialize(friendsList) as Dictionary<string,object>;
		    
		
		    object friendsH;
			List<object> friends=new List<object>();
			
			if(dict.TryGetValue("data",out friendsH)){
			 
			  
			friends=((IEnumerable)friendsH).Cast<object>().ToList();
			//Debug.Log(friends.Count);
			
			
			for(int i=0;i<friends.Count;i++){
			    Dictionary<string,object> friendDict=((Dictionary<string,object>)friends[i]);
				
			   Dictionary<string,object> friendDict2=((Dictionary<string,object>)friendDict["picture"]);
			
			  
			   object urlc;
			   friendDict2.TryGetValue("data",out urlc);
			  
			
			   List<object> imfo=new List<object>();
			   imfo=((IEnumerable)urlc).Cast<object>().ToList();
			
			   
			
			   string imageAddress=imfo[0].ToString().Trim();
			   string url;
			   
			   url=imageAddress.Substring(6,imageAddress.Length-7);
			
				FBUser fbuser=new FBUser(url);
				fbuser.id=friendDict["id"].ToString();
				fbuser.name=friendDict["name"].ToString();
				fbuser.pictureurl=url;
				StartCoroutine("GrabPictureForUser",fbuser);
				StartCoroutine("GetScoreAction",fbuser);
				fbFriends.Add(fbuser);
			}
			//Debug.Log(mydata);
	        AddSelf();		
			dataReady=true;
		}
		}catch(Exception e){
			Debug.Log("Exception came from function 'GrabDataOnce'");
	    }
		
	}
	
	void AddSelf(){
		
	  ProcessMyData();
		
	  FBUser me=new FBUser(PlayerFBPictureUrl);
	  me.id=FB.UserId;
	  me.name=PlayerFBname;
	  me.pictureurl=PlayerFBPictureUrl;
	  StartCoroutine("GrabPictureForUser",me);
	  StartCoroutine("GetScoreAction",me);
	  fbFriends.Add(me);
	}
	
	void ProcessMyData(){
	  try{
	     Dictionary<string,object> dict=Json.Deserialize(mydata) as Dictionary<string,object>;
		 PlayerFBname=dict["name"].ToString();
			Debug.Log(PlayerFBname);
		 object PicturesData;
		 List<object> data=new List<object>();
		 if(dict.TryGetValue("picture",out PicturesData)){
				//data=(List<object>)(((Dictionary<string, object>)PicturesData) ["data"]);
				//Debug.Log(PicturesData.ToString());
				 Dictionary<string,object> friendDict2=((Dictionary<string,object>)PicturesData);
				//Debug.Log(friendDict2["data"].ToString());
				object dd=friendDict2["data"];
				
				List<object> myd=new List<object>();
				myd=((IEnumerable)dd).Cast<object>().ToList();
				
			  string imageAddress=myd[0].ToString().Trim();
			   PlayerFBPictureUrl=imageAddress.Substring(6,imageAddress.Length-7);
				
				
		 }
	  }catch(Exception e){
	  }
	}
	
	
	IEnumerator GrabScoreForUser(FBUser fbuser){
		
		WWW www=new WWW(fbuser.id);
		yield return www;
		fbuser.picture=www.texture;
		
	}
	
	IEnumerator GrabPictureForUser(FBUser fbuser){
		//WWW www=new WWW(fbuser.pictureurl);
		
		WWW www=new WWW("https://graph.facebook.com/"+fbuser.id+"/picture?width=60&height=60");
		yield return www;
		fbuser.picture=www.texture;
		
	}
	bool showAchievementBanners=true;
	IEnumerator Start () {
		InitPlayerInfo();
		 
		 notreadytoShow=false;
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
		adBanner=GameObject.FindGameObjectWithTag("AdBanner");
		gamecode=CalculateMD5Hash(key+databaseID);
		Debug.Log(gamecode);
	  currentPage=Page.Main;
	    currentSub=LeftGUI.ALL;
		currentRight=RightGUI.ALL;
		Debug.Log(Screen.width/baseScreenWidth);
		
		DontDestroyOnLoad(gameObject);
		
	   FB.Init(SetInit,OnHideUnity);
		
		locationFound=false;
	   Input.location.Start();
	   int maxWait=10;
	   while(Input.location.status==LocationServiceStatus.Initializing&&maxWait>0){
	      yield return new WaitForSeconds(1);
		  maxWait--;
		}
	   if(maxWait<1){
		   yield return false;
		}
		if(Input.location.status==LocationServiceStatus.Failed){
		     yield return false;
			//return false;
		}else{
		  locationFound=true;
		}
		 Input.location.Stop();
		
	}
	
	void InitPlayerInfo(){
    	if(PlayerPrefs.HasKey("GaryGameGemsCount")){
		   GemsCount=PlayerPrefs.GetInt("GaryGameGemsCount");
		}else{
		  PlayerPrefs.SetInt("GaryGameGemsCount",3);
		}
	 
	}
	
	
	
	// Update is called once per frame
	void Update () {
	  if(asyncOperation!=null&&asyncOperation.progress==1){
	     Destroy(gameObject);
		}
	  if(locationFound)
		{
			latitude=Input.location.lastData.latitude;
			longitude=Input.location.lastData.longitude;
	       Debug.Log("Latitude: "+Input.location.lastData.latitude+"Longitude: "+Input.location.lastData.longitude+"Altitude "+Input.location.lastData.altitude+"Accuracy: "+Input.location.lastData.horizontalAccuracy+"Time: "+Input.location.lastData.timestamp);
		  locationFound=false;
		}
	}
	
	public string CalculateMD5Hash(string input)
    {
    // step 1, calculate MD5 hash from input
    MD5 md5 = System.Security.Cryptography.MD5.Create();
    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
    byte[] hash = md5.ComputeHash(inputBytes);
 
    // step 2, convert byte array to hex string
    StringBuilder sb = new StringBuilder();
    for (int i = 0; i < hash.Length; i++)
    {
        sb.Append(hash[i].ToString("X2"));
    }
    return sb.ToString();
   }
}
