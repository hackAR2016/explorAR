using UnityEngine;
using System.Collections;
using Explorar;

public class BrowserBehaviour : MonoBehaviour, IBrowser {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowNext() {
		gameObject.transform.Rotate (new Vector3 (0, 0, 360 / 5));
	}

	public void ShowPrevious() {
		gameObject.transform.Rotate (new Vector3 (0, 0, -360 / 5));
	}
}
