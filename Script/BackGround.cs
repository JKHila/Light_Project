using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour {
	//public GameObject back;
	private Vector2 initMousePos;
	//sdfsfsd
	public float minY=8,maxY=23;

	void setPos(int i)
	{
		this.transform.position = new Vector2 (0, 23);
		minY = i;
	}
	
	void OnMouseDown()
	{
		if(Time.timeScale !=0)
		initMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}
	void OnMouseDrag()
	{
		if(Time.timeScale !=0)
		{
			Vector2 worldpoint;
			worldpoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 diffPos = worldpoint - initMousePos;

			initMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = new Vector2 (0,Mathf.Clamp ((transform.position.y + diffPos.y),minY,maxY));
		}
	}

}