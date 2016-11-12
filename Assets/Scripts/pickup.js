public var collection:GameObject;
function Start () {

}

function Update () {
 //transform.Rotate(Vector3.up*Time.deltaTime*300,Space.World);
// Debug.Log("sss");
}

function OnTriggerEnter(coll:Collider){
  if(coll.name=="Player")
   {
   Instantiate(collection,transform.position,Quaternion.identity);
   Destroy(gameObject);
   }else if(coll.name=="DestroyObject"){
    Destroy(gameObject);
   }
//   Debug.Log(coll.name);
}