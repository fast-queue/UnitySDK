using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UB.SimpleCS;
using UnityEngine.UI;

public class Test : MonoBehaviour {
	public Text resText;

	RestApi api = RestApi.Instance;
	string url = "http://tcc-andre.ddns.net/queue";
	string key = "minhavidaeandarporestepais";

	void Start(){
	}

	public void getAllQueue(){
		var d = api.Send<Body>(RequestType.Get, url, key, null);

		resText.text = d;
		Debug.Log (d);
	}

	public void addQueue(string b){
		var d = api.Send<Body>(RequestType.Post, url, key, new Body(b));

		resText.text = d;
		Debug.Log (d);
	}

}
