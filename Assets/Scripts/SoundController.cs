using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    //单例

	private static SoundController _instance;
	public static SoundController Instance{
		get{
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(SoundController)) as SoundController;
			}
			return _instance;
		}
	}


	[SerializeField] private AudioSource didi;

	public void PlayDidi(){
		didi.Play ();
	}
}
