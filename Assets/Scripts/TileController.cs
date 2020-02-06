using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    public Material m_Material;
    public Color color;
    public Tile tile;
    public Text text;

    void Start()
    {
        text = GameObject.Find("EnvironmentText").GetComponent<Text>();
        //tile = new Tile("false", getTypes());
        //Fetch the Material from the Renderer of the GameObject
        m_Material = GetComponent<Renderer>().material;
        color = m_Material.color;
    }

    void Update()
    {

    }

    void OnMouseOver()
    {
        // Change the Color of the GameObject when the mouse hovers over it
        m_Material.color = Color.red;
        text.text = tile.toString();
        //print(tile.occupiedBy);
    }

    void OnMouseExit()
    {
        //Change the Color back to white when the mouse exits the GameObject
        m_Material.color = color;
    }

    string getTypes(){
        Color color = gameObject.GetComponent<Renderer>().sharedMaterial.color;
        if(color == Color.blue){
            return "Ocean";
        }else if(color == Color.green){
            return "Grass";
        }else if(color == Color.yellow){
            return "EnemySpawn";
        }else{
            return "Sand";
        }
    }
}
