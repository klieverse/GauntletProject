using System.Collections.Generic;
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

    private Tile LoadTile(char tileType, int x, int y)
    {
        switch (tileType)
        {
            //Tiles
            case '.':
                return LoadBasicTile("floors/floor 1", TileType.Background);
            case '+':
                return LoadBasicTile("Walls/Good Wall 1", TileType.Wall, 1);
            case '/':
               return LoadBasicTile("BreakableWall", TileType.BreakableWall, 1);
            case '-':
                return LoadBasicTile("HorizontalDoor", TileType.HorizontalDoor, 1);
            case '|':
                return LoadBasicTile("VerticalDoor", TileType.VerticalDoor, 1);
            case 'O':
                return LoadTeleport(x, y);
            case 'x':
                return LoadBasicTile("Trap", TileType.Trap, 1);
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

    private Tile LoadExitTile(string name, TileType tileType, int levelExit)
    {
        //nog een lijstje met de integers

        return new Tile("Tiles/" + name, tileType);
    }

    private Tile LoadSkeleton(int x, int y)
    {
        GameObjectList enemies = Find("enemies") as GameObjectList;
        TileField tiles = Find("tiles") as TileField;
        Vector2 startPosition = new Vector2(((float)x + 0.5f) * 64, (y + 1) * 64);
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
        Vector2 startPosition = new Vector2(((float)x + 0.5f) * 64, (y + 1) * 64);
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

    private Tile LoadPotion(Color color, int x, int y)
    {

        return new Tile("Tiles/floors/floor 1", TileType.Background);
    }

    private Tile LoadExtraPotion(int x, int y)
    {

        return new Tile("Tiles/floors/floor 1", TileType.Background);
    }

    private Tile LoadTreasure(int x, int y)
    {

        return new Tile("Tiles/floors/floor 1", TileType.Background);
    }

    private Tile LoadKey(int x, int y)
    {

        return new Tile("Tiles/floors/floor 1", TileType.Background);
    }
}
