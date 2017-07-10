using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : ScriptableObject {

    public Dictionary<string, char> chars = new Dictionary<string, char>();
    public Dictionary<string, FloorTileType> floorTileLibrary = new Dictionary<string, FloorTileType>();
    public Dictionary<string, Color> colors = new Dictionary<string, Color>();    

    // Use this for initialization
    public void Start ()
    {
        initChars();
        initColors();
        initTiles();
	}

    private void initChars()
    {
        chars.Add("FLOOR_CHAR", '.');
        chars.Add("LIQUID_CHAR", '~');
        chars.Add("CHASM_CHAR", ':');
        chars.Add("TRAP_CHAR", '%');
        chars.Add("FIRE_CHAR", '^');
        chars.Add("GRASS_CHAR", '"');
        chars.Add("BRIDGE_CHAR", '=');
        chars.Add("DESCEND_CHAR", '>');
        chars.Add("ASCEND_CHAR", '<');
        chars.Add("WALL_CHAR", '#');
        chars.Add("DOOR_CHAR", '+');
        chars.Add("OPEN_DOOR_CHAR", '\'');
        chars.Add("ASH_CHAR", '\'');
        chars.Add("BONES_CHAR", ',');
        chars.Add("MUD_CHAR", ',');
        chars.Add("WEB_CHAR", ':');
        chars.Add("FOLIAGE_CHAR", '&');
        chars.Add("VINE_CHAR", ':');
        chars.Add("ALTAR_CHAR", '|');
        chars.Add("LEVER_CHAR", '/');
        chars.Add("LEVER_PULLED_CHAR", '\\');
        chars.Add("STATUE_CHAR", '&');
        chars.Add("VENT_CHAR", '=');
        chars.Add("DEWAR_CHAR", '&');
        chars.Add("TRAMPLED_FOLIAGE_CHAR", '"');
        chars.Add("PLAYER_CHAR", '@');
        chars.Add("AMULET_CHAR", ',');
        chars.Add("FOOD_CHAR", ';');
        chars.Add("SCROLL_CHAR", '?');
        chars.Add("RING_CHAR", '=');
        chars.Add("CHARM_CHAR", '+');
        chars.Add("POTION_CHAR", '!');
        chars.Add("ARMOR_CHAR", '[');
        chars.Add("WEAPON_CHAR", '(');
        chars.Add("STAFF_CHAR", '\\');
        chars.Add("WAND_CHAR", '~');
        chars.Add("GOLD_CHAR", '*');
        chars.Add("GEM_CHAR", '+');
        chars.Add("TOTEM_CHAR", '0');
        chars.Add("TURRET_CHAR", '*');
        chars.Add("UNICORN_CHAR", 'U');
        chars.Add("KEY_CHAR", '-');
        chars.Add("ELECTRIC_CRYSTAL_CHAR", '$');
        chars.Add("UP_ARROW_CHAR", '^');
        chars.Add("DOWN_ARROW_CHAR", 'v');
        chars.Add("LEFT_ARROW_CHAR", '<');
        chars.Add("RIGHT_ARROW_CHAR", '>');
        chars.Add("UP_TRIANGLE_CHAR", '^');
        chars.Add("DOWN_TRIANGLE_CHAR", 'v');
        chars.Add("OMEGA_CHAR", '^');
        chars.Add("THETA_CHAR", '0');
        chars.Add("LAMDA_CHAR", '\\');
        chars.Add("KOPPA_CHAR", 'k');
        chars.Add("LOZENGE_CHAR", '+');
        chars.Add("CROSS_PRODUCT_CHAR", 'x');
        chars.Add("CHAIN_TOP_LEFT", '\\');
        chars.Add("CHAIN_BOTTOM_RIGHT", '\\');
        chars.Add("CHAIN_TOP_RIGHT", '/');
        chars.Add("CHAIN_BOTTOM_LEFT", '/');
        chars.Add("CHAIN_TOP", '|');
        chars.Add("CHAIN_BOTTOM", '|');
        chars.Add("CHAIN_LEFT", '-');
        chars.Add("CHAIN_RIGHT", '-');
        chars.Add("BAD_MAGIC_CHAR", '+');
        chars.Add("GOOD_MAGIC_CHAR", '$');
    }

    private void initColors()
    {
        //colors
        colors.Add("white", new Color(1f, 1f, 1f, 1f));
        colors.Add("gray", new Color(0.5f, 0.5f, 0.5f, 1f));
        colors.Add("darkGray", new Color(0.3f, 0.3f, 0.3f, 1f));
        colors.Add("veryDarkGray", new Color(0.15f, 0.15f, 0.15f, 1f));
        colors.Add("black", new Color(0f, 0f, 0f, 1f));
        colors.Add("yellow", new Color(1f, 1f, 0f, 1f));
        colors.Add("darkYellow", new Color(0.5f, 0.5f, 0f, 1f));
        colors.Add("teal", new Color(0.3f, 1f, 1f, 1f));
        colors.Add("purple", new Color(1f, 0f, 1f, 1f));
        colors.Add("darkPurple", new Color(0.5f, 0f, 0.5f, 1f));
        colors.Add("brown", new Color(0.6f, 0.4f, 0f, 1f));
        colors.Add("green", new Color(0f, 1f, 0f, 1f));
        colors.Add("darkGreen", new Color(0f, 0.5f, 0f, 1f));
        colors.Add("orange", new Color(1f, 0.5f, 0f, 1f));
        colors.Add("darkOrange", new Color(0.5f, 2.025f, 0f, 1f));
        colors.Add("blue", new Color(0f, 0f, 1f, 1f));
        colors.Add("darkBlue", new Color(0f, 0f, 0.5f, 1f));
        colors.Add("darkTurquoise", new Color(0f, 0.4f, 65, 1f));
        colors.Add("lightBlue", new Color(0.4f, 0.4f, 1f, 1f));
        colors.Add("pink", new Color(1f, 0.6f, 0.66f, 1f));
        colors.Add("red", new Color(1f, 0f, 0f, 1f));
        colors.Add("darkRed", new Color(0.5f, 0f, 0f, 1f));
        colors.Add("tan", new Color(0.8f, 0.67f, 0.15f, 1f));

        //tile Colors
        colors.Add("floorForeColor", new Color(0.3f, 0.3f, 0.3f, 1f));
        colors.Add("floorBackColor", new Color(0.2f, 0.2f, 0.1f, 1f));
        colors.Add("wallForeColor", new Color(0.7f, 0.7f, 0.7f, 1f));
        colors.Add("wallBackColor", new Color(0.45f, 0.45f, 0.40f, 1f));
    }

    private void initTiles()
    {
        floorTileLibrary.Add("default_ground", new FloorTileType(chars["FLOOR_CHAR"], colors["floorForeColor"], colors["floorBackColor"], "the ground", ""));
        floorTileLibrary.Add("default_wall", new FloorTileType(chars["WALL_CHAR"], colors["wallForeColor"], colors["wallBackColor"], "a stone wall", ""));
        //print("here100");
        //print(floorTileLibrary["default_wall"]);
        //print("here200");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
