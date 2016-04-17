#pragma strict

var allChildren = gameObject.GetComponentsInChildren(Transform);

function Start () {
	for (var child : Transform in allChildren) {
    	Debug.Log("the child is: " + child);
	}
}

function Update () {

}