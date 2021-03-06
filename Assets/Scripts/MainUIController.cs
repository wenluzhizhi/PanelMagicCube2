﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour {

	public SelectPageController selectPage;
	public GamePageController gamePage;
	public GameObject TipManager;
	public Text tipTxt;
	public GameObject QuitPanel;
	public int DifficultDegree = 0;
	public int selectPicNum=0;
	//单例

	private static MainUIController _instance;
	public static MainUIController Instance{
		get{
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(MainUIController)) as MainUIController;
			}
			return _instance;
		}
	}


	void Start(){
		OpenSelectPagePanel ();
		CancleQuit ();
	}

	public void OpenGamePanel(){
		selectPage.gameObject.SetActive (false);
		gamePage.gameObject.SetActive (true);
	}

	public void OpenSelectPagePanel(){
		selectPage.gameObject.SetActive (true);
		gamePage.gameObject.SetActive (false);
	}


	public void ShowTipManager(string str){
		TipManager.gameObject.SetActive (true);
		tipTxt.text = str;
	}

	public void OnClickCloseTipManager(){
		TipManager.gameObject.SetActive (false);
	}
	public float _timer=0.0f;
	void Update(){


		if (Input.GetKey (KeyCode.Escape)) {
			QuitPanel.gameObject.SetActive (true);
		}
	}

	public void ConfirmQuit(){
		Application.Quit ();
	}

	public void CancleQuit(){
		QuitPanel.gameObject.SetActive (false);
	}
}
