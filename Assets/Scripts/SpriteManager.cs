using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class SpriteManager : MonoBehaviour {

    public Texture2D texture;
    private Sprite[] sprites;

	// Use this for initialization
	public void Start () {
        //sprites = Resources.LoadAll<Sprite>(texture.name);
        buildSprites();
        print(sprites.Length);
        for (int i = 0; i < sprites.Length; i++)
        {
            print(sprites[i].ToString());
        }
    }

    public Sprite getSpriteFromChar(char c)
    {
        if(c == ' ')
        {
            return sprites[0];
        }
        else if (c == '#')
        {
            return sprites[3];
        }
        else if ( c == '.')
        {
            return sprites[14];
        }
        else
        {
            return sprites[0];
        } 
    }

    public void buildSprites()
    {
        string spriteSheet = AssetDatabase.GetAssetPath(texture);
        sprites = AssetDatabase.LoadAllAssetsAtPath(spriteSheet).OfType<Sprite>().ToArray();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
