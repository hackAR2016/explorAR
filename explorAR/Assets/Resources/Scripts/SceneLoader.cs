using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	private GameObject browser;

	// Use this for initialization
	void Start () {
		// load object browser
		int index = PlayerPrefs.GetInt("currentHouseIndex", 3);
		browser = GameObject.Find ("FrameMarker0/ObjectBrowser");
		browser.GetComponent<BrowserBehaviour> ().SetCurrentIndex (index);
	}	
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadHouseScene() {
		int index = browser.GetComponent<BrowserBehaviour> ().GetCurrentIndex ();
		Debug.Log ("loading house scene...");
		Destroy (browser);
		PlayerPrefs.SetInt ("currentHouseIndex", index);
		SceneManager.LoadScene (1);
	}
}
