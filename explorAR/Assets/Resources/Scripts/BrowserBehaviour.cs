using UnityEngine;
using System.Collections;
using Explorar;

public class BrowserBehaviour : MonoBehaviour, IBrowser {

	private int houseCount = 5;
	private int currentHouseIndex = 3;

	public float scale = 2.5f;
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

		selectHouse (currentHouseIndex, -1);
	}

	public void SetCurrentIndex(int index) {
		currentHouseIndex = index;
		selectHouse (index, -1);
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
		int temp = currentHouseIndex;
		currentHouseIndex = (currentHouseIndex + 1) % houseCount;
		selectHouse (currentHouseIndex, temp);
		start = transform.rotation;
		target = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3 (0, 0, 360 / 5));
		rotating = true;
	}

	public void ShowPrevious() {
		int temp = currentHouseIndex;
		currentHouseIndex = currentHouseIndex - 1;
		if (currentHouseIndex == -1)
			currentHouseIndex = houseCount - 1;
		selectHouse (currentHouseIndex, temp);
		start = transform.rotation;
		target = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3 (0, 0, 360 / 5));
		rotating = true;
	}

	private void selectHouse(int index, int previousIndex) {
		if (previousIndex != -1)
			houses [previousIndex].transform.localScale /= scale;
		houses [index].transform.localScale *= scale;
	}

	public void SelectCurrent() {
		
	}
}
