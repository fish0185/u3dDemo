#pragma strict
public var collection:GameObject;
private var rota:float=0;
private var xposition:float;
function Start () {
 xposition=transform.position.x*5;
 transform.rotation =Quaternion.Euler(Vector3(90, xposition,0));
// print(transform.position.x);
//xposition=transform.position.x;
//transform.eulerAngles = Vector3(90, 45, 0);
}

function Update () {
 //transform.Rotate(new Vector3(0,1,0)*Time.deltaTime*300,Space.World);
 //transform.Rotate(0,Time.deltaTime*300,0,Space.World);
// Debug.Log("sss");
rota+=Time.deltaTime*300;
transform.rotation =Quaternion.Euler(Vector3(90, -(xposition+rota),0));
//rota+=Time.deltaTime*300;
//transform.rotation =Quaternion.Euler(Vector3(90, transform.position.x+rota,0));
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