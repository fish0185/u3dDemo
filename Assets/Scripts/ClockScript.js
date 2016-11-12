#pragma strict

var clockIsPaused:boolean=false;
var startTime:float;
var timeRemaining:float;
var percent:float;
var clockBG:Texture2D;
var clockFG:Texture2D;
var clockFGMaxWidth:float;
function Awake(){
  startTime=120.0;
  clockFGMaxWidth=clockFG.width;
}

function Start () {

}

function Update () {
  if(!clockIsPaused){
    DoCountdown();
  }
}

function DoCountdown(){
  timeRemaining=startTime-Time.time;
  percent=timeRemaining/startTime*100;
  if(timeRemaining<0){
    timeRemaining=0;
    clockIsPaused=true;
    TimeIsUp();
  }
  ShowTime();
  Debug.Log("time remaining = "+timeRemaining);
}

function PauseColock(){
  clockIsPaused=true;
}

function UnPauseClock(){
  clockIsPaused=false;
}

function ShowTime(){
 var minutes:int;
 var seconds:int;
 var timeStr:String;
 seconds=timeRemaining/60;
 seconds=timeRemaining%60;
 timeStr=minutes.ToString()+":";
 timeStr+=seconds.ToString("D2");
 GetComponent.<GUIText>().text=timeStr;
}

function TimeIsUp(){
 Debug.Log("Time is up!");
}

function OnGUI(){
 var newBarWidth:float=(percent/100)*clockFGMaxWidth;
 var gap:int=20;
 GUI.BeginGroup(new Rect(Screen.width-clockBG.width-gap,gap,clockBG.width,clockBG.height));
 
 GUI.DrawTexture(Rect(0,0,clockBG.width,clockBG.height),clockBG);
 GUI.BeginGroup(new Rect(5,6,newBarWidth,clockFG.height));
 GUI.DrawTexture(Rect(0,0,clockFG.width,clockFG.height),clockFG);
 GUI.EndGroup();
 GUI.EndGroup();
}



