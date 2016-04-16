using UnityEngine;
using System.Collections;

public class fingerModel  {

	public int type;
	public int handId;
	public Vector3 tipPosition;
	public Vector3 btipPosition;
	public Vector3 pipPosition;
	public Vector3 dipPosition;
	public Vector3 mcpPosition;
	public Vector3 mcpDirection;
	public Vector3 intDirection;
	public Vector3 disDirection;
	public Vector3 mcpUpDirection;
	public Vector3 intUpDirection;
	public Vector3 disUpDirection;
	public Vector3 mcpFwdDirection;
	public Vector3 intFwdDirection;
	public Vector3 disFwdDirection;
	public GameObject disObj;
	public GameObject mcpObj;
	public Quaternion mcpInitialRotation;
	public GameObject intObj;
	public fingerModel(int t, int handId){
		type = t;
		this.handId = handId;
	}

	public fingerModel(){

	}
}
