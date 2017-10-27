using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SelectPageController : MonoBehaviour {

	public ItemImage itemPrefab;
	public List<Sprite> spriteItems=new List<Sprite>();
	public RectTransform content;
	[SerializeField] private GameObject cameraSelectPanel;

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
		int k = spriteItems.Count;
		returnAllResource (content.transform);
		cameraSelectPanel.gameObject.SetActive (false);
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
		if (k < spriteItems.Count) {
			return spriteItems[k];
		}
		return null;
	}

	public void OnClickCamera(){
		Debug.Log ("open camera:"+Time.time);
		cameraSelectPanel.gameObject.SetActive (true);
	}

	public void selectCamera(){
		OnClickCameraPanel ();
		cameraSelectPanel.gameObject.SetActive (true);
		AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject> ("currentActivity");
		jo.Call ("takeCamera");
	}

	public void SelectAlbum(){
		OnClickCameraPanel ();
		cameraSelectPanel.gameObject.SetActive (true);
		AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject> ("currentActivity");
		jo.Call ("takePhoto");
	}

	public void OnClickCameraPanel(){
		cameraSelectPanel.gameObject.SetActive (false);
	}


	public void getPath(string str){  //相册
		Debug.Log("相机返回的地址："+str+"   "+Time.time);	
		StartCoroutine (LoadImage(str));
	}

	public void GetTakeImagePath(string str){//相机
		Debug.Log("相机返回的地址："+str+"   "+Time.time);
		StartCoroutine (LoadImage(str));

	}



	private IEnumerator LoadImage(string imagePath)  
	{  
		WWW www = new WWW ("file://"+imagePath);  
		yield return www;  
		if (www.error == null)
		{  
			Texture2D t = www.texture;
			Sprite sprite = Sprite.Create(t, new Rect(0, 0,t.width,t.height), Vector2.zero);
			spriteItems.Add (sprite);
			initLayout ();
		}
		else
		{  
			Debug.Log("LoadImage>>>www.error:"+www.error);  

		}  
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
