using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UB.SimpleCS.Models;

public class Body : EmptyDto {

	public Body(string name){
		this.name = name;
	}

	private string name;
}
