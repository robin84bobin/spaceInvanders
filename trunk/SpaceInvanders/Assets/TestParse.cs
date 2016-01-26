using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;
using UnityEngine;

namespace Assets
{
    public class TestParse : MonoBehaviour {

        // Use this for initialization
        void Start () {
            StartCoroutine (Read ());


        }

        IEnumerator Read ()
        {
            yield return new WaitForSeconds (5f);
            ReadLevel ();
        }

        void FindCallBack (Task<IEnumerable<ParseObject>> task_)
        {
            foreach (ParseObject item in task_.Result) {
                Debug.Log(item.Get<string>("foo"));
            }
        }

        void ReadLevel ()
        {
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Level").WhereEqualTo("ID",1);
            query.FindAsync()//GetAsync("wsBDpV4hq2")
                .ContinueWith( t_ => {
                                        ParseObject level = (new List<ParseObject>(t_.Result))[0];
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
}
