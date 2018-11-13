using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingEffects : MonoBehaviour {

    readonly float pulsingScale = 1.2f;
    readonly string pulseName = "pulseAnimation";

    public void pulsingAnimation() {
        Hashtable ht = new Hashtable();
        ht.Add("name", pulseName);
        ht.Add("scale", Vector3.one * pulsingScale);
        ht.Add("time", 0.7f);
        ht.Add("easetype", "linear");
        ht.Add("looptype", "pingPong");
        iTween.ScaleTo(gameObject, ht);
    }

    public void resetScale() {
        iTween.StopByName(pulseName);
        transform.localScale = Vector3.one;
    }
}
