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
    public List<Tile> tiles;
    
    void Start() {
        enemies = GameObject.Find("TileMap").GetComponent<TileGenerator>().enemies;
        tiles = GameObject.Find("TileMap").GetComponent<TileGenerator>().tilesList;
        enemyText = GameObject.Find("EntityText").GetComponent<Text>();
        tileText = GameObject.Find("EnvironmentText").GetComponent<Text>();
        //tile = new Tile("false", getTypes());
        //Fetch the Material from the Renderer of the GameObject
        m_Material = GetComponent<Renderer>().material;
        color = m_Material.color;
    }

    void Update() { }

    void OnMouseOver() {
        if (enemies.Exists(enemy => tile.coords[0] == enemy.coords[0] && tile.coords[1] == enemy.coords[1])) {
            enemyText.text = enemies
                .Find(enemy => tile.coords[0] == enemy.coords[0] && tile.coords[1] == enemy.coords[1]).toString();
        }
        else {
            enemyText.text = "NAME: -\r\n" +
                             "ATK: -	DEF: -\r\n" +
                             "MAG: -	SPD: -";
        }

        // Change the Color of the GameObject when the mouse hovers over it
        m_Material.color = Color.red;
        tileText.text = tile.toString();
        //print(tile.occupiedBy);
    }

    void OnMouseExit() {
        //Change the Color back to white when the mouse exits the GameObject
        m_Material.color = color;
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