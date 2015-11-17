using UnityEngine;
using System.Collections;
using Parse;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TestParse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Read ());


	}

	IEnumerator Read ()
	{
		yield return new WaitForSeconds (5f);
		readLevel ();
	}

	void FindCallBack (Task<IEnumerable<ParseObject>> task)
	{
		foreach (ParseObject item in task.Result) {
			Debug.Log(item.Get<string>("foo"));
		}
	}

	void readLevel ()
	{
		ParseQuery<ParseObject> query = ParseObject.GetQuery("Level").WhereEqualTo("ID",1);
		query.FindAsync()//GetAsync("wsBDpV4hq2")
			.ContinueWith( t => {
				ParseObject level = (new List<ParseObject>(t.Result))[0];
			//ParseObject level = t.Result;
			ParseObject hero = level.Get<ParseObject>("Hero");
			string mass = hero.ObjectId;
			Debug.Log("Mass " + mass.ToString());
		});
	}
	
	// Update is called once per frame
	void Update () 
	{

	}


}
