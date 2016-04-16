using UnityEngine;
using System.Collections;
using Vuforia;

public class HouseSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("starting scene 2 ...");
		int index = PlayerPrefs.GetInt ("currentHouseIndex", 3);
		GameObject house = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/House" + (index + 1)));
		house.transform.localPosition = new Vector3(1, 1, 0);

		// create marker
		//GameObject marker = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/FrameMarker"));
		//marker.transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
