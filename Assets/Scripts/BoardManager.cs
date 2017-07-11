using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

public class BoardManager : MonoBehaviour
{
    // Using Serializable allows us to embed a class with sub properties in the inspector.
    [Serializable]
    public class Count
    {
        public int minimum;             //Minimum value for our Count class.
        public int maximum;             //Maximum value for our Count class.

        //Assignment constructor.
        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public SpriteManager spriteScript;
    public FloorManager floorManager;

    public float width = 0.19f;
    public float height = 0.33f;

    public int columns = 79;                                       //Number of columns in our game board.
    public int rows = 29;                                          //Number of rows in our game board.
    public Count wallCount = new Count(100, 300);                  //Lower and upper limit for our random number of walls per level.
    public Count itemCount = new Count(5, 25);                      //Lower and upper limit for our random number of food items per level.
    public GameObject exit;                                        //Prefab to spawn for exit.
    public GameObject[] floorTiles;                                //Array of floor prefabs.
    public GameObject[] wallTiles;                                 //Array of wall prefabs.
    public GameObject[] itemTiles;                                 //Array of food prefabs.
    public GameObject[] enemyTiles;                                //Array of enemy prefabs.
    public GameObject[] outerWallTiles;                            //Array of outer tile prefabs.

    private GameObject[] dynWallTiles;
    private GameObject[][][] floorGrid;

    private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
    private List<Vector3> gridPositions = new List<Vector3>();   //A list of possible locations to place tiles.

    public void draw(int x, int y, char c, Color fg, Color bg)
    {

    }

    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList()
    {
        //Clear our list gridPositions.
        gridPositions.Clear();

        //Loop through x axis (columns).
        for (int x = 1; x < columns - 1; x++)
        {
            //Within each column, loop through y axis (rows).
            for (int y = 1; y < rows - 1; y++)
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridPositions.Add(new Vector3(x*width, y*height, 0f));
            }
        }
    }


    //Sets up the outer walls and floor (background) of the game board.
    void BoardSetup()
    {
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject("Board").transform;

        //floorGrid = new GameObject[columns+1][][];

        //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
        for (int x = -1; x < columns + 1; x++)
        {
            //Loop along y axis, starting from -1 to place floor or outerwall tiles.
            for (int y = -1; y < rows + 1; y++)
            {
                //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                //GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                GameObject floorInstantiate = new GameObject();
                FloorTileType my_floor = floorManager.floorTileLibrary["default_ground"];
                floorInstantiate.AddComponent<SpriteRenderer>();
                SpriteRenderer sr1 = floorInstantiate.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                sr1.sprite = spriteScript.getSpriteFromChar(' ');
                sr1.color = my_floor.backColor;
                sr1.sortingLayerName = "Floor";

                GameObject topInstantiate = new GameObject();
                //topInstantiate.AddComponent<SpriteRenderer>();
                //SpriteRenderer sr2 = topInstantiate.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                //sr2.sprite = spriteScript.getSpriteFromChar(my_floor.character);
                //sr2.color = my_floor.foreColor;
                //sr2.sortingLayerName = "Dungeon";

                //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    topInstantiate.AddComponent<SpriteRenderer>();
                    SpriteRenderer sr2 = topInstantiate.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                    sr2.sprite = spriteScript.getSpriteFromChar('#');
                    sr2.color = my_floor.foreColor;
                    sr2.sortingLayerName = "Dungeon";
                }
                else
                {
                    topInstantiate.AddComponent<SpriteRenderer>();
                    SpriteRenderer sr2 = topInstantiate.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                    sr2.sprite = spriteScript.getSpriteFromChar(my_floor.character);
                    sr2.color = my_floor.foreColor;
                    sr2.sortingLayerName = "Dungeon";
                }
                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance1 = Instantiate(topInstantiate, new Vector3(x*width, y*height, 0f), Quaternion.identity) as GameObject;
                //floorGrid[x][y][0] = toInstantiate;
                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance1.transform.SetParent(boardHolder);
                GameObject instance2 = Instantiate(floorInstantiate, new Vector3(x * width, y * height, 0f), Quaternion.identity) as GameObject;
                //floorGrid[x][y][0] = toInstantiate;
                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance2.transform.SetParent(boardHolder);
            }
        }
    }


    //RandomPosition returns a random position from our list gridPositions.
    Vector3 RandomPosition()
    {
        //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        int randomIndex = Random.Range(0, gridPositions.Count);

        //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        Vector3 randomPosition = gridPositions[randomIndex];

        //Remove the entry at randomIndex from the list so that it can't be re-used.
        gridPositions.RemoveAt(randomIndex);

        //Return the randomly selected Vector3 position.
        return randomPosition;
    }


    //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        //Choose a random number of objects to instantiate within the minimum and maximum limits
        int objectCount = Random.Range(minimum, maximum + 1);

        //Instantiate objects until the randomly chosen limit objectCount is reached
        for (int i = 0; i < objectCount; i++)
        {
            //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
            Vector3 randomPosition = RandomPosition();

            //Choose a random tile from tileArray and assign it to tileChoice
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

            //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
            Instantiate(tileChoice, randomPosition, Quaternion.identity);

            //floorGrid[randomPosition.x][randomPosition.y][randomPosition.z];
        }
    }


    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene(int level)
    {
        //Creates the outer walls and floor.
        BoardSetup();

        //Reset our list of gridpositions.
        InitialiseList();

        GameObject myWall = new GameObject();
        FloorTileType my_wall = floorManager.floorTileLibrary["default_wall"];
        Sprite s = spriteScript.getSpriteFromChar(my_wall.character);
        myWall.AddComponent<SpriteRenderer>().sprite = s;
        SpriteRenderer sr = myWall.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        sr.color = my_wall.backColor;
        dynWallTiles = new GameObject[1];
        dynWallTiles[0] = myWall;
        //Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
        LayoutObjectAtRandom(dynWallTiles, wallCount.minimum, wallCount.maximum);
        //LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);

        //Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
        LayoutObjectAtRandom(itemTiles, itemCount.minimum, itemCount.maximum);

        //Determine number of enemies based on current level number, based on a logarithmic progression
        int enemyCount = (int)Mathf.Log(level, 2f);

        //Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

        //Instantiate the exit tile in the upper right hand corner of our game board
        Instantiate(exit, new Vector3((columns - 1)*width, (rows - 1)*height, 0f), Quaternion.identity);
    }
}
