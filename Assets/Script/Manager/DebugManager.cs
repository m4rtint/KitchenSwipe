using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour {

	public static DebugManager instance = null;

	[SerializeField]
	GameObject m_Center;

	public bool C_Direction;

	void Awake() {
		instance = this;
	}

	#region Sides
    public void SetCenter(Color color) {
        m_Center.GetComponent<Image>().color = color;
	}
  
	#endregion

}
