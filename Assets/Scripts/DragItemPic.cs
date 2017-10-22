using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DragItemPic : MonoBehaviour 
{

	public DragEnableComponent dragC;
	public RawImage showImage;
	public int rightPos=-1;
	void Awake()
	{
		dragC.StartDragAc += StartDrag;
		dragC.EndDragAc += EndDrag;
		dragC.EndSwitchAc += EndSwitchDrag;
	}


	public void StartDrag(){
		
		showImage.color = new Color (1,1,1,0.4f);

	}

	public void EndDrag(){
		showImage.color = new Color (1,1,1,1);
		//Debug.Log (this.gameObject.name+" -4444444--"+Time.time);
		resetCurPos ();
	}

	public void EndSwitchDrag(){
		//Debug.Log (this.gameObject.name+" -55555--"+Time.time);
		resetCurPos ();
		MainUIController.Instance.gamePage.RefreshTips ();
	}


	private void resetCurPos()
	{
		int currentPos = this.transform.parent.gameObject.GetComponent<DragItem> ().currentPos;
		if (currentPos == rightPos)
		{
			Debug.Log ("移动陈宫。。。"+Time.time);
		}
	}
}
