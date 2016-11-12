#pragma strict

public var f_speed:float=5.0;
public var loopSprites:SpriteManagerYU[];
private var in_direction : int;
public var CameraC : GameObject;
private var b_isJumping:boolean;
private var f_height:float;
private var f_lastY:float;
public var jumpSprite:JumpSpriteManager;
public var layerMask:LayerMask;

public var doorOpenTexture:Texture2D;
public var doorCloseTexture:Texture2D;
private var b_hasKey:boolean;

private var restartButton:GUITexture;
public var doorOpenSound:AudioClip;
public var getKeySound:AudioClip;
public var jumpSound:AudioClip;

//private var linerenderer:LineRenderer;
function Start () {
  //linerenderer=gameObject.GetComponent(LineRenderer);
  in_direction=1;
  b_hasKey=false;
  //Initialization Sprite Manager
  for(var i:int=0;i<loopSprites.length;i++){
    loopSprites[i].init();
  }
  Camera.main.transform.position=new Vector3(transform.position.x,transform.position.y,Camera.main.transform.position.z);
  CameraC.transform.position=new Vector3(transform.position.x,transform.position.y,CameraC.transform.position.z);
  var mesh:Mesh=GetComponent(MeshFilter).sharedMesh;
  f_height=mesh.bounds.size.y*transform.localScale.y;
  f_lastY=transform.position.y;
  b_isJumping=false;
  
 restartButton = GameObject.FindWithTag("RestartButton").GetComponent.<GUITexture>();
 restartButton.enabled=false;
  
}

public function OnTriggerEnter(hit:Collider):IEnumerator{
  if(hit.GetComponent.<Collider>().tag=="Key"){
    b_hasKey=true;
    GetComponent.<AudioSource>().volume=1.0;
    GetComponent.<AudioSource>().PlayOneShot(getKeySound);
    Destroy(hit.gameObject);
  }
  
  if(hit.GetComponent.<Collider>().tag=="Door"){
    if(b_hasKey){
      GetComponent.<AudioSource>().volume=1.0;
      GetComponent.<AudioSource>().PlayOneShot(doorOpenSound);
      hit.gameObject.GetComponent.<Renderer>().material.mainTexture=doorOpenTexture;
      yield WaitForSeconds(1);
      Destroy(gameObject);
      hit.gameObject.GetComponent.<Renderer>().material.mainTexture=doorCloseTexture;
      restartButton.enabled=true;
    }
  }
}

function Update () {
//Debug.Log(linerenderer.name);
 if(!b_isJumping){
  if(Input.GetButton("Horizontal")||Input.GetAxis("Mouse X")){
     in_direction=(Input.GetAxis("Horizontal")<0)||(Input.GetAxis("Mouse X")<0)?-1:1;
     
     GetComponent.<Rigidbody>().velocity=new Vector3((in_direction*f_speed),GetComponent.<Rigidbody>().velocity.y,0);
     loopSprites[0].resetFrame();
     loopSprites[1].updateAnimation(in_direction,GetComponent.<Renderer>().material);
  }else{
     //Stay
     loopSprites[1].resetFrame();
     
     loopSprites[0].updateAnimation(in_direction,GetComponent.<Renderer>().material);
  }
   if(Input.GetButton("Jump")){
     b_isJumping=true;
     
     GetComponent.<AudioSource>().volume=0.3;
     GetComponent.<AudioSource>().PlayOneShot(jumpSound);
     loopSprites[0].resetFrame();
     loopSprites[1].resetFrame();
     GetComponent.<Rigidbody>().velocity=new Vector3(GetComponent.<Rigidbody>().velocity.x,-Physics.gravity.y,0);
   }
  }else{
    jumpSprite.updateJumpAnimation(in_direction,GetComponent.<Rigidbody>().velocity.y,GetComponent.<Renderer>().material);
  }
  
}

function LateUpdate(){
  var hit:RaycastHit;
  var v3_hit:Vector3=transform.TransformDirection(-Vector3.up)*(f_height*0.5);
  var v3_right:Vector3=new Vector3(transform.position.x+(GetComponent.<Collider>().bounds.size.x*0.45),transform.position.y,transform.position.z);
  var v3_left:Vector3=new Vector3(transform.position.x-(GetComponent.<Collider>().bounds.size.x*0.45),transform.position.y,transform.position.z);
  
  if(Physics.Raycast(transform.position,v3_hit,hit,2.5,layerMask.value)){
     b_isJumping=false;
  }else if(Physics.Raycast(v3_right,v3_hit,hit,2.5,layerMask.value)){
    if(b_isJumping){
      b_isJumping=false;
    }
     
  }else if(Physics.Raycast(v3_left,v3_hit,hit,2.5,layerMask.value)){
     if(b_isJumping){
       b_isJumping=false;
     }
     
  }else{
    if(!b_isJumping){
       if(Mathf.Floor(transform.position.y)==f_lastY){
         b_isJumping=false;
       }else{
         b_isJumping=true;
       }
    }
  }
  f_lastY=Mathf.Floor(transform.position.y);
  CameraC.transform.position=new Vector3(transform.position.x,transform.position.y,CameraC.transform.position.z);
  Camera.main.transform.position=new Vector3(transform.position.x,transform.position.y,Camera.main.transform.position.z);
}

public function OnDrawGizmos() : void {
	var mesh = GetComponent(MeshFilter).sharedMesh;
	f_height = mesh.bounds.size.y * transform.localScale.y;
	var v3_right : Vector3 = new Vector3(transform.position.x + (GetComponent.<Collider>().bounds.size.x*0.45), transform.position.y, transform.position.z);
	var v3_left : Vector3 = new Vector3(transform.position.x - (GetComponent.<Collider>().bounds.size.x*0.45), transform.position.y, transform.position.z);
	Gizmos.color = Color.red;
	Gizmos.DrawRay(transform.position, transform.TransformDirection (-Vector3.up) * (f_height * 0.5));
	Gizmos.DrawRay(v3_right, transform.TransformDirection (-Vector3.up) * (f_height * 0.5));
	Gizmos.DrawRay(v3_left, transform.TransformDirection (-Vector3.up) * (f_height * 0.5));
}


class SpriteManagerYU{
  public var spriteTexture:Texture2D;
  public var in_framePerSec:int;
  public var in_gridX:int;
  public var in_gridY:int;
  
  private var f_timePercent:float;
  private var f_nextTime:float;
  
  private var f_gridX:float;
  private var f_gridY:float;
  private var in_curFrame:int;
  
  public function init():void{
    f_timePercent=1.0/in_framePerSec;
    f_nextTime=f_timePercent;
    f_gridX=1.0/in_gridX;
    f_gridY=1.0/in_gridY;
    in_curFrame=1;
  }
  
  public function updateAnimation(_direction:int,_material:Material):void{
//    Debug.Log("updateAnimation");
     _material.mainTexture=spriteTexture;
     
     if(Time.time>f_nextTime){
       f_nextTime=Time.time+f_timePercent;
       in_curFrame++;
       if(in_curFrame>in_framePerSec){
         in_curFrame=1;
       }
       //Debug.Log(in_curFrame);
     }
     _material.mainTextureScale=new Vector2(_direction*f_gridX,f_gridY);
     var in_col:int=0;
     if(in_gridY>1){
       in_col=Mathf.Ceil(in_curFrame/in_gridX);
       
     }
     //Debug.Log(in_col);
     if(_direction==1){
       _material.mainTextureOffset=new Vector2(((in_curFrame+1)%in_gridX)*f_gridX,in_col*f_gridY);
//       Debug.Log("Direction 1");
     }else{
      _material.mainTextureOffset=new Vector2(((in_gridX+(in_curFrame))%in_gridX)*f_gridX,in_col*f_gridY);
//       Debug.Log("Direction -1");
     }
     
  }
  
  public function resetFrame():void{
    in_curFrame=1;
  }
}

class JumpSpriteManager{
  public var t_jumpStartTexture:Texture2D;
  public var t_jumpDownTexture:Texture2D;
  public var t_jumpAirTexture:Texture2D;
  
  public function updateJumpAnimation(_direction:int,_velocityY:float,_material:Material):void{
    if((_velocityY>=-2.0)&&(_velocityY<=2.0)){
      _material.mainTexture=t_jumpAirTexture;
    }else if(_velocityY>2.0){
      _material.mainTexture=t_jumpStartTexture;
    }else{
      _material.mainTexture=t_jumpDownTexture;
    }
    _material.mainTextureScale=new Vector2(_direction*1,1);
    _material.mainTextureOffset=new Vector2(_direction*1,1);
  }
  
}
@script RequireComponent(AudioSource)