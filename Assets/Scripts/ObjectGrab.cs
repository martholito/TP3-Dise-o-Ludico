using UnityEngine;
using System.Collections;

public class objectGrab : MonoBehaviour {
	public Transform Object;	
	public string interactText = "Press F To Interact";
	public GUIStyle InteractTextStyle;
		
	private bool init = false;
	private bool hasEntered = false;
	private Rect interactTextRect;
		
	void Start () {
		//Check if Door Game Object is properly assigned
		if(Object == null){
			Debug.LogError (this + " :: Door Object Not Defined!");
		}
	}
		
	void Update () {
		if(!init)
			return;	
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			hasEntered = true;
		}
	}
	
	void OnTriggerExit(Collider other){
		hasEntered = false;
	}
	
	void OnGUI(){
		if(!init || !hasEntered)
			return;
		
		GUI.Label(interactTextRect, interactText, InteractTextStyle);
	}
}