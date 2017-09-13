﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine;

namespace FQ {
	
    public class RestApi
    {
		private string key;
		private string baseUrl;

		public RestApi(){}

		public RestApi(string url, string key)
		{
			this.baseUrl = url;
			this.key = key;
        }

		public T addQueue<T>(T obj) where T: FQ.BaseBody {
			var url = baseUrl + "/queue";
			return add(url, obj);
		}

		public T[] getAllQueue<T>() {
			var url = baseUrl + "/queue";
			return get<T>(url);
		}

		public T[] getAllPlayersOnQueue<T>(string _id) {
			var url = baseUrl + "/queue/" + _id + "/players";
			return get<T>(url);
		}

		public T addPlayerToQueue<T>(string _id, T obj) where T: FQ.BaseBody{
			var url = baseUrl + "/queue/" + _id + "/players";
			return add<T>(url, obj);
		}

		private T add<T>(string url, T obj) where T: FQ.BaseBody {
			T retObj = obj;
			var type = RequestType.Post;
			if(obj == null){
				throw new Exception ("No object recived on post!");
			}
			var sendObj = objectToJSON(obj);
			var request = this.Send (url, type, sendObj);
			T response = convertPostResponse<T> (request);
			retObj._id = response._id;
			return retObj;
		}

		private T[] get<T>(string url){
			var type = RequestType.Get;
			var request = this.Send (url, type, null);
			var x = "{ objects:" + request + "}";
			var response = convertPostResponse<getAllToWork<T>> (x);
			return response.objects;
		}

			
		private string Send(string url, RequestType apiRequestType, string body)
		{
			var request = HttpWebRequest.Create(new System.Uri(url));
			request.Headers ["API-KEY"] = this.key;
            request.Timeout = 21000; //milliseconds
			if (apiRequestType == RequestType.Get) {
				request.Method = "GET";
			} else if (apiRequestType == RequestType.Post) {
				request.Method = "POST";
			} else if (apiRequestType == RequestType.Delete) {
				request.Method = "DELETE";
			} else if (apiRequestType == RequestType.Put) {
				request.Method = "PUT";
			}


            if (body != null)
            {
                var dataToSend = Encoding.UTF8.GetBytes(body);
                request.ContentType = "application/json";
                request.ContentLength = dataToSend.Length;
                request.GetRequestStream().Write(dataToSend, 0, dataToSend.Length);
            }

            var response = request.GetResponse();

            //HttpResponseMessage response = client.PostAsync(url, content).Result;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string responseString = reader.ReadToEnd();
        
                return responseString;
            }
        }

		private T convertPostResponse<T>(string response){
			//just trying to convert response to my helper empty dto, so i can understand if any error happened:)
			var body = JsonConvert.DeserializeObject<T>(response);
			if (body == null )
			{
				throw new Exception("Erro conversão json - POST"); //throw error message
			}
			return body;
		}

		private string objectToJSON(object obj){
			var serialized = JsonConvert.SerializeObject(obj);
			return serialized;
		}
    }
	class getAllToWork<T>{
		
		public T []objects;
	}
}
