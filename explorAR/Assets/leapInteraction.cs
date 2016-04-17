using UnityEngine;
using System.Collections;
using System;
using System.IO;
using WebSocketSharp;
using System.Threading;
using SimpleJSON;



public class leapInteraction : MonoBehaviour {

	private WebSocket ws;
	private Thread receiveThread;
	private Thread saveImgThread;
	private string frame;
	public handModel leftHand;
	public handModel rightHand;
	public handModel prevLeftHand;
	private handModel prevRightHand;
	public JSONNode jsonFrame;
	public float scale;
	public GameObject TV;
	public WWW www;
	public WebCamTexture text;
	public int frameRate;
	public float rotationsPerMinute = 10.0f;
	float timer;
	public float maxTimer;
	bool gestureRecognized;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 30;
		scale = 200f;
		timer = maxTimer;
		gestureRecognized = false;
		rightHand = new handModel ("right");
		prevRightHand = new handModel ("right");
		prevLeftHand = new handModel ("left");
		leftHand = new handModel ("left");
		//		rightHand.handObj = GameObject.Find ("male_arm_right");
		//		rightHand.forearm = GameObject.Find ("Bip01 R Forearm");
		//		rightHand.initialArmRotation = rightHand.forearm.transform.rotation;
		//		rightHand.hand = GameObject.Find ("Bip01 R Hand");
		//		rightHand.fingers [0].disObj = GameObject.Find ("Bip01 R Finger02");
		//		rightHand.fingers [0].intObj = GameObject.Find ("Bip01 R Finger01");
		//		rightHand.fingers [0].mcpObj = GameObject.Find ("Bip01 R Finger0");
		//		rightHand.fingers [1].disObj = GameObject.Find ("Bip01 R Finger12");
		//		rightHand.fingers [1].intObj = GameObject.Find ("Bip01 R Finger11");
		//		rightHand.fingers [1].mcpObj = GameObject.Find ("Bip01 R Finger1");
		//		rightHand.fingers [2].disObj = GameObject.Find ("Bip01 R Finger22");
		//		rightHand.fingers [2].intObj = GameObject.Find ("Bip01 R Finger21");
		//		rightHand.fingers [2].mcpObj = GameObject.Find ("Bip01 R Finger2");
		//		rightHand.fingers [3].disObj = GameObject.Find ("Bip01 R Finger32");
		//		rightHand.fingers [3].intObj = GameObject.Find ("Bip01 R Finger31");
		//		rightHand.fingers [3].mcpObj = GameObject.Find ("Bip01 R Finger3");
		//		rightHand.fingers [4].disObj = GameObject.Find ("Bip01 R Finger42");
		//		rightHand.fingers [4].intObj = GameObject.Find ("Bip01 R Finger41");
		//		rightHand.fingers [4].mcpObj = GameObject.Find ("Bip01 R Finger4");
		//		leftHand.handObj = GameObject.Find ("male_arm_left");
		//		leftHand.forearm = GameObject.Find ("Bip01 R Forearm001");
		//		leftHand.initialArmRotation = leftHand.forearm.transform.rotation;
		//		leftHand.hand = GameObject.Find ("Bip01 R Hand001");
		//		leftHand.fingers [0].disObj = GameObject.Find ("Bip01 R Finger045");
		//		leftHand.fingers [0].intObj = GameObject.Find ("Bip01 R Finger044");
		//		leftHand.fingers [0].mcpObj = GameObject.Find ("Bip01 R Finger043");
		//		leftHand.fingers [1].disObj = GameObject.Find ("Bip01 R Finger048");
		//		leftHand.fingers [1].intObj = GameObject.Find ("Bip01 R Finger047");
		//		leftHand.fingers [1].mcpObj = GameObject.Find ("Bip01 R Finger046");
		//		leftHand.fingers [2].disObj = GameObject.Find ("Bip01 R Finger051");
		//		leftHand.fingers [2].intObj = GameObject.Find ("Bip01 R Finger050");
		//		leftHand.fingers [2].mcpObj = GameObject.Find ("Bip01 R Finger049");
		//		leftHand.fingers [3].disObj = GameObject.Find ("Bip01 R Finger054");
		//		leftHand.fingers [3].intObj = GameObject.Find ("Bip01 R Finger053");
		//		leftHand.fingers [3].mcpObj = GameObject.Find ("Bip01 R Finger052");
		//		leftHand.fingers [4].disObj = GameObject.Find ("Bip01 R Finger057");
		//		leftHand.fingers [4].intObj = GameObject.Find ("Bip01 R Finger055");
		//		leftHand.fingers [4].mcpObj = GameObject.Find ("Bip01 R Finger055");


		ws = new WebSocket ("ws://localhost:8081");

		init ();

		//		text.videoVerticallyMirrored = true;
		//TV.GetComponent<Renderer>().material.mainTexture = text;
		//	Vector3 ang = TV.GetComponent<Renderer>().transform.eulerAngles;
		//	ang.y = ang.y + 180;
		//	TV.GetComponent<Renderer>().transform.eulerAngles = ang;




	}

	// Update is called once per frame
	void Update () {

		if (ws.IsAlive) {
			//Debug.Log ("alive");
		} else {
			Debug.Log("tryin' to connect");
			ws.Connect();
		}

		if (!(frame.IsNullOrEmpty())) {
			jsonFrame = JSON.Parse (frame);
			if(jsonFrame["hands"].Count>0){
				ParseFrame(jsonFrame);
			}else{
//				GameObject.Find ("model").GetComponent<rotate> ().rotating = false;
				leftHand.valid = false;
				//				leftHand.handObj.GetComponent<Renderer>().enabled=false;
				rightHand.valid = false;
				//				rightHand.handObj.GetComponent<Renderer>().enabled=false;
			}
		}

		if (gestureRecognized && timer > 0) {
			timer -= Time.deltaTime;
		} else if (gestureRecognized && timer <= 0) {
			gestureRecognized = false;
			timer = maxTimer;
			Debug.Log ("timer reset");
		}

		//Debug.Log (gestureRecognized);




	}
	//
	//	IEnumerator loadImagine (string url){
	//		www = new WWW(url);
	//		yield return www;
	//		while(!www.isDone){}
	//		GameObject TV = GameObject.Find ("TV");
	//		TV.renderer.material.mainTexture = www.texture;
	//
	//
	//	}
	//
	//	void SaveTextureToFile( Texture2D texture, string filePath)
	//	{
	//		byte[] bytes=texture.EncodeToJPG(); 
	//		File.WriteAllBytes (filePath, bytes);
	//		
	//	}
	//

	public void ParseFrame(JSONNode jsonFrame){
		leftHand.valid = false;
		rightHand.valid = false;
		//
		//		for (int i = 0; i < jsonFrame ["gestures"].Count; i++) {
		//			if (jsonFrame ["gestures"] [i] != null ) {
		//				string type = jsonFrame ["gestures"] [i] ["type"];
		//				Debug.Log (jsonFrame ["gestures"]);
		//			}
		//		
		//		}


		if (jsonFrame ["hands"].Count == 1) {
			string handType = jsonFrame ["hands"] [0] ["type"];
			if (handType == "right") {
				rightHand.valid = true;
				leftHand.valid = false;
				//
			} else if (handType == "left") {
				leftHand.valid = true;
				rightHand.valid = false;

			}
		}else if(jsonFrame ["hands"].Count == 2){
//			Vector3 hand1 = new Vector3 (float.Parse(jsonFrame ["hands"] [0] ["palmPosition"] [0]),
//				float.Parse( jsonFrame ["hands"] [0] ["palmPosition"] [1]), float.Parse(jsonFrame ["hands"] [0] ["palmPosition"] [2]));
//			Vector3 hand2 = new Vector3 (float.Parse(jsonFrame ["hands"] [1] ["palmPosition"] [0]),
//				float.Parse(jsonFrame ["hands"] [1] ["palmPosition"] [1]), float.Parse(jsonFrame ["hands"] [1] ["palmPosition"] [2]));
//			Debug.Log(Vector3.Distance(hand1,hand2));
////			GameObject.Find ("model").GetComponent<expode> ().distance (Vector3.Distance (hand1, hand2));
			/// 
			GameObject.Find("Manager").GetComponent<SceneLoader>().LoadHouseScene();
			gestureRecognized = true;
			Debug.Log("right & left");

		} else {
			Debug.Log ("no hand");
		}

		if(leftHand.valid && rightHand.valid){
			
		}else if(leftHand.valid && !rightHand.valid && !gestureRecognized){

			//			rightHand.handObj.GetComponent<Renderer>().enabled=false;
			GameObject.Find("ObjectBrowser").GetComponent<BrowserBehaviour>().ShowPrevious();
			gestureRecognized = true;
			Debug.Log("left");

		}else if(!leftHand.valid && rightHand.valid && !gestureRecognized){
			//			leftHand.handObj.GetComponent<Renderer>().enabled=false;
			GameObject.Find("ObjectBrowser").GetComponent<BrowserBehaviour>().ShowNext();
			gestureRecognized = true;
			Debug.Log("right");

		}else if(!leftHand.valid && !rightHand.valid){
			Debug.Log("no hands");


		}


		for (int h=0; h<jsonFrame["hands"].Count; h++) {
			string type = jsonFrame ["hands"][h]["type"];
			//			string temp = jsonFrame ["hands"] [h] ["palmPosition"] [0];
			//			float x = float.Parse (temp)/(scale/12f) ;
			//			temp = jsonFrame ["hands"] [h] ["palmPosition"] [1];
			//			float y = (float.Parse (temp)/(scale/15f)) -8f;
			//			temp = jsonFrame ["hands"] [h] ["palmPosition"] [2];
			//			float z = (-float.Parse (temp)/(scale/4f));
			//			Vector3 pos = new Vector3 (x, y, z);
			//			
			//			//gets palm normal 
			//			temp = jsonFrame ["hands"] [h] ["palmNormal"] [0];
			//			x = float.Parse (temp);
			//			temp = jsonFrame ["hands"] [h] ["palmNormal"] [1];
			//			y = float.Parse (temp);			
			//			temp = jsonFrame ["hands"] [h] ["palmNormal"] [2];
			//			z = float.Parse (temp);
			//			Vector3 normal = new Vector3 (x, y, -z);
			//			normal = normal.normalized;
			//			//angles from normal
			//			float rotx = ((float)Mathf.Atan2 (y, z) * Mathf.Rad2Deg ); 
			//			float rotz = ((float)Mathf.Atan2 (y, x) * Mathf.Rad2Deg );
			//			temp = jsonFrame ["hands"] [h] ["direction"] [0];
			//			x = float.Parse (temp);
			//			temp = jsonFrame ["hands"] [h] ["direction"] [1];
			//			y = float.Parse (temp);
			//			temp = jsonFrame ["hands"] [h] ["direction"] [2];			 
			//			z = float.Parse (temp);
			//			Vector3 direction = new Vector3(x,y,z);
			//			direction=direction.normalized;
			//			rotx = ((float)Mathf.Atan2 (y, z) * Mathf.Rad2Deg );
			//			float roty = Mathf.Atan2 (x, z) * Mathf.Rad2Deg;
			//
			//			temp = jsonFrame ["hands"] [h] ["elbow"] [0];
			//			x = float.Parse (temp)/(scale/4f) ;
			//			temp = jsonFrame ["hands"] [h] ["elbow"] [1];
			//			y = (float.Parse (temp)/(scale/2.8f)) -8f;
			//			temp = jsonFrame ["hands"] [h] ["elbow"] [2];
			//			z = (-float.Parse (temp)/(scale/2.8f));
			//			Vector3 elbow = new Vector3(x,y,z);
			//
			//			temp = jsonFrame ["hands"] [h] ["wrist"] [0];
			//			x = float.Parse (temp)/(scale/4f) ;
			//			temp = jsonFrame ["hands"] [h] ["wrist"] [1];
			//			y = (float.Parse (temp)/(scale/2.8f)) -8f;
			//			temp = jsonFrame ["hands"] [h] ["wrist"] [2];
			//			z = (-float.Parse (temp)/(scale/2.8f));
			//			Vector3 wrist = new Vector3(x,y,z);

			if(type=="right"){
				rightHand.valid=true;
				leftHand.valid=false;
				//
			}else if(type=="left"){
				leftHand.valid=true;
				rightHand.valid=false;

			}





		}

//		if(leftHand.valid && rightHand.valid){
//			Debug.Log("right & left");
//		}else if(leftHand.valid && !rightHand.valid){
//			//			rightHand.handObj.GetComponent<Renderer>().enabled=false;
//			Debug.Log("left");
//			GameObject.Find ("model").GetComponent<rotate> ().rotating = true;
//			GameObject.Find ("model").GetComponent<rotate> ().dir = "cw";
//		}else if(!leftHand.valid && rightHand.valid){
//			//			leftHand.handObj.GetComponent<Renderer>().enabled=false;
//			Debug.Log("right");
//			GameObject.Find ("model").GetComponent<rotate> ().rotating = true;
//			GameObject.Find ("model").GetComponent<rotate> ().dir = "ccw";
//		}else if(!leftHand.valid && !rightHand.valid){
//			Debug.Log("no hands");
//			GameObject.Find ("model").GetComponent<rotate> ().rotating = false;
//
//		}

		//		for (int p = 0; p<jsonFrame["pointables"].Count; p++) {
		//			int hId = int.Parse(jsonFrame["pointables"][p]["handId"]);
		//			fingerModel f = new fingerModel();
		//			if(leftHand.id==hId){
		//				f = leftHand.fingers[int.Parse(jsonFrame["pointables"][p]["type"])];
		//			}else{
		//				f = rightHand.fingers[int.Parse(jsonFrame["pointables"][p]["type"])];
		//			}
		//			f.handId = int.Parse(jsonFrame["pointables"][p]["handId"]);
		////			Debug.Log(jsonFrame["pointables"][p]["bases"].Count);
		//			f.mcpDirection = new Vector3 (float.Parse(jsonFrame["pointables"][p]["bases"][1][0][0]),
		//			                              float.Parse(jsonFrame["pointables"][p]["bases"][1][0][1]),
		//			                              -float.Parse(jsonFrame["pointables"][p]["bases"][1][0][2]));
		//			f.intDirection = new Vector3 (float.Parse(jsonFrame["pointables"][p]["bases"][2][0][0]),
		//			                              float.Parse(jsonFrame["pointables"][p]["bases"][2][0][1]),
		//			                              -float.Parse(jsonFrame["pointables"][p]["bases"][2][0][2]));
		//			f.disDirection = new Vector3 (float.Parse(jsonFrame["pointables"][p]["bases"][3][0][0]),
		//			                              float.Parse(jsonFrame["pointables"][p]["bases"][3][0][1]),
		//			                             -float.Parse(jsonFrame["pointables"][p]["bases"][3][0][2]));
		//			f.mcpUpDirection = new Vector3 (float.Parse(jsonFrame["pointables"][p]["bases"][1][1][0]),
		//			                              float.Parse(jsonFrame["pointables"][p]["bases"][1][1][1]),
		//			                              -float.Parse(jsonFrame["pointables"][p]["bases"][1][1][2]));
		//			f.intUpDirection = new Vector3 (float.Parse(jsonFrame["pointables"][p]["bases"][2][1][0]),
		//			                              float.Parse(jsonFrame["pointables"][p]["bases"][2][1][1]),
		//			                              -float.Parse(jsonFrame["pointables"][p]["bases"][2][1][2]));
		//			f.disUpDirection = new Vector3 (float.Parse(jsonFrame["pointables"][p]["bases"][3][1][0]),
		//			                              float.Parse(jsonFrame["pointables"][p]["bases"][3][1][1]),
		//			                              -float.Parse(jsonFrame["pointables"][p]["bases"][3][1][2]));
		//			f.mcpFwdDirection = new Vector3 (float.Parse(jsonFrame["pointables"][p]["bases"][1][2][0]),
		//			                                float.Parse(jsonFrame["pointables"][p]["bases"][1][2][1]),
		//			                                -float.Parse(jsonFrame["pointables"][p]["bases"][1][2][2]));
		//			f.intFwdDirection = new Vector3 (float.Parse(jsonFrame["pointables"][p]["bases"][2][2][0]),
		//			                                float.Parse(jsonFrame["pointables"][p]["bases"][2][2][1]),
		//			                                -float.Parse(jsonFrame["pointables"][p]["bases"][2][2][2]));
		//			f.disFwdDirection = new Vector3 (float.Parse(jsonFrame["pointables"][p]["bases"][3][2][0]),
		//			                                float.Parse(jsonFrame["pointables"][p]["bases"][3][2][1]),
		//			                                -float.Parse(jsonFrame["pointables"][p]["bases"][3][2][2]));
		//
		//		}

		//		if (leftHand.valid) {
		//			for(int p = 0; p < leftHand.fingers.Length; p++){
		//				Quaternion mcpRotation = Quaternion.LookRotation(leftHand.fingers[p].mcpDirection,leftHand.fingers[p].mcpUpDirection);
		//				Quaternion intRotation = Quaternion.LookRotation(leftHand.fingers[p].intDirection,leftHand.fingers[p].intUpDirection);
		//				Quaternion disRotation = Quaternion.LookRotation(leftHand.fingers[p].disDirection,leftHand.fingers[p].disUpDirection);
		//
		//				leftHand.fingers[p].mcpObj.transform.rotation = mcpRotation;
		//				leftHand.fingers[p].intObj.transform.rotation = intRotation;
		//				leftHand.fingers[p].disObj.transform.rotation = disRotation;
		//			}
		//		}
		//
		//		if (rightHand.valid) {
		//			for(int p = 0; p < rightHand.fingers.Length; p++){
		//				Quaternion mcpRotation = Quaternion.LookRotation(-rightHand.fingers[p].mcpDirection,-rightHand.fingers[p].mcpUpDirection);
		//				Quaternion intRotation = Quaternion.LookRotation(-rightHand.fingers[p].intDirection,-rightHand.fingers[p].intUpDirection);
		//				Quaternion disRotation = Quaternion.LookRotation(-rightHand.fingers[p].disDirection,-rightHand.fingers[p].disUpDirection);
		//				
		//				rightHand.fingers[p].mcpObj.transform.rotation = mcpRotation;
		//				rightHand.fingers[p].intObj.transform.rotation = intRotation;
		//				rightHand.fingers[p].disObj.transform.rotation = disRotation;
		//			}
		//		}

	}



	void init(){
		receiveThread = new Thread(
			new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
	}

	private  void ReceiveData(){
		ws.OnMessage += ( sender,  e) => frame = e.Data;
	}


}
