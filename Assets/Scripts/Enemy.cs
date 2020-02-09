using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public Sprite sprite;
    public string enemyName;
    public int[] coords;
    /*
     * Don't forget the stats
     */

    public Enemy(Sprite sprite, string enemyName, int[] coords) {
        this.sprite = sprite;
        this.enemyName = enemyName;
        this.coords = coords;
    }

    public String toString() {
        return "Name: " + this.enemyName;
    }
}
