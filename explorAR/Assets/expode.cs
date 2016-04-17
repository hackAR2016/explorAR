using UnityEngine;
using System.Collections;

public class expode : MonoBehaviour {



	public bool exploded;
	// Use this for initialization
	void Start () {
		exploded = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void distance(float distance){
		Debug.Log (PlayerPrefs.GetInt ("currentHouseIndex"));
		if (distance > 250f && !exploded) {
			if (PlayerPrefs.GetInt ("currentHouseIndex") == 0 || PlayerPrefs.GetInt ("currentHouseIndex") == 2 || PlayerPrefs.GetInt ("currentHouseIndex") == 4 ) {
				gameObject.GetComponent<Animation> ().Play ("pullApart");
			} else if (PlayerPrefs.GetInt ("currentHouseIndex") == 1 || PlayerPrefs.GetInt ("currentHouseIndex") == 3 ){
				Debug.Log("Cliups "+gameObject.GetComponent<Animation> ().GetClipCount ());
				gameObject.GetComponent<Animation> ().Play ("explode");

			}
			exploded = true;
		}if (distance < 250f && exploded) {
			if (PlayerPrefs.GetInt ("currentHouseIndex") == 0 || PlayerPrefs.GetInt ("currentHouseIndex") == 2 || PlayerPrefs.GetInt ("currentHouseIndex") == 4 ) {
				gameObject.GetComponent<Animation> ().Play ("pullTogether");
			} else if (PlayerPrefs.GetInt ("currentHouseIndex") == 1 || PlayerPrefs.GetInt ("currentHouseIndex") == 3 ){
				gameObject.GetComponent<Animation> ().Play ("implode");
			}
			exploded = false;
		}
	}
}
