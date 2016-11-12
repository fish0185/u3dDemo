#pragma strict

function Start () {

}

function Update () {
 transform.Rotate(Vector3.up*Time.deltaTime*300,Space.World);
// Debug.Log("sss");
}

