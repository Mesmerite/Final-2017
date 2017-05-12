using UnityEngine;
using System;
using System.Collections.Generic; //Allow for lists
using Random = UnityEngine.Random; //Random now uses random number generator

namespace Completed {

public class LevelManager : MonoBehaviour {

	public class Count {
		public int maximum;
		public int minimum;

		public Count (int min, int max) {
			maximum = min;
			maximum = max;
		}
	}

	//Size of game screen
	public int columns = 15;
	public int rows = 15;
	//How many walls
	public Count wallCount = new Count (10, 15); //Min and max number of walls
	//How many energy items
	public Count energCount = new Count (1, 5);
	//Navigation
	public GameObject entrance;
	public GameObject exit;
	//Enemies
	public GameObject[] basicBot;
	public GameObject[] intelBot;
	public GameObject[] basicFly;
	public GameObject[] intelFly;
	//Ground tiles
	public GameObject[] platformTile;
	public GameObject[] wallTile;
	public GameObject[] spikeTile;
	public GameObject[] elecTile;

	//Hold all game objects in levelHolder
	private Transform levelHolder;
	//Track possible positions on level in private list
	private List <Vector3> gridPositions = new List<Vector3>();

	//Initialise
	void InitialiseList() {
		gridPositions.Clear ();
		for (int x = 1; x < columns - 1; x++) {
			for (int y = 1; y < rows - 1; y++) {
				gridPositions.Add (new Vector3 (x, y, 0f)); //List of possible positions to place items etc.
			}
		}
			
	}
	/*
	//Wall tiles
	void levelSetup() {
		for (int x = -1; x < columns + 1; x++) {
			GameObject toInstantiate = wallTile (0, floorTiles.Length);
		}
	}
	*/
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
}
