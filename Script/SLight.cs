using UnityEngine;
using System.Collections;

public class SLight: MonoBehaviour {
	public Transform C_light;
	public GameObject hand;

	void Start () {
		hand = GameObject.Find ("Handler");
		C_light = this.transform.FindChild ("Light");
		C_light.GetComponent<Light> ().range = 7;
		C_light.GetComponent<CircleCollider2D> ().radius = 3.4f;
	}
	public void OnMouseDown()
	{
		AudioSource.PlayClipAtPoint(hand.GetComponent<Handler>().SE_click,transform.position);
		if(Time.timeScale !=0)
		C_light.GetComponent<OnLight>().SetLight ();
		//Debug.Log ("click");
	}
	public void Onlight()
	{
		C_light.GetComponent<OnLight>().Onlight ();
	}
	public void Offlight()
	{
		//C_light.SendMessage ("Offlight");
		C_light.GetComponent<OnLight>().Offlight ();
	}
}
