using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileType : MonoBehaviour {

    public char character = ' '; //char		
    public Color foreColor = new Color(1f, 1f, 1f, 1f); //fore color
    public Color backColor = new Color(0f, 0f, 0f, 1f); //back color
    //priority
    //private ignite //value representing flammability
    //fireType //value representing the type of fire spawned
    //int discovType //value representing its discovered value
    //promoteType //value representing what happens if the tile can become a different tile
    //promoteChance //value representing the random chance it will promote on its own
    //glowLight //what kind of light, if any, does this produce
    public string layer; // the layer the tile belongs to
    //flags 
    public string description;
    public string flavorText = "";

    public FloorTileType(char _character, Color _foreColor, Color _backColor, string _description, string _flavorText)
    {
        character = _character;
        foreColor = _foreColor;
        backColor = _backColor;
        //layer = _layer;
        description = _description;
        flavorText = _flavorText;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
