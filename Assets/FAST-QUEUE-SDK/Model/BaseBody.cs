﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FQ {
	
	public class BaseBody {

		public BaseBody(){
		}
		public string _id;

		protected string toJson(){
			return "{ _id: " + _id + " } "; 
		}
	}


}