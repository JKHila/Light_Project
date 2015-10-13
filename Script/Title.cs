using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	public GameObject logoPanel;
	void Start()
	{
		StartCoroutine ("showLogo");
	}
	IEnumerator showLogo()
	{
		while (true) {
			yield return new WaitForSeconds (2);
			logoPanel.SetActive (false);
			//Time.timeScale = 0;
		}
	}
	public void title()
	{
		StopCoroutine ("showLogo");
		Debug.Log ("title");
		Application.LoadLevel (1);
	}
}
