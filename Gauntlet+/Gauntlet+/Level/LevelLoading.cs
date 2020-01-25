﻿using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    public int LevelWidth { get; protected set; }
    public int LevelHeight { get; protected set; }
    string path;
    List<string> textLines;
    int width;


    public void LoadTiles(string path)
    {
        this.path = path;
        textLines = new List<string>();
        StreamReader fileReader = new StreamReader(path);
        string line = fileReader.ReadLine();
        width = line.Length;
        while (line != null)
        {
            textLines.Add(line);
            line = fileReader.ReadLine();
        }
        TileField background = new TileField(textLines.Count, width, 0, "floors/floor 1");
        TileField tiles = new TileField(textLines.Count, width, 1, "tiles");
        
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < textLines.Count; ++y)
            {
                Tile t = LoadTile(textLines[y][x], x, y);
                Tile b = LoadBasicTile("floors/floor 1", TileType.Background);
                tiles.Add(t, x, y);
                background.Add(b, x, y);
            }
        }
        Add(background);
        Add(tiles);

        LevelWidth = width * tiles.CellWidth;
        LevelHeight = (textLines.Count - 0) * tiles.CellHeight;
    }

    public void ReloadEnemies()
    {
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < textLines.Count; ++y)
            {
                Tile t = ReloadEnemy(textLines[y][x], x, y);
            }
        }
    }

    public Tile ReloadEnemy(char tileType, int x, int y)
    {
        switch (tileType)
        {

            case 'S':
                return LoadSkeleton(x, y);
            case 'g':
                return LoadGnome(x, y);
            default:
                return null;
        }

    }

    private Tile LoadTile(char tileType, int x, int y)
    {
        switch (tileType)
        {
            //Tiles

                //Vloer
            case '.':
                return LoadFloorTile(x, y);
            case '+':
                return LoadWallTile(x, y);
            case '/':
                return LoadBreakableWall(x, y);
            case '-':
                return LoadHorizontalDoor(x, y);
            case '|':
                return LoadVerticalDoor(x,y);
            case 'O':
                return LoadTeleport(x, y);
            case 'x':
                return LoadBasicTile("Trap", TileType.Trap, 1);
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                return LoadExitTile(tileType, x, y);

            //Enemies
            case 'S':
                return LoadSkeleton(x, y);
            case 'g':
                return LoadGnome(x, y);
//            case '':
//                return LoadTempleTroll(x, y);
//            case 'H':
//                return LoadTempleHellhound(x, y);
//            case 'w':
//                return LoadTempleWizard(x, y);
            case 'V':
                return LoadThyra(x, y);
            case 'W':
                return LoadWarrior(x, y);
            case 'E':
                return LoadElf(x, y);
            case 'M':
                return LoadMerlin(x, y);

                //Items
            case 'P':
                return LoadPotion(PotionType.Normal, x, y);
            case 'p':
                return LoadPotion(PotionType.Orange, x, y);
            case 'K':
                return LoadKey(x, y);
            case 'a':
                return LoadExtraPotion(x, y);
            case 't':
                return LoadTreasure(x, y);

            default:
                return LoadFloorTile(x, y);
        }        
    }


    private Tile LoadBasicTile(string name, TileType tileType, int layer = 0)
    {
        return new Tile("Tiles/" + name, tileType, layer);
    }

    private Tile LoadExitTile(int levelExit, int x, int y)
    {
        GameObjectList exits = Find("Exits") as GameObjectList;
        Vector2 position = new Vector2(x * Tile.Size, y * Tile.Size);
        Exit e = new Exit(layer, "Exit", position, levelExit-50);
        exits.Add(e);
        return e;
    } 

    private Tile LoadVerticalDoor(int x, int y)
    {
        GameObjectList doors = Find("Doors") as GameObjectList;
        Vector2 position = new Vector2(x * Tile.Size, y * Tile.Size);
        Door door = new Door(1, "VerticalDoor", position, TileType.VerticalDoor);
        doors.Add(door);
        return door;
    }

    private Tile LoadHorizontalDoor(int x, int y)
    {
        GameObjectList doors = Find("Doors") as GameObjectList;
        Vector2 position = new Vector2(x * Tile.Size, y * Tile.Size);
        Door door = new Door(1, "HorizontalDoor", position, TileType.HorizontalDoor);
        doors.Add(door);
        return door;
    }

    private Tile LoadBreakableWall(int x, int y)
    {
        GameObjectList breakwalls = Find("BreakableWalls") as GameObjectList;
        Vector2 position = new Vector2(x * Tile.Size, y * Tile.Size);
        BreakableWall breakWall = new BreakableWall(1, "BreakableWall", position);
        breakwalls.Add(breakWall);
        return breakWall;
    }

    private Tile LoadSkeleton(int x, int y)
    {
        GameObjectList enemies = Find("enemies") as GameObjectList;
        TileField tiles = Find("tiles") as TileField;
        Vector2 startPosition = new Vector2(((float)x + 0.5f) * Tile.Size, (y + 1) * Tile.Size);
        Ghost enemy = new Ghost(startPosition);
        //enemy.Position = new Vector2(((float)x + 0.5f) * tiles.CellWidth, (y + 1) * tiles.CellHeight + 25.0f);
        enemies.Add(enemy);
        return new Tile();
        /*        TileField tiles = Find("tiles") as TileField;
                Vector2 startPosition = new Vector2(((float)x + 0.5f) * 55, (y + 1) * 55);
                Ghost ghost = new Ghost(startPosition);
                Add(ghost);
        */
        //return new Tile("Tiles/background", TileType.Background);
    }
    
    private Tile LoadTeleport(int x, int y)
    {
        /*        GameObjectList teleports = Find("tiles") as GameObjectList;
                TileField tiles = Find("tiles") as TileField;
                Vector2 startPosition = new Vector2(((float)x + 0.5f) * 55, (y + 1) * 55);
                Teleport t = new Teleport(startPosition);
                teleports.Add(t);
                return new Tile(); */
        GameObjectList teleports = Find("teleport") as GameObjectList;
        //TileField tiles = Find("tiles") as TileField;
        Vector2 startPosition = new Vector2((float)x * Tile.Size, y * Tile.Size);
        Teleport teleport = new Teleport(startPosition);
        teleports.Add(teleport);
        return new Tile();
    }

    private Tile LoadGnome(int x, int y)
    {
        GameObjectList enemies = Find("enemies") as GameObjectList;
        TileField tiles = Find("tiles") as TileField;
        Vector2 startPosition = new Vector2(((float)x + 0.5f) * Tile.Size, (y + 1) * Tile.Size);
        Gnome enemy = new Gnome(startPosition);
        //enemy.Position = new Vector2(((float)x + 0.5f) * tiles.CellWidth, (y + 1) * tiles.CellHeight + 25.0f);
        enemies.Add(enemy);
        return new Tile();
        /*        TileField tiles = Find("tiles") as TileField;
                Vector2 startPosition = new Vector2(((float)x + 0.5f) * 55, (y + 1) * 55);
                Gnome gnome = new Gnome(startPosition);
                Add(gnome);
                return new Tile("Tiles/background", TileType.Background);
                */
    }

    private Tile LoadThyra(int x, int y)
    {
        startPositionThyra = new Vector2(x, y);  // set the startposition for this class
        return new Tile("Tiles/floors/floor 1", TileType.Background); //return background tile
    }

    private Tile LoadWarrior(int x, int y)
    {
        startPositionThor = new Vector2(x, y);
        return new Tile("Tiles/floors/floor 1", TileType.Background);
    }

    private Tile LoadElf(int x, int y)
    {
        startPositionQuestor = new Vector2(x, y);
        return new Tile("Tiles/floors/floor 1", TileType.Background);
    }

    private Tile LoadMerlin(int x, int y)
    { 
        startPositionMerlin = new Vector2(x, y);
        return new Tile("Tiles/floors/floor 1", TileType.Background);
    }

    private Tile LoadPotion(PotionType type, int x, int y)
    {
        GameObjectList Items = Find("potions") as GameObjectList;
        Vector2 position = new Vector2(x * Tile.Size, y * Tile.Size);
        Potion item = new Potion(type, 2, type.ToString() + "Potion", position);
        Items.Add(item);
        return new Tile("Tiles/floors/floor 1", TileType.Background);
    }

    private Tile LoadExtraPotion(int x, int y)
    {

        return new Tile("Tiles/floors/floor 1", TileType.Background);
    }

    private Tile LoadTreasure(int x, int y)
    {
        GameObjectList Items = Find("treasures") as GameObjectList;
        Vector2 position = new Vector2(x * Tile.Size, y * Tile.Size);
        Treasure item = new Treasure(2, "treasure", position);
        Items.Add(item);
        return new Tile("Tiles/floors/floor 1", TileType.Background);
    }

    private Tile LoadKey(int x, int y)
    {
        GameObjectList Items = Find("keys") as GameObjectList;
        Vector2 position = new Vector2(x * Tile.Size, y * Tile.Size);
        Key item = new Key(2, "Key", position);
        Items.Add(item);
        return new Tile("Tiles/floors/floor 1", TileType.Background);
    }
    private Tile LoadFood(int x, int y)
    {
        GameObjectList Items = Find("food") as GameObjectList;
        Vector2 position = new Vector2(x * Tile.Size, y * Tile.Size);
        Food item = new Food(2, "food", position);
        Items.Add(item);
        return new Tile("Tiles/floors/floor 1", TileType.Background);
    }

    private Tile LoadFloorTile(int x, int y)
    {
        string floor;
        int i = GameEnvironment.Random.Next(0, 75);
        switch (i)
        {
            case 0:
                floor = "floor 2";
                break;
            case 1:
                floor = "floor 3";
                break;
            case 2:
                floor = "floor 4";
                break;
            case 3:
                floor = "floor 5";
                break;
            case 4:
                floor = "floor 6";
                break;
            case 5:
                floor = "floor 7";
                break;
            case 6:
                floor = "floor 8";
                break;
            case 7:
                floor = "floor 1";
                break;
            default:
                floor = "floor 1";
                break;
        }
        return new Tile("Tiles/floors/" + floor, TileType.Background, 0, "floor");
    }

}
