using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DragItem : MonoBehaviour {


	public int currentPos=-1;
	[SerializeField] private DragEnableComponent drag;
	public DragItemPic dpic;
	public void setImage(int itemPos,int ChildRightPos,Texture2D t)
	{
		dpic = this.gameObject.transform.GetComponentInChildren<DragItemPic> ();
		if (dpic)
		{
			
			dpic.showImage.texture = t as Texture;
			dpic.rightPos = ChildRightPos;
			this.currentPos=itemPos;
			dpic.showImage.gameObject.name =ChildRightPos+"_dragitem";
			drag.Init ();

		}
	}

}
