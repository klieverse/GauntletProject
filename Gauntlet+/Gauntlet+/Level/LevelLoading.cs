﻿using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    public int LevelWidth { get; protected set; }
    public int LevelHeight { get; protected set; }



    public void LoadTiles(string path)
    {
        List<string> textLines = new List<string>();
        StreamReader fileReader = new StreamReader(path);
        string line = fileReader.ReadLine();
        int width = line.Length;
        while (line != null)
        {
            textLines.Add(line);
            line = fileReader.ReadLine();
        }
        TileField background = new TileField(textLines.Count, width, 0, "background");
        TileField tiles = new TileField(textLines.Count, width, 1, "tiles");
        
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < textLines.Count; ++y)
            {
                Tile t = LoadTile(textLines[y][x], x, y);
                Tile b = LoadBasicTile("Background", TileType.Background);
                tiles.Add(t, x, y);
                background.Add(b, x, y);
            }
        }
        Add(background);
        Add(tiles);

        LevelWidth = width * tiles.CellWidth;
        LevelHeight = (textLines.Count - 0) * tiles.CellHeight;
    }

    private Tile LoadTile(char tileType, int x, int y)
    {
        switch (tileType)
        {
                //Tiles
            case '.':
                return LoadBasicTile("Background", TileType.Background);
            case '+':
                return LoadBasicTile("Wall", TileType.Wall, 1);
            case '/':
               return LoadBasicTile("BreakableWall", TileType.BreakableWall, 1);
            case '-':
                return LoadBasicTile("HorizontalDoor", TileType.HorizontalDoor, 1);
            case '|':
                return LoadBasicTile("VerticalDoor", TileType.VerticalDoor, 1);
            case 'O':
                return LoadBasicTile("Teleport", TileType.Teleporter, 1);
            case 'x':
                return LoadBasicTile("Trap", TileType.Trap, 1);
            
                //Enemies
            case 'S':
                return LoadSkeleton(x, y);
            case 'G':
                return LoadGnome(x, y);
//            case 'T':
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
                return LoadPotion(Color.Blue, x, y);
            case 'p':
                return LoadPotion(Color.Orange, x, y);
            case 'K':
                return LoadKey(x, y);
            case 'a':
                return LoadExtraPotion(x, y);
            case 't':
                return LoadTreasure(x, y);

            default:
                int t = (int)tileType;
                return LoadExitTile("Exit", TileType.Exit, t);
            }        
    }

    private Tile LoadBasicTile(string name, TileType tileType, int layer = 0)
    {
        return new Tile("Tiles/" + name, tileType, layer);
    }

    private Tile LoadExitTile(string name, TileType tileType, int levelExit, int layer = 1)
    {
        //nog een lijstje met de integers

        return new Tile("Tiles/" + name, tileType, layer);
    } 
    

    private Tile LoadSkeleton(int x, int y)
    {
        GameObjectList enemies = Find("enemies") as GameObjectList;
        TileField tiles = Find("tiles") as TileField;
        Vector2 startPosition = new Vector2(((float)x + 0.5f) * 55, (y + 1) * 55);
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

    private Tile LoadGnome(int x, int y)
    {
        GameObjectList enemies = Find("enemies") as GameObjectList;
        TileField tiles = Find("tiles") as TileField;
        Vector2 startPosition = new Vector2(((float)x + 0.5f) * 55, (y + 1) * 55);
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
        return new Tile("Tiles/background", TileType.Background);
    }

    private Tile LoadWarrior(int x, int y)
    {

        return new Tile("Tiles/background", TileType.Background);
    }

    private Tile LoadElf(int x, int y)
    {

        return new Tile("Tiles/background", TileType.Background);
    }

    private Tile LoadMerlin(int x, int y)
    {

        return new Tile("Tiles/background", TileType.Background);
    }

    private Tile LoadPotion(Color color, int x, int y)
    {

        return new Tile("Tiles/background", TileType.Background);
    }

    private Tile LoadExtraPotion(int x, int y)
    {

        return new Tile("Tiles/background", TileType.Background);
    }

    private Tile LoadTreasure(int x, int y)
    {

        return new Tile("Tiles/background", TileType.Background);
    }

    private Tile LoadKey(int x, int y)
    {

        return new Tile("Tiles/background", TileType.Background);
    }
}
