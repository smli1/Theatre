using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailData {

	public string name;
	public string content;
	private string font;
	public int fontSize;
	private int imageNum;
	private static Sprite[] mailArray;
	private static Sprite[] mailOpenedArray;

	public MailData(string name = ""){
		
		this.name = name;
		setVaribles (name);

		if(mailArray == null){
			mailArray = Resources.LoadAll<Sprite>("Mails");
		}
		if(mailOpenedArray == null){
			mailOpenedArray = Resources.LoadAll<Sprite>("MailsOpened");
		}
	}

	public void setVaribles(string name){
		switch(name.Split('_')[0]){
		case "cat":
			font = "Spring";
			fontSize = 20;
			content = "I want a yarn! meow ~";
			imageNum = 0;
			break;
		case "Leave":
			font = "Leafs";
			fontSize = 35;
			content = "RUSTLINGRUSTLINGRUSTLINGRUSTLING";
			imageNum = 0;
			break;
		case "":
			font = "Arial";
			fontSize = 20;
			content = "";
			imageNum = 0;
			break;
		default: 
			font = "Arial";
			fontSize = 20;
			content = "";
			imageNum = 0;
			break;
		}
	}

	public Sprite getSprite(){
		return mailArray [imageNum];
	}

	public Sprite getSpriteOpened(){
		return mailArray [imageNum];
	}

	public Font getFont(){
		if(font == "Arial"){
			return Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
		}
		return Resources.Load<Font> ("Fonts/"+font);
	}

}
