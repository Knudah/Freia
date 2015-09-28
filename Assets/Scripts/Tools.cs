using UnityEngine;
using System.Collections;
using System;

public class Tools : MonoBehaviour {
	public enum Regulated : int{
		unknown,
		up,
		down,
	};

	public static Regulated RandomRegulated(){
		return (Regulated) (UnityEngine.Random.Range(0, Enum.GetNames(typeof(Regulated)).Length));
	}
}
