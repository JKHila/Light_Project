using UnityEngine;
using System.Collections;

public class Share : MonoBehaviour {

	private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
	private const string TWEET_LANGUAGE = "kr"; 
	
	public void ShareToTwitter (string textToDisplay)
	{
		Application.OpenURL(TWITTER_ADDRESS +
		                    "?text=" + WWW.EscapeURL(textToDisplay) +
		                    "&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE));
	}

	private const string FACEBOOK_APP_ID = "684921834972424";
	private const string FACEBOOK_URL = "http://facebook.com/dialog/feed";
	
	public void ShareToFacebook ()//(string linkParameter, string nameParameter, string captionParameter, string descriptionParameter, string pictureParameter, string redirectParameter)
	{
		string linkParameter = "https://drive.google.com/open?id=0BxJzW6UCVzy5fjRYZTQ3LWE3LWJFY2VrMVhrdS1ZaTBIa3h6VWdGeXljSUR0dHpiU25MX2c";
		string nameParameter = "불을 밝혀라!";
		string captionParameter="지금바로 플레이스토어로 꼬우!";//밑에 
		string descriptionParameter="우리동네 불을 밝혀주세요!";//위에 
		string pictureParameter="http://postfiles6.naver.net/20150904_85/ckheee93_1441334916108zpvyg_PNG/title.png?type=w3";
		string redirectParameter = "http://facebook.com";
		Application.OpenURL (FACEBOOK_URL + "?app_id=" + FACEBOOK_APP_ID +
		                     "&link=" + WWW.EscapeURL(linkParameter) +
		                     "&name=" + WWW.EscapeURL(nameParameter) +
		                     "&caption=" + WWW.EscapeURL(captionParameter) + 
		                     "&description=" + WWW.EscapeURL(descriptionParameter) + 
		                     "&picture=" + WWW.EscapeURL(pictureParameter) + 
		                     "&redirect_uri=" + WWW.EscapeURL(redirectParameter));
	}
}
