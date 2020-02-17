using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy {
    public Sprite sprite;
    public string enemyName;

    public int[] coords;
    public int ATK = Random.Range(0, 10);
    public int DEF = Random.Range(0, 10);
    public int MAG = Random.Range(0, 10);
    public int SPD = Random.Range(0, 10);

    public Enemy(Sprite sprite, string enemyName, int[] coords) {
        this.sprite = sprite;
        this.enemyName = enemyName;
        this.coords = coords;
    }

    public String toString() {
        return "NAME: " + this.enemyName + "\r\n"
             + "ATK: " + this.ATK + "    " + "DEF: " + this.DEF + "\r\n"
             + "MAG: " + this.MAG + "    " + "SPD: " + this.SPD + "\r\n";
    }
}