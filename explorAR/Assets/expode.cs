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
	
		if (distance > 250f && !exploded) {
			if (PlayerPrefs.GetInt ("currentHouseIndex") == 1) {
				gameObject.GetComponent<Animation> ().Play ("pullApart");
			} else if (PlayerPrefs.GetInt ("currentHouseIndex") == 2){
				gameObject.GetComponent<Animation> ().Play ("explode2");
			}
			exploded = true;
		}if (distance < 250f && exploded) {
			if (PlayerPrefs.GetInt ("currentHouseIndex") == 1) {
				gameObject.GetComponent<Animation> ().Play ("pullTogether");
			} else if (PlayerPrefs.GetInt ("currentHouseIndex") == 2){
				gameObject.GetComponent<Animation> ().Play ("implode2");
			}
			exploded = false;
		}
	}
}
