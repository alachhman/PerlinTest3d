using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy {
    public GameObject gameObject;
    public string enemyName;

    public int[] coords;
    public int ATK = Random.Range(0, 10);
    public int DEF = Random.Range(0, 10);
    public int MAG = Random.Range(0, 10);
    public int SPD = Random.Range(0, 10);
    public int MOV = Random.Range(0, 4);

    public Enemy(GameObject gameObject, string enemyName, int[] coords) {
        this.gameObject = gameObject;
        this.enemyName = enemyName;
        this.coords = coords;
    }

    public String toString() {
        return "NAME: " + this.enemyName + "\r\n"
             + "ATK: " + this.ATK + "    " + "DEF: " + this.DEF + "\r\n"
             + "MAG: " + this.MAG + "    " + "SPD: " + this.SPD + "\r\n";
    }
}