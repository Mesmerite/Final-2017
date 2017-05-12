using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/*
 * 
 * // LOAD ENEMY IN AFTER PLAYER


REFERENCE
0 == cube / Standard Tile
1 == elecCube / Electric Tile
2 == spikeCube / Spiky Tile
3 == player
4 == basicBot / Basic Robot Enemy
5 == intelBot / Intelligent Robot Enemy
6 == flybot / Flying Enemy
7 == orb / Small Health-Restoring Item
8 == sneakCube / Standard Tile with hidden damage mechanic
9 == Entry Door
10 == Exit Door
11 == Background1 - WARM RED/YELLOW
12 == Wall
13 == Background2 - BLUE
14 == Background3 - PURPLE
15 == Background4 - GREEN
16 == Background5 - RED
17 == Pillar
18 == Button
19 == Trap Button
20 == Data Chip
21 == Friend Bot
22 == Flying Platform A
23 == Flying Platform B
24 == orb / Medium Health-Restoring Item
25 == Standard cube 2
26 == Idle Flying Enemy
27 == Background6 - START
28 == Terminal

*/

public class ItemLoader : MonoBehaviour {

	//Levels
	protected int currentLevel;
	protected List<int> levelsComplete = new List<int>();
	protected List<int> unusedLevels = new List<int> (new int[]{ 1, 2, 3, 4, 5});

	//Life float
	public int life = 10;

	//Morality floats
	public float moral = 0;
	//Negative
	public float fear = 0;
	public float anger = 0;
	public float sadness = 0;
	public float disgust = 0;

	//Positive
	public float surprise = 0;
	public float happiness = 0;

	//Game object prefabs
	public GameObject background1;
	public GameObject background2;
	public GameObject background3;
	public GameObject background4;
	public GameObject background5;
	public GameObject background6;
	public GameObject background7;
	public GameObject cube;
	public GameObject elecCube;
	public GameObject spikeCube;
	public GameObject intelBot;
	public GameObject hostileBot;
	public GameObject flyBot;
	public GameObject entryDoor;
	public GameObject exitDoor;
	public GameObject player;
	public GameObject orb;
	public GameObject wall;
	public GameObject pillar;
	public GameObject button;
	public GameObject trapButton;
	public GameObject friendBot;
	public GameObject flyCube;
	public GameObject flyCubeB;
	public GameObject mediOrb;
	public GameObject idleFlyBot;
	public GameObject terminal;


	//Game over state
	private int playerIndex;
	public bool dead = false;
	private float deathTime;

	//Life view
	public GUIText lifeInt;

	//Enemies
	private int enemiesLoaded = 0;
	//Friend Robots
	private int robotsLoaded = 0;

	private int buttonIndex;

	public string path;
	protected List<GameObject> grid = new List<GameObject>();

	void Start () {
		loadLevel ();
	}

	void Update() {

		if(life <= 0 && dead == false) {
			dead = true;
			deathTime = Time.time; //Moment since scene has been loaded
			Debug.Log ("Dead");
			PlayerControl playerScript = grid [playerIndex].GetComponent<PlayerControl> ();
			playerScript.GameOver ();
		}
		if (dead == true && Time.time >= 1.2f + deathTime) {
			SceneManager.LoadScene (2);
		}


		//Restraints
		if (life >= 10) {
			life = 10;
		}
		//FEAR
		if (fear <= 0) {
			fear = 0;
		}
		if (fear >= 25) {
			fear = 25;
		}
		//ANGER
		if (anger <= 0) {
			anger = 0;
		}
		if (anger >= 20) {
			anger = 20;
		}
		//DISGUST
		if (disgust <= -20) {
			disgust = -20;
		}
		if (disgust >= 20) {
			disgust = 20;
		}
		//SADNESS


		//SURPRISE
		if (surprise <= 0) {
			surprise = 0;
		}
		if (surprise >= 20) {
			surprise = 20;
		}
		//HAPPINESS
		if (happiness <= 0) {
			happiness = 0;
		}
		if (happiness >= 20) {
			happiness = 20;
		}

	}
		
	public void Clear () {
		destroyLevel ();
		loadLevel ();
	}

	//Morality functions
	public void changeFear(float no) {
		fear += no;
	}
	public void changeAnger(float no1) {
		anger += no1;
	}
	public void changeDisgust(float no2) {
		disgust += no2;
	}
	public void changeSadness(float no3) {
		sadness += no3;
	}
	public void changeSurprise(float no4) {
		surprise += no4;
	}
	public void changeHappiness(float no5) {
		happiness += no5;
	}

	//Life function
	public void lifeMeter (int hp) {
		life += hp;
	}

	//Morality booleans
	public bool raiseSurprise = false;
	public bool raiseHappy1 = false;
	public bool raiseHappy2 = false;

	//Change scene
	public void LoadByIndex (int sceneIndex) {
		SceneManager.LoadScene (sceneIndex);

	}

	public void loadLevel(){

		//Change Values
		if (raiseSurprise == true) {
			changeSurprise (+3);
			raiseSurprise = false;
		}
		if (raiseHappy1 == true) {
			changeHappiness (+1);
			raiseHappy1 = false;
		}
		if (raiseHappy2 == true) {
			changeHappiness (+2);
			raiseHappy2 = false;
		}

		//LEVEL LOADING
		//UNCOMMENT
		/*
		int randomNo = Random.Range (0, unusedLevels.Count);
		currentLevel = unusedLevels[randomNo];

		string path = "Level" + currentLevel;
		*/
		
		//Load specific level - DON'T USE
		string path = "Level4";
	

		Debug.Log(path);

		ItemContainer ic = ItemContainer.Load(path);

		foreach (Item item in ic.items) {
			//Debug.Log("Type: "+item.type+", posX: "+item.posX+", posY: "+item.posY);



			//------------------------------------------------------ BACKGROUNDS -----------------------------------------------------------

			//DISGUST
			if (disgust >= 15 && disgust <= 20) {
				if (item.type == 11) {
					//Red
					grid.Add (Instantiate (background5, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
				}
			}
			if (disgust >= 7 && disgust <= 14) {
				if (item.type == 11) {
					//Orange
					grid.Add (Instantiate (background7, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
				}
			}
			if (disgust >= 1 && disgust <= 6) {
				if (item.type == 11) {
					//Warm red
					grid.Add (Instantiate (background1, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
				}
			}
			if (disgust >= -7 && disgust <= 0) {
				if (item.type == 11) {
					//Purple
					grid.Add (Instantiate (background3, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
				}
			}
			if (disgust >= -14 && disgust <= -8) {
				if (item.type == 11) {
					//Blue
					grid.Add (Instantiate (background2, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
				}
			}
			if (disgust >= -20 && disgust <= -15) {
				if (item.type == 11) {
					//Green
					grid.Add (Instantiate (background4, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
				}
			}

			if (item.type == 13) {
				//Blue
				grid.Add (Instantiate (background2, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
			}

			//POSITIVE
			//HAPPINESS

			//------------------------------------------------------ HEALING ITEMS -----------------------------------------------------------

			//Small healing orb
			if (happiness <= 7 && fear <= 12) {
				if (item.type == 7) {
					grid.Add (Instantiate (orb, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
				} 
			}
			//Small/medium
			if (happiness >= 78 && happiness <= 13 && fear <= 12) {
				if (item.type == 7) {
					if (Random.Range (0, 10) >= 5) {
						grid.Add (Instantiate (mediOrb, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
					} else {
						grid.Add (Instantiate (orb, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					}
				}
			}
			//Medium healing orb
			if (happiness >= 14 && happiness <= 20 && fear <= 12) {
				if (item.type == 24) {
					grid.Add (Instantiate (mediOrb, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
				}
			}
				
			//------------------------------------------------------ DESTRUCTIVE ELEMENTS -----------------------------------------------------------

			//--------------------------------------- TILES
			//--------------------ELECTRIC

			if (item.type == 0) {
				grid.Add (Instantiate (cube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
			}

			//LOW LEVEL
			if (fear >= 0 && fear <= 8) {
				if (item.type == 0) {
					grid.Add (Instantiate (cube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
				} else if (item.type == 1) {
					if (Random.Range (0, 10) >= 7) {
						grid.Add (Instantiate (cube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					} else {
						grid.Add (Instantiate (elecCube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					} 
				}
			}
			//MIDDLE
			if (fear >= 9 && fear <= 17) {
				if (item.type == 0) {
					grid.Add (Instantiate (cube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
				} else if (item.type == 1) {
					if (Random.Range (0, 10) >= 5) {
						grid.Add (Instantiate (cube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					} else {
						grid.Add (Instantiate (elecCube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					}  
				}
			}

			//HIGH
			if (fear >= 18 && fear <= 25) {
				if (item.type == 0) {
					grid.Add (Instantiate (cube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
				} else if (item.type == 1) {
					if (Random.Range (0, 10) >= 3) {
						grid.Add (Instantiate (cube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					} else {
						grid.Add (Instantiate (elecCube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					}  
				}
			}


			//--------------------SPIKED
			//LOW LEVEL
			if (fear <= 8) {
				if (item.type == 2) {
					if (Random.Range (0, 10) >= 7) {
						grid.Add (Instantiate (spikeCube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					} else {
						grid.Add (Instantiate (cube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					}
				}
			}
			//MIDDLE
			if (fear >= 9 && fear <= 17) {
				if (item.type == 2) {
					if (Random.Range (0, 10) >= 5) {
						grid.Add (Instantiate (spikeCube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					} else {
						grid.Add (Instantiate (cube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					}
				}
			}
			//HIGH
			if (fear >= 18) {
				if (item.type == 2) {
					if (Random.Range (0, 10) >= 3) {
						grid.Add (Instantiate (spikeCube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					} else {
						grid.Add (Instantiate (cube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					}
				}
			}
		

			//--------------------------------------- ENEMIES
			//--------------------GROUND
			if (item.type == 5) {
				if (Random.Range (0, 10) >= 1) {
					grid.Add (Instantiate (hostileBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
					IntelBot hostScript = grid [grid.Count - 1].GetComponent<IntelBot> ();
					hostScript.GetPlayer (grid [playerIndex]);
					enemiesLoaded++;
				} 
			} 

			if (anger <= 5) {
				if (item.type == 4) {
					if (Random.Range (0, 10) >= 1) {
						grid.Add (Instantiate (intelBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
						enemiesLoaded++;
					} 
				} 
			}
			if (anger >= 6 && anger <= 10) {
				if (item.type == 4) {
						grid.Add (Instantiate (intelBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
						enemiesLoaded++;
				} 
			}
			 if (anger >= 11 && anger <= 15) {
				if (item.type == 4) {
					if (Random.Range (0, 10) >= 6) {
						grid.Add (Instantiate (hostileBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
						IntelBot hostScript = grid [grid.Count - 1].GetComponent<IntelBot> ();
						hostScript.GetPlayer (grid [playerIndex]);
						enemiesLoaded++;
					}
				} else {
					if (item.type == 4) {
						grid.Add (Instantiate (intelBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
						enemiesLoaded++; 
					}
				}
			}
			if (anger >= 16) {
				if (item.type == 4) {
					if (Random.Range (0, 10) >= 8) {
						grid.Add (Instantiate (hostileBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
						IntelBot hostScript = grid [grid.Count - 1].GetComponent<IntelBot> ();
						hostScript.GetPlayer (grid [playerIndex]);
						enemiesLoaded++;
					}
				} else {
					if (item.type == 4) {
						grid.Add (Instantiate (intelBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
						enemiesLoaded++; 
					}
				}
			}

			//Enemies - FLYING BOT HOSTILE
			//Individual

			if (item.type == 26) {
				enemiesLoaded++;
				grid.Add (Instantiate (idleFlyBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
				IntelIdle floatScript = grid [grid.Count - 1].GetComponent<IntelIdle> ();
				floatScript.GetPlayer (grid [playerIndex]);
			}
	

			//Definite
			if (anger <= 5) {
				if (item.type == 6) {
					if (Random.Range (0, 10) >= 1) {
						enemiesLoaded++;
						grid.Add (Instantiate (flyBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
						IntelBot flyScript = grid [grid.Count - 1].GetComponent<IntelBot> ();
						flyScript.GetPlayer (grid [playerIndex]);
					}
				}
			}
			//Low
			else if (anger > 5 && anger <= 10) {
				if (item.type == 6) {
					enemiesLoaded++;
					grid.Add (Instantiate (flyBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
					IntelBot flyScript = grid [grid.Count - 1].GetComponent<IntelBot> ();
					flyScript.GetPlayer (grid [playerIndex]);
				}
			}
			//High
			 else if (anger >= 11) {
				if (item.type == 6) {
					if (Random.Range (0, 10) >= 6) {
						grid.Add (Instantiate (flyBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
						IntelBot hostScript = grid [grid.Count - 1].GetComponent<IntelBot> ();
						hostScript.GetPlayer (grid [playerIndex]);
						enemiesLoaded++;
					} else {
						grid.Add (Instantiate (idleFlyBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
						IntelIdle floatScript = grid [grid.Count - 1].GetComponent<IntelIdle> ();
						floatScript.GetPlayer (grid [playerIndex]);
						enemiesLoaded++; 
					}
				}
			}

			//---------------------------------------------------

			//Backgrounds
			if (item.type == 29) {
				grid.Add (Instantiate (background6, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
			}
				
			//Moving platforms
			//A
			if (item.type == 22) {
				grid.Add (Instantiate (flyCube, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
			}
			//B
			if (item.type == 23) {
				grid.Add (Instantiate (flyCubeB, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
			}

			//SCENERY ------------------------
			//Walls
			if (item.type == 12) {
				grid.Add (Instantiate (wall, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
			}
			//Pillar
			if (item.type == 17) {
				grid.Add (Instantiate (pillar, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
			}
			//Button
			if (item.type == 18) {
				grid.Add (Instantiate (button, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
			}
			//Trap Button
			if (item.type == 19) {
				grid.Add (Instantiate (trapButton, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
			}

			//Doors
			if (item.type == 9) {
				grid.Add (Instantiate (entryDoor, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
			}
			if (item.type == 10) {
				grid.Add (Instantiate (exitDoor, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
			}

			//OTHER ----------------------------

			//Player
			if (item.type == 3) {
				grid.Add (Instantiate (player, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
				playerIndex = grid.Count - 1;
			}
			//Terminal
			if (item.type == 28) {
				grid.Add (Instantiate (terminal, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);	
			}
			//Friend Bot
			if (item.type == 21) {
				grid.Add (Instantiate (friendBot, new Vector3 (item.posX, item.posY, 0), Quaternion.Euler (0, 0, 0)) as GameObject);
				robotsLoaded++;
			}
		}
	}

	private void remainingEnemies() {
		PlayerControl playerScript = grid [playerIndex].GetComponent<PlayerControl> ();
		float remaining = (float)(enemiesLoaded - playerScript.enemiesDestroyed);
		changeHappiness (remaining);
		changeFear (- remaining/2);
	}
	private void remainingRobots() {
		ButtonPress buttonScript = GetComponent<ButtonPress> ();
		float freed = (float)(robotsLoaded- buttonScript.robotsFreed);
		changeAnger (freed - 3);
	}
  
	private void remainingOrbs() {
		ButtonPress buttonScript = grid [buttonIndex].GetComponent<ButtonPress> ();
		float left = (float)(robotsLoaded- buttonScript.robotsFreed);
		changeAnger (left + 2);
	}

	//
	void decideEnding() {
		if(happiness >= 10 || anger <= 9) {
			SceneManager.LoadScene (4);
		}
		if(anger >= 10) {
			SceneManager.LoadScene (5);
		}
	}

	protected void destroyLevel(){
		remainingEnemies ();
		//remainingRobots ();
		//remainingOrbs ();

		//UNCOMMENT

		unusedLevels.Remove (currentLevel);


		if (unusedLevels.Count <= 0) {
			PlayerPrefs.SetInt ("Final Health",life);
			decideEnding ();
		}


		for (int i = 0; i < grid.Count; i++) {
			Destroy (grid [i]);
		}

		grid.Clear ();
		enemiesLoaded = 0;
		robotsLoaded = 0;
	}
}
