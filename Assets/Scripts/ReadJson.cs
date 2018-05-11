using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadJson : MonoBehaviour {

	private string jsonString;

	void Start () {
		jsonString = File.ReadAllText(Application.dataPath + "/Resources/Json/ScriptSetting.json");
		//Debug.Log(jsonString);
		//RootObject data = JsonConvert.DeserializeObject<RootObject>(jsonstring);
		//Debug.Log(data.script[0].actors[0].name);
	}
	

}

public class ActorStepOffset
{
    public int x { get; set; }
    public int y { get; set; }
    public int z { get; set; }
}

public class Actor
{
    public string name { get; set; }
    public int actor_num { get; set; }
    public List<string> actor_clip_name { get; set; }
    public int actor_total_steps { get; set; }
    public List<int> actor_steps_action { get; set; }
    public List<int> actor_steps_delay { get; set; }
    public string actor_markers_name { get; set; }
    public List<string> actor_markers_num { get; set; }
    public bool isFixedPosition { get; set; }
    public List<ActorStepOffset> actor_step_offset { get; set; }
}

public class Script
{
    public List<Actor> actors { get; set; }
}

public class RootObject
{
    public List<Script> script { get; set; }
}
