#pragma strict

var scrollSpeed:float=1.0;
//var direction:Vector2;


 
private var time:float=0;
function Start () {
  GetComponent.<Renderer>().material.mainTextureOffset=Vector2(0,0);
}

function Update () {
time+=Time.deltaTime;
  var offset:float=time*scrollSpeed;
  GetComponent.<Renderer>().material.mainTextureOffset=Vector2(offset,0);
}