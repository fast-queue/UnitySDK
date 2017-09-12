using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FQ;

public class Test : MonoBehaviour {
	public Text resText;
	public Text textName;
	public Text id;

	private Dictionary<string, MyQueueClass> maps;

	const string url = "http://tcc-andre.ddns.net/queue";
	const string key = "minhavidaeandarporestepais";
	RestApi api;

	void Start(){
		api = new RestApi(url, key);
		maps = new Dictionary<string, MyQueueClass> ();
	}

	public void getAllQueue(){
		MyQueueClass[] x = api.getAllQueue<MyQueueClass>();

		resText.text = x.Length + " Arrays";
		
	}

	public void addQueue(string b){
		MyQueueClass body = new MyQueueClass ("andre", 32);
		var resp = api.addQueue<MyQueueClass> (body);
		maps.Add (resp._id, resp);

		// set text on editor
		id.text = resp._id;
		textName.text = resp.name;
	}

}
