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

	const string url = "http://tcc-andre.ddns.net";
	const string key = "minhavidaeandarporestepais";

	RestApi api;
	string queue = "";

	void Start(){
		api = new RestApi(url, key);
		maps = new Dictionary<string, MyQueueClass> ();
	}

	public void getAllQueue(){
		MyQueueClass[] x = api.getAllQueue<MyQueueClass>();
		queue = x[0]._id;

		Debug.Log(queue);

		string output = "";

		for (int i = 0; i < x.Length; i++)
		{
			output+= "{ name: " + x[i].name + ", _id: " + x[i]._id + "}";
			if(i != x.Length){
				output += ",";
			}
		}

		resText.text = output;
	}

	public void addQueue(string name){
		MyQueueClass body = new MyQueueClass (name, 32);
		var resp = api.addQueue<MyQueueClass> (body);
		maps.Add (resp._id, resp);

		// set text on editor
		id.text = resp._id;
		textName.text = resp.name;
	}

	public void addPlayerToQueue(string name){
		MyPlayerClass player = new MyPlayerClass(name);
		api.addPlayerToQueue<MyPlayerClass>(queue, player);

	}

}
