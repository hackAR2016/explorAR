using UnityEngine;
using System.Collections;
using Explorar;

public class BrowserBehaviour : MonoBehaviour, IBrowser {

	private int houseCount = 5;
	private int currentHouseIndex = 3;

	public float smallScale = 0.5f;
	public float largeScale = 1f;

	private Quaternion start;
	private Quaternion target;
	private float speed = 0.5f;
	private float time = 0;
	private bool rotating = false;
	private float duration = 0.6f;

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

	public void SetCurrentIndex(int index) {
		currentHouseIndex = index;
		selectHouse (index);
	}

	public int GetCurrentIndex() {
		return currentHouseIndex;
	}

	// Update is called once per frame
	void Update () {
		if (rotating) {
			time += Time.deltaTime;
			transform.rotation = Quaternion.Lerp (start, target, time / duration);

			if (time >= duration) {
				time = 0;
				rotating = false;
			}
		}
	}

	public void ShowNext() {
		currentHouseIndex = (currentHouseIndex + 1) % houseCount;
		selectHouse (currentHouseIndex);
		start = transform.rotation;
		target = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3 (0, 0, 360 / 5));
		rotating = true;
	}

	public void ShowPrevious() {
		currentHouseIndex = currentHouseIndex - 1;
		if (currentHouseIndex == -1)
			currentHouseIndex = houseCount - 1;
		selectHouse (currentHouseIndex);
		start = transform.rotation;
		target = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3 (0, 0, 360 / 5));
		rotating = true;
	}

	private void selectHouse(int index) {
		for(int i = 0; i < houseCount; i++) {
			houses [i].transform.localScale = new Vector3 (smallScale, smallScale, smallScale);
		}
		houses [index].transform.localScale = new Vector3(largeScale, largeScale, largeScale);
	}

	public void SelectCurrent() {
		
	}
}
