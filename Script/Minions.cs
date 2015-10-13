using UnityEngine;
using System.Collections;

public class Minions : MonoBehaviour {
	private SpriteRenderer sr;
	private float hp;
	private float gColor;
	private float rColor;
	private int size=0;

	public float MaxHp = 3;
	public float speed;
	public bool isIn=false;
	public bool isGoal = false;
	public bool isOver = false;
	public Sprite img1;
	public Sprite img2;

	public GameObject handler;
	public GameObject[] collObj = new GameObject[2];
	public GameObject Dlight;
	void Start () 
	{
		hp = MaxHp;
		Dlight = GameObject.Find ("Directional light");
		handler = GameObject.Find ("Handler");

		sr = transform.GetComponent<SpriteRenderer>();
		gColor = Dlight.GetComponent<Light> ().color.g;
		rColor = Dlight.GetComponent<Light> ().color.r;

		StartCoroutine ("Idle");
		StartCoroutine ("subHP");
	}
	void Update () 
	{
		if(!isOver)
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		if (!isGoal)
		{
			if (!isIn) {
				sr.color = new Color (rColor * hp / 3,
				                     gColor * hp / 3,
				                     Dlight.GetComponent<Light> ().color.b);
			} else {
				hp = MaxHp;
				sr.color = new Color (255, 255, 255, 255);
				//가로등을 터치해서 불을 껐을때 판정
				if (collObj [0] && !collObj [0].activeSelf) 
				{
					if (collObj [1] && !collObj [1].activeSelf) 
					{
						isIn = false;
						size = 0;
					}
					else if(!collObj[1])
					{
						isIn = false;
						size = 0;
					}
				}
				else if(collObj [1] && !collObj [1].activeSelf)
				{
					if (collObj [0] && !collObj [0].activeSelf) 
					{
						isIn = false;
						size = 0;
					}
					else if(!collObj[0])
					{
						isIn = false;
						size = 0;
					}
				}
			}
			if (hp < 1) {
				isOver = true;
				StopCoroutine ("subHP");
				StopCoroutine ("Idle");
				handler.SendMessage ("GameOver");
				hp = MaxHp;
			}
		}
	}
	IEnumerator Idle ()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.1f);
			if( sr.sprite== img1)
				sr.sprite = img2;
			
			else
				sr.sprite= img1;
		}
	}
	IEnumerator subHP()
	{
		while(true)
		{
			yield return new WaitForSeconds (1.0f);
			if(!isIn && hp>0)
				hp--;
		}
	}
	//불빛이 겹치면 첫번째 불빛에만 판정이 가기때문에 현재 충돌중인 오브젝트를 불러와서 겹쳐진
	//불빛이 있다면 겹쳐진불빛을 coll에 넣고 그경우가 아닌 처음으로 불빛에 들어왔을때는 else에서 처리
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.name == "Goal")
		{
			isGoal = true;
			//Debug.Log ("goal");
			handler.SendMessage("reachGoal");
			StopCoroutine ("subHP");
		}

		if (coll.name == "Light") 
		{
			//같은 범위안에 들어왔는지 검사해서 아니면 빈 배열에 다른 범위 대입 
			if(collObj[size] != coll.gameObject)
			{
				if(collObj[0] != null)
				{
					if(size == 0)
						collObj[++size] = coll.gameObject;
					else
						collObj[--size] = coll.gameObject;
				}
				else
					collObj[size] = coll.gameObject;
				//Debug.Log (size);
			}
			else
			{
				//같은가로등이 켜졌다 꺼졌을때
				collObj[size] = coll.gameObject;
			}
			isIn = true;
		}
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.name == "Light") 
		{
			//배열 두개 검사해서 빠져나간 범위의 오브젝트는 null
			for(int i=0;i<2;i++)
			{
				if (collObj[i]==coll.gameObject)
				{
					collObj[i] = null;
					size=0;
				}
			}
			//두개 배열 모두 빠져나갔으면 off
			if(collObj[0] == null && collObj[1] == null)
			{
				isIn=false;
				size=0;
			}
		} 
	}

	// sprite object 입니다.

	


}
