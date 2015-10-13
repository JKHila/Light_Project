using UnityEngine;
using System.Collections;
enum kind{BOY,GIRL,CHILD,ELDER,BIKE,MOTO,BOY_F,GIRL_F,CHILD_F,ELDER_F,BIKE_F,MOTO_F};
public class Pos
{
	private float posX,posY;
	private int kindOf;
	public Pos(float m_x, float m_y, int k)
	{
		posX = m_x;
		posY = m_y;
		kindOf = k;
	}
	public int getKind(){return kindOf;}
	public float getX(){return posX;}
	public float getY(){return posY;}
}
public class Stage
{
	private int NumOfMini;
	public Pos[] pt;
	//private GameObject[] Mini;
	//private int NumOfMapLight;
	private int NumOfTotalLight;
	private string StageName;
	public Stage(string n,int mini,Pos[] m_pt,int light)
	{
		StageName = n;
		NumOfMini = mini;
		pt = m_pt;
		NumOfTotalLight = light;
	}
	public Stage (string n, int light)
	{
		StageName = n;
		NumOfTotalLight = light;
	}
	public void setMini(Pos[] m_pt,int mini)
	{
		NumOfMini = mini;
		pt = m_pt;
	}
	public string getStageName()
	{
		return StageName;
	}
	public int getNumOfMini()
	{
		return NumOfMini;
	}
	public float getTotalLight()
	{
		return NumOfTotalLight;
	}
}
public static class Data{

	public static int numLimit;
	public static int coin;
	public static int stageNum;
	public static int isClear=0;

	public static Stage[] Seoul_stage = new Stage[7]{
		new Stage("동작구",1,new Pos[]{
			new Pos(-0.5f,-3,(int)kind.BOY)},
		6),
		new Stage("관악구",3,new Pos[]{
			new Pos(-2,-3,(int)kind.BOY),
			new Pos(-0.5f,-3,(int)kind.GIRL),
			new Pos(1,-3,(int)kind.CHILD)},
		6),
		new Stage("강남구",4,new Pos[]{
			new Pos(-2.5f,-3,(int)kind.BOY),
			new Pos(-1.3f,-3,(int)kind.GIRL),
			new Pos(0.21f,-3,(int)kind.CHILD),
			new Pos(1.3f,-3,(int)kind.CHILD)},
		8),

		new Stage("노원구",3,new Pos[]{
			new Pos(-2,16,(int)kind.GIRL_F),
			new Pos(-1.37f,16,(int)kind.BOY_F),
			new Pos(0.96f,-3,(int)kind.CHILD)},
		8),
		new Stage("송파구",4,new Pos[]{
			new Pos(-1.37f,16,(int)kind.BOY_F),
			new Pos(-2.0f,16,(int)kind.ELDER_F),
			new Pos(1.08f,-3,(int)kind.GIRL),
			new Pos(0.36f,-3,(int)kind.BOY)},
		8),
		new Stage("강서구",6,new Pos[]{
			new Pos(1.3f,20,(int)kind.CHILD_F),
			new Pos(0.62f,20,(int)kind.CHILD_F),
			new Pos(-2.0f,20,(int)kind.ELDER_F),
			new Pos(-1.3f,-3,(int)kind.GIRL),
			new Pos(-2.38f,-3,(int)kind.BOY),
			new Pos(-1.89f,-3,(int)kind.GIRL)},
		10),
		new Stage("무한모드",6)
	};
}
