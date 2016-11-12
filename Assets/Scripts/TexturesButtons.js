#pragma strict

public var normalTexture:Texture2D;
public var rollOverTexture:Texture2D;
public var clickSound:AudioClip;
public var key:GameObject;
public var player:GameObject;

function Start(){
 Debug.Log("Start");
}
function Update(){
//  print("update");
}
function OnMouseEnter(){
 GetComponent.<GUITexture>().texture=rollOverTexture;
 Debug.Log("OnMouse Enter");
}

function OnMouseExit(){
 GetComponent.<GUITexture>().texture=normalTexture;
 Debug.Log("OnMouse Enter");
}

function OnMouseUp():IEnumerator{
 GetComponent.<AudioSource>().PlayOneShot(clickSound);
 yield new WaitForSeconds(1.0);
 //Instantiate(player, new Vector3(player.transform.position.x,player.transform.position.y,0.0),player.transform.rotation);
 //Instantiate(key, new Vector3(key.transform.position.x,key.transform.position.y,0.0),key.transform.rotation);
 GetComponent.<GUITexture>().enabled=false;
 Application.LoadLevel("Level1");
}


@script RequireComponent(AudioSource)