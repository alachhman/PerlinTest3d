﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileGenerator : MonoBehaviour {
    private bool isGenerating;

    public Material Material1;

    public Material Material2;

    // Width and height of the texture in pixels.
    public int pixWidth;
    public int pixHeight;

    // The origin of the sampled area in the plane.
    public float xOrg;
    public float yOrg;

    // The number of cycles of the basic noise pattern that are repeated
    // over the width and height of the texture.
    public float scale = 1.0F;
    public float verticalityScale = 0f;

    public float[,] grid = new float[25, 25];

    public GameObject tileMapCurr;
    public GameObject tileMapPrefab;

    public GameObject block1;
    public int worldWidth = 25;
    public int worldHeight = 25;
    public float spawnSpeed = 0;

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
            ClearChildren(tileMapCurr);
            noise = new PerlinNoiseGrid(10, 10, 1.0f);
            StartCoroutine(CreateWorld(generateTileGrid(noise)));
        }
    }

    Tile[,] generateTileGrid(PerlinNoiseGrid noise) {
        Tile[,] tiles = new Tile[worldHeight, worldWidth];
        for (int x = 0; x < worldWidth; x++) {
            for (int z = 0; z < worldHeight; z++) {
                float currentNoise = noise[x, z];
                Color currentColor;
                string type;
                string occupiedBy = "-";
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
                        break;
                }

                tiles[x, z] = new Tile(occupiedBy, type, currentColor, currentNoise);
            }
        }

        return tiles;
    }

    private IEnumerator CreateWorld(Tile[,] tiles) {
        isGenerating = true;
        for (int x = 0; x < worldWidth; x++) {
            yield return new WaitForSeconds(spawnSpeed);
            for (int z = 0; z < worldHeight; z++) {
                yield return new WaitForSeconds(spawnSpeed);
                GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
                block.GetComponent<Renderer>().sharedMaterial.color = tiles[x, z].color;
                block.GetComponent<TileController>().tile = tiles[x, z];
                block.transform.parent = transform;
                if (tiles[x, z].type == "Ocean") {
                    //verticalityScale = tiles[x, z].noise * 1.0f;
                    verticalityScale = 0f;
                }
                else {
                    verticalityScale = (tiles[x, z].noise * 3f) - 0.9f;
                }

                block.transform.localPosition = new Vector3(x, verticalityScale - 4.5f, z);
            }
        }

        isGenerating = false;
    }

    public void ClearChildren(GameObject obj) {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles) {
            Destroy(tile);
        }
    }
}