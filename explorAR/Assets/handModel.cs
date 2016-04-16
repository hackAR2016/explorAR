using UnityEngine;
using System.Collections;
using System;
using WebSocketSharp;
using System.Threading;
using SimpleJSON;


public class handModel  {

	public GameObject handObj;
	private Vector3 palmPosition;
	public Vector3  initialPalmPosition;
	private string type;
	public int id;
	private Vector3 palmRotation;
	public Vector3 palmNormal;
	public Vector3 initialPalmRotation;
	public Quaternion initialArmRotation;
	public Quaternion initialHandRotation;
	private Vector3 stabilizedPalmPosition;
	public Vector3 palmDirection;
	public bool valid;
	public GameObject forearm;
	public GameObject hand;
	public fingerModel[] fingers;
	public Vector3 elbowPosition;
	public Vector3 wristPosition;
	
	
	public handModel(string type){
		
		palmRotation = new Vector3 ();
		palmDirection = new Vector3 ();
		stabilizedPalmPosition = new Vector3 ();
		palmPosition = new Vector3 ();
		elbowPosition = new Vector3 ();
		type = null;
		id = 0;
		valid = false;
		fingers = new fingerModel[5];
		for (int i = 0; i < fingers.Length; i++) {
			fingers[i]=new fingerModel();
		}
	}
	
	public string getType(){
		return this.type;
	}
	public Vector3 getPalmPos(){
		return this.palmPosition;
	}
	
	public Vector3 getStabilizedPalmPos(){
		return this.stabilizedPalmPosition;
	}
	
	public Vector3 getPalmRotation(){
		return this.palmRotation;
	}
	
	public void setType(string t){
		this.type = t;
	}
	
	public void setPalmPos(Vector3 p){
		this.palmPosition = p;
	}
	
	public void setPalmRotation(Vector3 p){
		this.palmRotation = p;
	}
	
	public void setStabilizedPalmPos(Vector3 p){
		this.stabilizedPalmPosition = p;
	}
	
	public handModel Clone(){
		handModel temp = new handModel (this.type);
		temp.setPalmPos(new Vector3(this.palmPosition.x,this.palmPosition.y,this.palmPosition.z));
		temp.setStabilizedPalmPos (new Vector3 (this.stabilizedPalmPosition.x, this.stabilizedPalmPosition.y, this.stabilizedPalmPosition.z));
		temp.setPalmRotation (new Vector3 (this.palmRotation.x, this.palmRotation.y, this.palmRotation.z));
		return temp;
	}
}
