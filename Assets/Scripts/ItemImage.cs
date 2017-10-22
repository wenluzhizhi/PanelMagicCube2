using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemImage : MonoBehaviour {
	public Image img;
	public int num;
	private SelectPageController parentP;
	public void setImg(Sprite sp,int num,SelectPageController p){
		img.sprite = sp;
		this.num = num;
		parentP = p;
	}



	public void OnClickButton(){
		//Debug.Log ("rt-----"+Time.time);
		parentP.SelectPageNum (this.num);
	}


}
