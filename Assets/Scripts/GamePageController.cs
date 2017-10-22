using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using EGUI;
public class GamePageController : MonoBehaviour {


	public Image showImage;
	public List<DragItem> listItem = new List<DragItem> ();
	public List<Texture2D> listTextures = new List<Texture2D> ();

	public  GameObject sliceContent;
	public DragItem DragItemPrefab;

	[SerializeField] private myToggle tog;
	[SerializeField] private Text tips;

	private GridLayoutGroup gridLayout=null;
	void OnEnable(){
		tips.text = "点击开始...";
		DragItemPrefab.gameObject.SetActive (false);
		sliceContent.gameObject.SetActive (false);
		gridLayout = sliceContent.GetComponent<GridLayoutGroup> ();
		showImage.sprite = MainUIController.Instance.selectPage.getSp ();
		showImage.gameObject.SetActive (true);
		tog.IsCheck = false;
	}
	public void OnClickReturn(){
		switchCount = 0;
		tips.text = "点击开始...";
		MainUIController.Instance.OpenSelectPagePanel ();
		//returnAllChild (sliceContent.transform);
		listTextures.Clear ();

	}


	public void OnClickSlicePicTog(bool isTrue)
	{
		
		if (listTextures.Count == 0&&isTrue) {
			SlicePic ();
		} else {
			sliceContent.gameObject.SetActive (isTrue);
			showImage.gameObject.SetActive (!isTrue);
		}
	}

	private void SlicePic(){
		sliceContent.gameObject.SetActive (true);
		showImage.gameObject.SetActive (false);
		Texture2D slicePic_t2d = showImage.sprite.texture;
		if (slicePic_t2d == null) {
			return;
		}
		if (listTextures.Count > 0) {
			foreach(Texture2D t1 in listTextures){
				DestroyImmediate (t1);
			}
		}

		listTextures = new List<Texture2D> ();
		listTextures.Clear ();
		returnAllChild (sliceContent.transform);

		listItem = new List<DragItem> ();
		listItem.Clear ();

		int difficultDegree = MainUIController.Instance.DifficultDegree;
		float _w = slicePic_t2d.width;
		float _h = slicePic_t2d.height;
		int _offsetW = (int)(_w / difficultDegree);
		int _offsetH = (int)(_h / difficultDegree);
		gridLayout.cellSize = showImage.rectTransform.rect.size / difficultDegree;
		gridLayout.constraintCount = difficultDegree;
		for (int i = 0; i < difficultDegree; i++) //高度 行
		{
			for (int j = 0; j < difficultDegree; j++) //宽度  列
			{
				Texture2D _temp = new Texture2D (_offsetW, _offsetH,TextureFormat.RGB24,false);
				DragItem _item = getItemDrag ().GetComponent<DragItem>();
				_item.transform.SetParent (sliceContent.transform,false);
				int n = (i * difficultDegree + j);
				_item.name =  n+ "_itme";
				_temp.SetPixels (slicePic_t2d.GetPixels(j*_offsetW,i*_offsetH,_offsetW,_offsetH));
				_temp.Apply ();

			     listTextures.Add (_temp);
				listItem.Add (_item);

				  
			}
		}
		fillImageForItems ();
		tips.text = "拖拽移动...";
	}

	private void fillImageForItems(){

		int[] ar = getRandomArray (listItem.Count);
		for (int i = 0; i < listItem.Count; i++) {
			listItem[i].setImage(i,ar[i],listTextures[ar[i]]);
		}
	}

	public void CaculateScore(){
		
	}

	public int switchCount=0;
	public void RefreshTips(){
		switchCount++;
		calculateScore ();
	}
	public int score=0;

	void OnGUI1(){
		if (GUILayout.Button ("11111111111111111111111111111111111111111111111111111111111111111button")) {
			//calculateScore ();
			fillImageForItems();
		}
	}
	private void calculateScore(){
		SoundController.Instance.PlayDidi ();
		score = 0;
		for (int i = 0; i < listItem.Count; i++) 
		{
			DragItem _t=listItem[i];
			DragItemPic _p = _t.gameObject.GetComponentInChildren<DragItemPic> ();
			if (_t.currentPos ==_p.rightPos&&_t.gameObject.activeInHierarchy)
			{
				score++;
			}
		}
		//Debug.Log ("----"+score);
		tips.text = "移动次数："+switchCount+"\r\n正确位置"+score+"/"+listItem.Count;
		if (score == listItem.Count) {
			Debug.Log ("完成拼图"+Time.time);
			MainUIController.Instance.ShowTipManager ("恭喜，拼图成功!");

		}
	}

	#region  资源池

	public List<GameObject> itemPool=new List<GameObject>();
	private GameObject getItemDrag(){
		GameObject go = null;
		if (itemPool.Count > 0) {
			go = itemPool [0];
			itemPool.RemoveAt (0);
		} else {
			go = GameObject.Instantiate (DragItemPrefab.gameObject,DragItemPrefab.transform.position,Quaternion.identity) as GameObject;
		}
		go.SetActive (true);
		return go;
	}


	private void returnItem(GameObject go){
		go.SetActive (false);
		itemPool.Add (go);


	}

	private void returnAllChild(Transform content){
		foreach (Transform t in content) {
			if (t.gameObject.activeInHierarchy) {
				
				returnItem (t.gameObject);
			}
		}
	}


	private int[] getRandomArray(int length){
		int [] ar=new int[length];
		for (int i = 0; i < length; i++)
			ar [i] = i;
		for (int i = length - 1; i >= 0; i--) {
			int k = Random.Range (0,i+1);
			int temp = ar [i];
			ar [i] = ar [k];
			ar [k] = temp;
		}
		return ar;
	}

	#endregion
}


