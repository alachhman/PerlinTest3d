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

    public Tile(string occupiedBy, string type, Color color, float noise){
        this.occupiedBy = occupiedBy;
        this.type = type;
        this.color = color;
        this.noise = noise;
    }

    public string toString(){
        return "Type: " + this.type + "\r\nOccupied By: " + this.occupiedBy;
    }
}
