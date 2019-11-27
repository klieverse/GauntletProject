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
        TileField tiles = new TileField(textLines.Count - 1, width, 1, "tiles");
        
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < textLines.Count - 1; ++y)
            {
                Tile t = LoadTile(textLines[y][x], x, y);
                tiles.Add(t, x, y);
            }
        }
    }

    private Tile LoadTile(char tileType, int x, int y)
    {
        return new Tile();
        //switch (tileType)
        //{
            
        //}
    }

    /*private Tile LoadBasicTile()
    {
        Tile t = new Tile("Tiles/" + name, tileType);
        
    }*/
}
