using UnityEngine;
using System.Collections;

public class OnLight : MonoBehaviour {
	private bool isLight=false;

	public GameObject hand;
	public GameObject collObj;
	void Start()
	{
	}
	public void SetLight()
	{
		if (!isLight) 
		{
			if(hand.transform.GetComponent<Handler>().NumOfLight>0)
			{
				isLight = true;
				hand.SendMessage("SetLightNum",-1);
				this.gameObject.SetActive(true);
			}
		}
		else
		{
			hand.SendMessage("SetLightNum",1);
			isLight = false;
			this.gameObject.SetActive(false);
		}
	}
	public void Offlight()
	{
		if(isLight)
		{
			isLight = false;
			this.gameObject.SetActive(false);
		}
	}
	public void Onlight()
	{
		if(!isLight)
		{
			isLight = true;
			this.gameObject.SetActive(true);
		}
	}
	public void AddRange()
	{
		this.transform.GetComponent<Light> ().range ++;
		this.transform.GetComponent<CircleCollider2D> ().radius += 0.1f;
	}
}
