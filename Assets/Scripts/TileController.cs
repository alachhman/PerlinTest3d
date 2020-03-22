using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour {
	public Material m_Material;
	public Color color;
	public Tile tile;
	public Text tileText;
	public Text enemyText;
	public List<Enemy> enemies;
	public Tile[,] tiles;
	public Enemy thisEnemy;
	public bool isSomethingSelected;
	public List<Tile> selectedtiles;

	void Start() {
		enemies = GameObject.Find("TileMap").GetComponent<TileGenerator>().enemies;
		tiles = GameObject.Find("TileMap").GetComponent<TileGenerator>().tilesList;
		enemyText = GameObject.Find("EntityText").GetComponent<Text>();
		tileText = GameObject.Find("EnvironmentText").GetComponent<Text>();
		//tile = new Tile("false", getTypes());
		//Fetch the Material from the Renderer of the GameObject
		m_Material = GetComponent<Renderer>().material;
		color = m_Material.color;
		thisEnemy = enemies.Find(enemy => tile.coords[0] == enemy.coords[0] && tile.coords[1] == enemy.coords[1]);
		isSomethingSelected = false;
		selectedtiles = new List<Tile>();
	}

	void Update() { }

	void OnMouseOver() {
		bool hasEnemy = enemies.Exists(enemy => tile.coords[0] == enemy.coords[0] && tile.coords[1] == enemy.coords[1]);
		if (hasEnemy) {
			enemyText.text = thisEnemy.toString();
		}
		else {
			enemyText.text = "NAME: -\r\n" +
			                 "ATK: -	DEF: -\r\n" +
			                 "MAG: -	SPD: -";
		}

		if (Input.GetMouseButtonDown(0) && hasEnemy) {
			if (!isSomethingSelected) {
				isSomethingSelected = true;
				int x = tile.coords[0];
				int y = tile.coords[1];
				print("CLICKED: " + x + ", " + y);
				int max = thisEnemy.MOV;
				for (int n = 0; n < max + 1; n++) {
					for (int i = 0; i < n + 1; i++) {
						for (int j = 0; j < n + 1; j++) {
							print("[" + (i + j - n + x) + "," + (i - j + y) + "]");
							int xAxis = i + j - n + x;
							int yAxis = i - j + y;
							if (((xAxis >= 0) && (xAxis < 16)) && ((yAxis >= 0) && (yAxis < 16))) {
								tiles[xAxis, yAxis].tileObject.GetComponent<TileController>().m_Material.color =
									Color.yellow;
								tiles[xAxis, yAxis].tileObject.GetComponent<TileController>().isSomethingSelected =
									true;
								selectedtiles.Add(tiles[xAxis, yAxis]);
							}
						}
					}
				}
			}
			else {
				isSomethingSelected = false;
				foreach (Tile selected in selectedtiles) {
					tiles[selected.coords[0], selected.coords[1]].tileObject.GetComponent<TileController>()
						.isSomethingSelected = false;
					tiles[selected.coords[0], selected.coords[1]].tileObject.GetComponent<TileController>().m_Material
							.color =
						tiles[selected.coords[0], selected.coords[1]].tileObject.GetComponent<TileController>().color;
				}
			}
		}


		// Change the Color of the GameObject when the mouse hovers over it
		if (!isSomethingSelected) {
			m_Material.color = Color.red;
		}

		tileText.text = tile.toString();
		//print(tile.occupiedBy);
	}

	void OnMouseExit() {
		//Change the Color back to white when the mouse exits the GameObject
		if (!isSomethingSelected) {
			m_Material.color = color;
		}
	}

	string getTypes() {
		Color color = gameObject.GetComponent<Renderer>().sharedMaterial.color;
		if (color == Color.blue) {
			return "Ocean";
		}
		else if (color == Color.green) {
			return "Grass";
		}
		else if (color == Color.yellow) {
			return "EnemySpawn";
		}
		else {
			return "Sand";
		}
	}
}