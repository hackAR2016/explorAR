using UnityEngine;
using System.Collections;

namespace Explorar {
	public interface IBrowser {

		void ShowNext();
		void ShowPrevious();
		void SetCurrentIndex(int index);
		int GetCurrentIndex();
	}
}