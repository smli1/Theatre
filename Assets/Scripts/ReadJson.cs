using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class ReadJson : MonoBehaviour {

	private string jsonString;
	public static RootObject data;

	void Awake () {
		jsonString = File.ReadAllText(Application.dataPath + "/Resources/Json/ScriptSetting.json");
		//Debug.Log(jsonString);
		data = JsonConvert.DeserializeObject<RootObject>(jsonString);
		//Debug.Log(data.script[0].actors[0].name);
		Debug.Log(data.script[0]);
	}   
}

public class ActorStepOffset
{
    public double x { get; set; }
    public double y { get; set; }
    public double z { get; set; }
}

public class Actor
{
    public string name { get; set; }
    public int actor_num { get; set; }
    public List<string> actor_clip_name { get; set; }
    public int actor_total_steps { get; set; }
    public List<int> actor_steps_action { get; set; }
    public List<double> actor_steps_delay { get; set; }
    public string actor_markers_name { get; set; }
    public List<string> actor_markers_num { get; set; }
    public bool isFixedPosition { get; set; }
    public List<ActorStepOffset> actor_step_offset { get; set; }
}

public class Textscript
{
    public string text { get; set; }
    public int showOnActionNum { get; set; }
}

public class Script
{
    public string name { get; set; }
    public List<Actor> actors { get; set; }
    public List<Textscript> textscript { get; set; }
}

public class RootObject
{
    public List<Script> script { get; set; }
}