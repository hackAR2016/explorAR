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
			gameObject.GetComponent<Animation> ().Play ("pullApart");
			exploded = true;
		}if (distance < 250f && exploded) {
			gameObject.GetComponent<Animation> ().Play ("pullTogether");
			exploded = false;
		}
	}
}
