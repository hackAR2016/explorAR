using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {



	public bool rotating;
	public string dir;
	public float rotationsPerMinute;
	// Use this for initialization
	void Start () {
		rotating = false;
		dir = "";
		rotationsPerMinute = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		if (rotating) {
			if (dir == "cw") {
				transform.Rotate (0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
			} else if (dir == "ccw") {
				transform.Rotate (0, -6.0f * rotationsPerMinute * Time.deltaTime, 0);
			}
		} else {
			transform.Rotate (0,0, 0);
		}
	
	}
}
