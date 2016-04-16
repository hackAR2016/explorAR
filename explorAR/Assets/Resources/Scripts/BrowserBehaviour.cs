using UnityEngine;
using System.Collections;
using Explorar;

public class BrowserBehaviour : MonoBehaviour, IBrowser {

	private int houseCount = 5;
	private int currentHouseIndex = 0;

	public float smallScale = 0.5f;
	public float largeScale = 1f;

	private GameObject[] houses = new GameObject[5];

	// Use this for initialization
	void Start () {
		houses[0] = GameObject.Find ("FrameMarker0/ObjectBrowser/House1");
		houses[1] = GameObject.Find ("FrameMarker0/ObjectBrowser/House2");
		houses[2] = GameObject.Find ("FrameMarker0/ObjectBrowser/House3");
		houses[3] = GameObject.Find ("FrameMarker0/ObjectBrowser/House4");
		houses[4] = GameObject.Find ("FrameMarker0/ObjectBrowser/House5");

		selectHouse (currentHouseIndex);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowNext() {
		currentHouseIndex = (currentHouseIndex + 1) % houseCount;
		selectHouse (currentHouseIndex);
	}

	public void ShowPrevious() {
		currentHouseIndex = currentHouseIndex -1;
		if (currentHouseIndex == -1)
			currentHouseIndex = houseCount = 4;
		selectHouse (currentHouseIndex);
	}

	private void selectHouse(int index) {
		for(int i = 0; i < houseCount; i++) {
			if (i != index) {
				houses [i].transform.localScale = new Vector3(smallScale, smallScale, smallScale);
			}
		}
		houses [index].transform.localScale = new Vector3(largeScale, largeScale, largeScale);
	}
}
