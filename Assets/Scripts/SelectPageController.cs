using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SelectPageController : MonoBehaviour {

	public ItemImage itemPrefab;
	public Sprite[] spriteItems;
	public RectTransform content;


	public GameObject SelectDegree;
	#region mono event

	void Start () {
		initLayout ();
	}

	void Update () {
	
	}

	#endregion

	#region internal function

	private void initLayout()
	{
		SelectDegree.SetActive (false);
		int k = spriteItems.Length;
		returnAllResource (content.transform);
		for (int i = 0; i < k; i++) 
		{
			GameObject go = getAGameObject ();
			go.transform.SetParent (content.transform,false);
			go.GetComponent<ItemImage> ().setImg (spriteItems[i],i,this);
		}
	}
	#endregion

	#region external function

	public void SelectPageNum(int num){
		Debug.Log ("slect num---"+num+"       "+Time.time);
		MainUIController.Instance.selectPicNum = num;
		SelectDegree.SetActive (true);
	}

	public void CancelSelectDegree(){
		SelectDegree.SetActive (false);
	}

	public void SelectDifficultDegree(GameObject go){
		MainUIController.Instance.DifficultDegree = go.transform.GetSiblingIndex ()+3;
		CancelSelectDegree ();
		MainUIController.Instance.OpenGamePanel ();
	}


	public Sprite getSp(){
		int k = MainUIController.Instance.selectPicNum;
		if (k < spriteItems.Length) {
			return spriteItems[k];
		}
		return null;
	}

	#endregion




	#region resource pool


	public List<GameObject> listResource = new List<GameObject> ();

	public void returnAllResource(Transform parent)
	{
		foreach (Transform t in parent)
		{
			t.gameObject.SetActive (false);
			listResource.Add (t.gameObject);

		}
	}


	public GameObject getAGameObject(){

		GameObject go = null;
		if (listResource.Count == 0) {
			go = GameObject.Instantiate (itemPrefab.gameObject,itemPrefab.transform.position,Quaternion.identity) as GameObject;
		} else {
			go=listResource[0];
			listResource.RemoveAt (0);

		}
		go.SetActive (true);
		return go;
	}


	#endregion
}
