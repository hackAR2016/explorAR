using UnityEngine;
using System.Collections;
using Explorar;

public class BrowserBehaviour : MonoBehaviour, IBrowser {

	public int ObjectCount = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowNext() {
		gameObject.transform.Rotate (new Vector3 (0, 360/ObjectCount, 0));
	}

	public void ShowPrevious() {
		gameObject.transform.Rotate (new Vector3 (0, -360/ObjectCount, 0));
	}
}
