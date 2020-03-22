using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile{

    public enum Types{
        Grass,
        Sand,
        Ocean,
        EnemySpawn
    }

    public string occupiedBy;
    public string type;
    public Color color;
    public float noise;
    public int[] coords;
    public Enemy enemy;
    public GameObject tileObject;
    public float verticalityScale;
    
    public Tile(string occupiedBy, string type, Color color, float noise, int[] coords, GameObject tileObject, float verticalityScale){
        this.occupiedBy = occupiedBy;
        this.type = type;
        this.color = color;
        this.noise = noise;
        this.coords = coords;
        this.tileObject = tileObject;
        this.verticalityScale = verticalityScale;
    }

    public string toString(){
        return "TYPE: " + this.type + "\r\n" +
               "OCCUPIED: " + this.occupiedBy + "\r\n" +
               "COORDS: [ " + this.coords[0] + " , " + this.coords[1] + " ]";
    }
}
