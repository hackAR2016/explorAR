using UnityEngine;
using System.Collections;

namespace Explorar {
	public class BrowserLoader {

		public void ShowBrowser() {
			GameObject.Instantiate (Resources.Load<GameObject> (""));
		}
	}
}