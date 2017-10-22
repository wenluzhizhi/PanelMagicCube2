using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.Events;


namespace EGUI
{

	public class myToggle :UIBehaviour,IPointerClickHandler
	{

		[Serializable]
		public class ToggleEvent:UnityEvent<bool>{}

		[SerializeField]private bool isCheck;
		public bool IsCheck{
			get{
				return isCheck;
			}
			set{
				isCheck = value;
				setShowImagState ();
			}
		}


		public GameObject CheckShowGameObject;
		public GameObject CheckHideGameObject;

		public ToggleEvent OnValueChanged=new ToggleEvent();

		#region monoEvent
		void Start ()
		{
	       
		}


		void Update ()
		{
	
		}


		public void OnPointerClick(PointerEventData data){
			this.IsCheck = !IsCheck;
			///Debug.Log ("--------"+IsCheck);
			if (OnValueChanged != null)
			{
				OnValueChanged.Invoke (this.IsCheck);
			}
			setShowImagState ();
		}

		private void setShowImagState(){

			if (CheckShowGameObject)
				CheckShowGameObject.SetActive (isCheck);
			if (CheckHideGameObject)
				CheckHideGameObject.SetActive (!isCheck);
		}

		#endregion
	}
}
