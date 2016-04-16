using UnityEngine;
using System.Collections;
using Explorar;

public class BrowserBehaviour : MonoBehaviour, IBrowser {

	private int houseCount = 5;
	private int currentHouseIndex = 0;
	private Vector3 currentHousePos = new Vector3 (0, 0, 0);
	private Vector3 nextHousePos = new Vector3 (-10, 0, 0);
	private Vector3 prevHousePos = new Vector3 (10, 0, 0);

	private GameObject currentHouse;
	private GameObject prevHouse;
	private GameObject nextHouse;

	// Use this for initialization
	void Start () {
		updateHouses ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowNext() {
		currentHouseIndex = (currentHouseIndex + 1) % houseCount;
		updateHouses ();
	}

	public void ShowPrevious() {
		currentHouseIndex = currentHouseIndex -1;
		if (currentHouseIndex == -1)
			currentHouseIndex = houseCount - 1;
		updateHouses ();
	}

	private void updateHouses() {
		Destroy (prevHouse);
		Destroy (currentHouse);
		Destroy (nextHouse);

		int prev = currentHouseIndex - 1;
		if (prev == -1)
			prev = houseCount - 1;
		prev++;
		int next = ((currentHouseIndex + 1) % houseCount) + 1;
		
		prevHouse = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/House" + prev));
		currentHouse = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/House" + (currentHouseIndex + 1)));
		nextHouse = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/House" + next));

		prevHouse.transform.parent = gameObject.transform;
		currentHouse.transform.parent = gameObject.transform;
		nextHouse.transform.parent = gameObject.transform;

		prevHouse.transform.localPosition = prevHousePos;
		currentHouse.transform.localPosition = currentHousePos;
		nextHouse.transform.localPosition = nextHousePos;
	}
}
