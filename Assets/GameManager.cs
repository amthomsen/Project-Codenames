using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public Color startColor, red, blue, grey;

	// Use this for initialization
	void Awake() {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShiftColor()
    {

    }
}
