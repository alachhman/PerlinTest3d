using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileGenerator : MonoBehaviour {
	public bool isGenerating;

	public Material Material1;

	public Material Material2;

	// Width and height of the texture in pixels.
	public int pixWidth;
	public int pixHeight;

	// The origin of the sampled area in the plane.
	public int xOrg;
	public int yOrg;

	// The number of cycles of the basic noise pattern that are repeated
	// over the width and height of the texture.
	public float scale = 1.0F;

	public float[,] grid = new float[16, 16];

	public GameObject tileMapCurr;
	public GameObject tileMapPrefab;
	public GameObject EnemyGameObject;

	public GameObject block1;
	public static int worldWidth = 16;
	public static int worldHeight = 16;
	public float spawnSpeed = 0;
	public int maxEnemies;

	public List<Enemy> enemies = new List<Enemy>();
	public Tile[,] tilesList = new Tile[worldHeight, worldWidth];

	PerlinNoiseGrid noise;

	// Start is called before the first frame update
	void Start() {
		isGenerating = false;
		noise = new PerlinNoiseGrid(pixWidth, pixHeight, scale);
		StartCoroutine(CreateWorld(generateTileGrid(noise)));
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown("r") && !isGenerating) {
			ClearChildren();
			//noise = new PerlinNoiseGrid(pixWidth, pixHeight, scale);
			xOrg += 16;
			StartCoroutine(CreateWorld(generateTileGrid(noise)));
		}
	}

	Tile[,] generateTileGrid(PerlinNoiseGrid noise) {
		Tile[,] tiles = new Tile[worldHeight, worldWidth];
		for (int x = 0; x < worldWidth; x++) {
			for (int z = 0; z < worldHeight; z++) {
				float currentNoise = noise[x + xOrg, z + yOrg];
				float vScale = (currentNoise * 3f) - 0.9f;
				Color currentColor;
				string type;
				string occupiedBy = "-";
				int[] coords = {x, z};
				switch (currentNoise) {
					case float n when (currentNoise >= 0.45):
						var tileSeed = Random.Range(0, 10);
						var buildingSeed = Random.Range(0.0f, 1.0f);
						if (tileSeed > 8) {
							currentColor = new Color(0f, 0.42f, 0f);
							type = "Forest";
							if (x > 0 && z > 0) {
								var additionalTreeSeed = Random.Range(0, 10);
								if (additionalTreeSeed > 3) {
									if (tiles[x - 1, z].type == "Plains") {
										tiles[x - 1, z].color = new Color(0f, 0.42f, 0f);
										tiles[x - 1, z].type = "Forest";
									}
								}

								if (additionalTreeSeed > 6) {
									if (tiles[x, z - 1].type == "Plains") {
										tiles[x, z - 1].color = new Color(0f, 0.42f, 0f);
										tiles[x, z - 1].type = "Forest";
									}
								}
							}
						}
						else {
							if (buildingSeed > 0.99f) {
								currentColor = Color.magenta;
								type = "Plains";
								occupiedBy = "Building";
							}
							else {
								currentColor = new Color(0.02f, 0.85f, 0f);
								type = "Plains";
							}
						}

						break;
					case float n when (currentNoise >= 0.35):
						currentColor = new Color(255f / 255f, 249f / 255f, 177f / 255f);
						type = "Sand";
						break;
					default:
						currentColor = Color.blue;
						type = "Ocean";
						vScale = 0f;
						break;
				}
				Tile tile = new Tile(occupiedBy, type, currentColor, currentNoise, coords,null, vScale);
				tiles[x, z] = tile;
				tilesList = tiles;
			}
		}
		return tiles;
	}

	private IEnumerator CreateWorld(Tile[,] tiles) {
		isGenerating = true;
		var enemyCount = 0;
		for (int x = 0; x < worldWidth; x++) {
			yield return new WaitForSeconds(spawnSpeed);
			for (int z = 0; z < worldHeight; z++) {
				yield return new WaitForSeconds(spawnSpeed);
				GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
				block.GetComponent<Renderer>().sharedMaterial.color = tiles[x, z].color;
				block.GetComponent<TileController>().tile = tiles[x, z];
				block.transform.parent = transform;
				block.transform.localPosition = new Vector3(x, tiles[x, z].verticalityScale - 4.5f, z);
				tiles[x, z].tileObject = block;
				var tile = tiles[x, z];
				var enemySpawn = Random.Range(0f, 1f);
				if (tile.type == "Plains") {
					if (enemySpawn > 0.95f) {
						GameObject enemy =
							Instantiate(EnemyGameObject, Vector3.zero,
								EnemyGameObject.transform.rotation) as GameObject;
						Sprite temp;
						Enemy enemyObj;
						
						int[] coords = {x, z};
						if (enemyCount == 0) {
							temp = Resources.Load("Images/Player", typeof(Sprite)) as Sprite;
							enemyObj = new Enemy(enemy, "Sword Slime", coords);
						}
						else {
							temp = Resources.Load("Images/enemy_1", typeof(Sprite)) as Sprite;
							enemyObj = new Enemy(enemy, "Nature Slime", coords);
						}
						enemy.GetComponent<EnemyController>().enemy = enemyObj;
						enemy.GetComponent<SpriteRenderer>().sprite = temp;
						enemies.Add(enemyObj);
						enemy.transform.localPosition =
							new Vector3(tile.coords[0] + 4, tile.verticalityScale + 0.78f, tile.coords[1] + 4);
						enemyCount++;
					}
				}
			}
		}

		//ToDo: some editor work to make a clone of enemy prefab to simulate a unit, then implement the logic to only spawn one of him
		//leave scaling for the future
		// int[] unitSpawn = {Random.Range(0, 25), Random.Range(0, 25)};
		// while (tiles[unitSpawn[0], unitSpawn[1]].type != "Plains") {
		//     
		// }


		isGenerating = false;
	}

	public void ClearChildren() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies) {
			Destroy(enemy);
		}

		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
		foreach (GameObject tile in tiles) {
			Destroy(tile);
		}
	}
}