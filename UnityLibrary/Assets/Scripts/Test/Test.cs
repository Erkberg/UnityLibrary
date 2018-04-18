using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ErksUnityLibrary;

public class Test : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < 10; i++) list.Add(i);
		list.ShuffleList();
        foreach (int i in list) Debug.Log(i);
	}

}
