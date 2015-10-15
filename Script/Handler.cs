using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Handler : MonoBehaviour
{
	//public SLight[] slt;
	//public Minions[] mini

	//public GameObject[] SL;
	private bool isStarted = false;
	private bool isCreditOn = false;
	public GameObject[] kindImage;
	public GameObject[] kindMini;
	public GameObject[] G_mini;
	public GameObject[] OutImage;
	public GameObject[] btn;
	public GameObject[] selectStageBtn;
	public Transform[] spawnPoint;
	public Text text;
	public Text startText;
	public Text spNum;
	public Text timeText;
	public GameObject creditPanel;
	public GameObject SelectPanel;
	public GameObject IFN_clearPanel;
	public GameObject clearPanel;
	public GameObject Gover;
	public GameObject Spanel;
	public GameObject pauseImg;
	public GameObject pMini;
	public GameObject back;
	public GameObject goal;
	public GameObject canvas;
	public GameObject pauseBtn;
	public GameObject ExitPanel;
	public AudioClip SE_clear;
	public AudioClip SE_gameover;
	public AudioClip SE_click;
	public AudioClip SE_pause;
	public int totalMinion;
	public int Minion;
	public int NumOfLight;
	public int level = 0;

	//public int NumLimit;
	// Use this for initialization
	void Awake ()
	{
		//Time.timeScale = 0;
		Screen.SetResolution (720, 1280, true);
		if(PlayerPrefs.GetInt("First_Start") == 0)
		{
			Data.numLimit = 2;
			PlayerPrefs.SetInt("numLight",Data.numLimit);
			Debug.Log("처음 시작했음");
			PlayerPrefs.SetInt("First_Start", 1);
			
			//세이브과정이 없다면 위 값이 저장되지 않음.
			//같은 메소드 내에서는 저장하지 않아도 되므로 메소드 맨 마지막에 한 번만 하면 됨.
			PlayerPrefs.Save();
		}
		//만일 값이 1 일경우 이미 한번 플레이를 했으니 처음과정을 스킵함.
		else
		{
			Data.numLimit = PlayerPrefs.GetInt("numLight");
			Data.isClear = PlayerPrefs.GetInt("isClear");
			Debug.Log("처음이 아님");
		}

		setMenu ();
			//for test

	}

	public void setMenu()
	{
		int stagenum;
		Transform num;
		for(int i = 0;i<7;i++)
		{
			num = selectStageBtn[i].transform.FindChild ("Text");
			string a = num.GetComponent<Text>().text;
			stagenum = int.Parse(a);
			//Debug.Log (stagenum);
			if (Data.isClear > stagenum-2)
				selectStageBtn[i].gameObject.GetComponent<Button> ().interactable = true;
		}
	}
	public void StageStart(int i)
	{
		Time.timeScale = 1;
		Data.stageNum = i;
		if (Data.stageNum - 1 < 0) {
			Data.numLimit = 3;
		} else
			level = Data.stageNum - 1;
		int rlevel = level + 1;
		spNum.text += Data.numLimit.ToString ();
		startText.text += "  " + rlevel.ToString (); //+ ".  " + Data.Seoul_stage [level].getStageName ();


		SelectPanel.SetActive (false);
		AudioSource.PlayClipAtPoint(SE_click,transform.position);
		Spanel.SetActive (true);

	}
	public void panelTouch ()
	{
		init ();
		isStarted = true;
		Spanel.SetActive (false);
	}
	// Update is called once per frame
	void Update ()
	{
		//게임종료
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(!ExitPanel.activeSelf)
			{
				ExitPanel.SetActive(true);
				Time.timeScale = 0;			
			}
			else
			{
				ExitPanel.SetActive(false);
				Time.timeScale = 1;
			}
		}


		if (level < 6) {
			if (Minion >= totalMinion) {
				if (Data.isClear < level + 1) {
					PlayerPrefs.SetInt ("isClear", level + 1);
					PlayerPrefs.Save ();
					Data.isClear = level + 1;
				}
				AudioSource.PlayClipAtPoint(SE_clear,transform.position);
				//GetComponent<Admobs> ().SendMessage ("ShowInterAd");
				pauseBtn.SetActive (false);
				clearPanel.SetActive (true);
				Minion = 0;
			}
		} 
		if (isStarted)
			CameraCheckOut ();
	}

	IEnumerator StartInfnite ()
	{
		timeText.gameObject.SetActive (true);
		int time = 30;
		while (time >0) {
			timeText.text = time.ToString ();
			yield return new WaitForSeconds (1);
			time--;
		}
		//GetComponent<Admobs> ().SendMessage ("ShowInterAd");
		pauseBtn.SetActive (false);
		clearPanel.SetActive (true);
		Transform tmp = clearPanel.transform.FindChild ("gotoNext");
		tmp.GetComponent<Button> ().interactable = false;
		foreach (GameObject obj in G_mini) {
			if(obj)
			obj.GetComponent<Minions>().isGoal = true;
		}
	}
	Transform[] ChooseSet (int numRequired)
	{
		Transform[] result = new Transform[numRequired];
		bool[] isIn = new bool[8];
		int rnd;
		for (int i = 0; i<numRequired; i++) {
			rnd = Random.Range (0,8);
			if(!isIn[rnd])
			{
				result[i] = spawnPoint[rnd];
				isIn[rnd] = true;
			}
			else
				i--;
		}
		return result;
	}
	IEnumerator IFN_getInfo()
	{
		int curNum = 0;
		int rndNum;
		int[] rndKind;
		Transform[] choosedSpawn;
		for (int j = 0; j<6; j++) {
			rndNum = Random.Range (1, 3);
			rndKind = new int[rndNum];
			choosedSpawn = ChooseSet (rndNum);
			for (int i = 0; i<rndNum; i++) {
				if (choosedSpawn [i].position.y > 0)
					rndKind [i] = Random.Range (6, 9);
				else
					rndKind [i] = Random.Range (0, 3);
				IFN_create (curNum+i, rndKind [i], choosedSpawn [i]);
			}
			curNum += rndNum;
			yield return new WaitForSeconds(5.0f);
		}
	}
	public void IFN_create(int curnum,int kind, Transform trs)
	{
		G_mini [curnum] = (GameObject)Instantiate (kindMini [kind], transform.position, transform.rotation);
		OutImage [curnum] = (GameObject)Instantiate (kindImage [kind], new Vector2(1000,1000), transform.rotation);

		G_mini [curnum].transform.parent = pMini.transform;
		OutImage [curnum].transform.SetParent (canvas.transform);
			
		G_mini [curnum].transform.localPosition = new Vector2 (trs.localPosition.x,trs.localPosition.y);

	}
	public void init ()
	{
		//튜토리얼 세팅 
		if (level == 2 && Data.numLimit == 2) {
			Data.numLimit++;
			PlayerPrefs.SetInt ("numLight", Data.numLimit);
			PlayerPrefs.Save ();
		} //else if (level == 3)
		//	btn [0].SetActive (true);
		else
			btn [0].SetActive (false);
		//배경 설정
		StopCoroutine ("StartInfnite");
		StopCoroutine ("IFN_getInfo");
		float mapLength = Data.Seoul_stage [level].getTotalLight () * (-2) + 29;
		float Gpos = (Data.Seoul_stage [level].getTotalLight ()) * 2 - 0.5f;
		back.SendMessage ("setPos", mapLength);
		goal.transform.position = new Vector2 (0.1f, Gpos);
		clearPanel.SetActive (false);
		Gover.SetActive (false);
		pauseBtn.SetActive (true);
		notPause ();

		//레벨 셋팅
		isStarted = true;Debug.Log (Data.numLimit);
		NumOfLight = Data.numLimit;
		text.text = NumOfLight.ToString ();
		totalMinion = Data.Seoul_stage [level].getNumOfMini ();

		//이전 캐릭터 삭제
		foreach (GameObject min in G_mini) {
			if (min)
				Destroy (min.gameObject);
		}
		foreach (GameObject min in OutImage) {
			if (min)
				Destroy (min.gameObject);
		}

		//캐릭터 생성
		if (level < 6) {
			G_mini = new GameObject[totalMinion];
			OutImage = new GameObject[totalMinion];
			for (int i = 0; i<totalMinion; i++) {
				G_mini [i] = (GameObject)Instantiate (kindMini [Data.Seoul_stage [level].pt [i].getKind ()], transform.position, transform.rotation);
				OutImage [i] = (GameObject)Instantiate (kindImage [Data.Seoul_stage [level].pt [i].getKind ()], transform.position, transform.rotation);

				G_mini [i].transform.parent = pMini.transform;
				OutImage [i].transform.SetParent (canvas.transform);

				G_mini [i].transform.localPosition = new Vector2 (Data.Seoul_stage [level].pt [i].getX (), Data.Seoul_stage [level].pt [i].getY ());
			}
		} else {
			totalMinion = 15;
			G_mini = new GameObject[totalMinion];
			OutImage = new GameObject[totalMinion];
			StartCoroutine ("IFN_getInfo");
			StartCoroutine ("StartInfnite");
		}
		//불끔
		AllOffLight ();
	}

	void SetLightNum (int i)
	{
		NumOfLight += i;
		text.text = NumOfLight.ToString ();
	}

	void reachGoal ()
	{
		Minion++;
	}

	void GameOver ()
	{	
		AudioSource.PlayClipAtPoint(SE_gameover,transform.position);
		//GetComponent<Admobs> ().SendMessage ("ShowInterAd");
		isStarted = false;
		Time.timeScale = 0;
		pauseBtn.SetActive (false);
		Gover.SetActive (true);
	}

	public void GotoMenu ()
	{


		//Application.LoadLevel (1);
		SelectPanel.SetActive (true);
		foreach (GameObject min in G_mini) {
			if (min)
				Destroy (min.gameObject);
		}
		foreach (GameObject min in OutImage) {
			if (min)
				Destroy (min.gameObject);
		}
		Time.timeScale = 1;
		AudioSource.PlayClipAtPoint(SE_click,transform.position);
		Time.timeScale = 0;
		spNum.text = "x";
		startText.text = "Stage";
		pauseImg.SetActive(false);
		Gover.SetActive (false);
		pauseBtn.SetActive (false);
		setMenu ();
		AllOffLight ();


	}

	public void GotoNext ()
	{
		++level;
		init ();
	}

	void CameraCheckOut ()
	{
		for (int i =0; i< totalMinion; i++) {
			if (G_mini [i] && !G_mini [i].GetComponent<Minions> ().isGoal) {
				Vector3 pos = Camera.main.WorldToViewportPoint (G_mini [i].transform.position); 
				if ((pos.x >= 0.0f && pos.x <= 1.0f) && (pos.y >= 0.0f && pos.y <= 1.0f)) {
					OutImage [i].SetActive (false);//안에있다
				} else {//밖에있다.
					OutImage [i].SetActive (true);
					Vector3 OBJ_Pos = Camera.main.ViewportToScreenPoint (pos);
					if (pos.y < 0.0f) {
						OutImage [i].transform.position = new Vector2 (OBJ_Pos.x, 80f);
					} else if (pos.y > 1.0f) {
						OutImage [i].transform.position = new Vector2 (OBJ_Pos.x, 1140.1f);
					}
				}
			} else if(G_mini[i])
				OutImage [i].SetActive (false);
		}
	}

	//item
	void AllOnLight ()
	{
		GameObject[] SL;
		SL = GameObject.FindGameObjectsWithTag ("Slight");
		for (int i = 0; i<SL.Length; i++) {
			SL [i].SendMessage ("Onlight");
		}
		//Debug.Log(allight);
	}

	void AllOffLight ()
	{
		GameObject[] SL;
		SL = GameObject.FindGameObjectsWithTag ("light");
		for (int i = 0; i<SL.Length; i++) {
			SL [i].SendMessage ("Offlight");
		}
		//Debug.Log(allight);
	}

	public void pause ()
	{
		AudioSource.PlayClipAtPoint(SE_pause,transform.position);
		float timeS = Time.timeScale;
		if (timeS != 0) {
			Time.timeScale = 0;
			pauseImg.SetActive (true);
		}
	}

	public void notPause ()
	{
		Time.timeScale = 1;
		pauseImg.SetActive (false);
		ExitPanel.SetActive (false);
		AudioSource.PlayClipAtPoint(SE_click,canvas.transform.position);
	}
	public void showCredit()
	{

		if (!isCreditOn) {
			creditPanel.SetActive (true);
			isCreditOn = true;
		} else {
			creditPanel.SetActive (false);
			isCreditOn = false;
		}
	}
	public void ExitGame()
	{
		PlayerPrefs.SetInt ("numLight", Data.numLimit);
		PlayerPrefs.Save ();
		Application.Quit ();
	}
	public void playSE(AudioClip aud)
	{
		AudioSource.PlayClipAtPoint(aud,transform.position);
	}

}
