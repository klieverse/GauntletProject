using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
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
            case '|':
                return LoadBasicTile("VerticleDoor", TileType.VerticalDoor, 1);
            case '-':
                return LoadBasicTile("HorizontalDoor", TileType.HorizontalDoor, 1);
            case 'O':
                return LoadBasicTile("Teleport", TileType.Teleporter, 1);
            case 'x':
                return LoadBasicTile("Trap", TileType.Trap, 1);

                //Players
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
            case 'k':
                return LoadExtraPotion(x, y);


            //Enemies (to be added)

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

    private Tile LoadKey(int x, int y)
    {

        return new Tile("Tiles/background", TileType.Background);
    }
}
