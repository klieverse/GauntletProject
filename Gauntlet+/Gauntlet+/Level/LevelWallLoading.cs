using System;
using System.Collections.Generic;
using System.IO;

partial class Level : GameObjectList
    {

    private Tile LoadWallTile(int x, int y) // selects the correct wall tile depending on walls arround it;
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
        string assetName = "";

        char aboveLeft;
        char above;
        char aboveRight;
        char right;
        char downRight;
        char down;
        char downLeft;
        char left;

        //set Chars
        if (x > 0)
        {
            left = textLines[y][x - 1];
        }
        else left = '-';

        if (y > 0)
        {
            above = textLines[y - 1][x];
        }
        else above = '-';

        if (x < width - 1)
        {
            right = textLines[y][x + 1];
        }
        else right = '-';

        if (y < textLines.Count - 1)
        {
            down = textLines[y + 1][x];
        }
        else down = '-';

        if (x > 0 && y > 0)
        {
            aboveLeft = textLines[y - 1][x - 1];
        }
        else aboveLeft = '-';

        if (x < width - 1 && y < textLines.Count - 1)
        {
            downRight = textLines[y + 1][x + 1];
        }
        else downRight = '-';

        if (x > 0 && y < textLines.Count - 1)
        {
            downLeft = textLines[y + 1][x - 1];
        }
        else downLeft = '-';

        if (x < width - 1 && y > 0)
        {
            aboveRight = textLines[y - 1][x + 1];
        }
        else aboveRight = '-';

        // set assetName
        if (above != '+' && right == '+' && down != '+' && left != '+')
        {
            assetName = "Tiles/Walls/good wall 2"; //
        }
        else if (above != '+' && right == '+' && down != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 3"; ///
        }
        else if (aboveLeft != '+' && above == '+' && aboveRight != '+' && right == '+' && downRight != '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 4"; ////
        }
        else if (aboveLeft != '+' && above == '+' && aboveRight != '+' && right == '+' && downRight == '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 5"; /////
        }
        else if (aboveLeft != '+' && above == '+' && aboveRight != '+' && right == '+' && downRight != '+' && down == '+' && downLeft == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 6"; //////
        }
        else if (aboveLeft == '+' && above == '+' && aboveRight != '+' && right == '+' && downRight != '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 7"; ///////
        }
        else if (aboveLeft != '+' && above == '+' && aboveRight == '+' && right == '+' && downRight != '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 8"; ////////
        }
        else if (aboveLeft == '+' && above == '+' && aboveRight == '+' && right == '+' && downRight == '+' && down == '+' && downLeft == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 9"; /////////
        }
        else if (above == '+' && aboveRight == '+' && right == '+' && down != '+' && left != '+')
        {
            assetName = "Tiles/Walls/good wall 10"; //////////
        }
        else if (aboveLeft == '+' && above == '+' && right != '+' && down != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 11"; ///////////
        }
        else if (above != '+' && right != '+' && down == '+' && downLeft == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 12"; ////////////
        }
        else if (above != '+' && right == '+' && downRight == '+' && down == '+' && left != '+')
        {
            assetName = "Tiles/Walls/good wall 13"; /////////////
        }
        else if (aboveLeft == '+' && above == '+' && aboveRight == '+' && right == '+' && down != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 14"; //////////////
        }
        else if (above != '+' && right == '+' && downRight == '+' && down == '+' && downLeft == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 15"; ///////////////
        }
        else if (aboveLeft == '+' && above == '+' && right != '+' && downLeft == '+' && down == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 16"; ////////////////
        }
        else if (above == '+' && aboveRight == '+' && right == '+' && downRight == '+' && down == '+' && left != '+')
        {
            assetName = "Tiles/Walls/good wall 17"; /////////////////
        }
        else if (aboveLeft != '+' && above == '+' && aboveRight != '+' && right == '+' && downRight == '+' && down == '+' && downLeft == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 18"; //////////////////
        }
        else if (aboveLeft != '+' && above == '+' && aboveRight == '+' && right == '+' && downRight == '+' && down == '+' && downLeft == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 19"; ///////////////////
        }
        else if (aboveLeft == '+' && above == '+' && aboveRight != '+' && right == '+' && downRight == '+' && down == '+' && downLeft == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 20"; ////////////////////
        }
        else if (aboveLeft != '+' && above == '+' && aboveRight == '+' && right == '+' && downRight == '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 21"; /////////////////////
        }
        else if (aboveLeft == '+' && above == '+' && aboveRight == '+' && right == '+' && downRight == '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 22"; //////////////////////
        }
        else if (aboveLeft == '+' && above == '+' && aboveRight != '+' && right == '+' && downRight != '+' && down == '+' && downLeft == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 23"; ///////////////////////
        }
        else if (aboveLeft == '+' && above == '+' && aboveRight == '+' && right == '+' && downRight != '+' && down == '+' && downLeft == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 24"; ////////////////////////
        }
        else if (aboveLeft == '+' && above == '+' && aboveRight == '+' && right == '+' && downRight != '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 25"; /////////////////////////
        }
        else if (aboveLeft != '+' && above == '+' && aboveRight == '+' && right == '+' && downRight != '+' && down == '+' && downLeft == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 26"; //////////////////////////
        }
        else if (aboveLeft == '+' && above == '+' && aboveRight != '+' && right == '+' && downRight == '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 27"; ///////////////////////////
        }
        else if (above == '+' && right != '+' && down == '+' && left != '+')
        {
            assetName = "Tiles/Walls/good wall 28"; ////////////////////////////
        }
        else if (above == '+' && right != '+' && down != '+' && left != '+')
        {
            assetName = "Tiles/Walls/good wall 29"; /////////////////////////////
        }
        else if (above != '+' && right != '+' && down == '+' && left != '+')
        {
            assetName = "Tiles/Walls/good wall 30"; //////////////////////////////
        }
        else if (above != '+' && right != '+' && down != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 31"; ///////////////////////////////
        }
        else if (above == '+' && aboveRight != '+' && right == '+' && downRight != '+' && down == '+' && left != '+')
        {
            assetName = "Tiles/Walls/good wall 32"; ////////////////////////////////
        }
        else if (above != '+' && right == '+' && downRight != '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 33"; /////////////////////////////////
        }
        else if (aboveLeft != '+' && above == '+' && right != '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 34"; //////////////////////////////////
        }
        else if (aboveLeft != '+' && above == '+' && aboveRight != '+' && right == '+' && down != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 35"; ///////////////////////////////////
        }
        else if (aboveLeft != '+' && above == '+' && right != '+' && down != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 36"; ////////////////////////////////////
        }
        else if (above == '+' && aboveRight != '+' && right == '+' && down != '+' && left != '+')
        {
            assetName = "Tiles/Walls/good wall 37"; /////////////////////////////////////
        }
        else if (above != '+' && right == '+' && downRight != '+' && down == '+' && left != '+')
        {
            assetName = "Tiles/Walls/good wall 38"; //////////////////////////////////////
        }
        else if (above != '+' && right != '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 39"; ///////////////////////////////////////
        }
        else if (aboveLeft == '+' && above == '+' && aboveRight != '+' && right == '+' && down != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 40"; ////////////////////////////////////////
        }
        else if (aboveLeft != '+' && above == '+' && aboveRight == '+' && right == '+' && down != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 41"; /////////////////////////////////////////
        }
        else if (above == '+' && aboveRight == '+' && right == '+' && downRight != '+' && down == '+' && left != '+')
        {
            assetName = "Tiles/Walls/good wall 42"; //////////////////////////////////////////
        }
        else if (above == '+' && aboveRight != '+' && right == '+' && downRight == '+' && down == '+' && left != '+')
        {
            assetName = "Tiles/Walls/good wall 43"; ///////////////////////////////////////////
        }
        else if (above != '+' && right == '+' && downRight == '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 44"; ////////////////////////////////////////////
        }
        else if (above != '+' && right == '+' && downRight != '+' && down == '+' && downLeft == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 45"; /////////////////////////////////////////////
        }
        else if (aboveLeft != '+' && above == '+' && right != '+' && down == '+' && downLeft == '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 46"; //////////////////////////////////////////////
        }
        else if (aboveLeft == '+' && above == '+' && right != '+' && down == '+' && downLeft != '+' && left == '+')
        {
            assetName = "Tiles/Walls/good wall 47"; ///////////////////////////////////////////////
        }
        else assetName = "Tiles/Walls/good wall 1";

        return new Tile(assetName, TileType.Wall, 0, "Wall");
    }
}

