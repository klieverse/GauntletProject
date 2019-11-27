using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObjectGrid : GameObject
{
    protected GameObject[,] grid;
    protected const int cellWidth = 16, cellHeight = 16;

    public GameObjectGrid(int rows = 64, int columns = 64, int layer = 0, string id = "")
        : base(layer, id)
    {
        grid = new GameObject[columns, rows];
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                grid[x, y] = null;
            }
        }
    }

    public void Add(GameObject obj, int x, int y)
    {
        grid[x, y] = obj;
        obj.Position = new Vector2(x * cellWidth, y * cellHeight);
    }

    public GameObject Get(int x, int y)
    {
        if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
        {
            return grid[x, y];
        }
        else
        {
            return null;
        }
    }

    public GameObject[,] Objects
    {
        get
        {
            return grid;
        }
    }

    public int Rows
    {
        get { return grid.GetLength(1); }
    }

    public int Columns
    {
        get { return grid.GetLength(0); }
    }

    public int CellWidth
    {
        get { return cellWidth; }
    }

    public int CellHeight
    {
        get { return cellHeight; }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        foreach (GameObject obj in grid)
        {
            obj.HandleInput(inputHelper);
        }
    }

    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in grid)
        {
            obj.Update(gameTime);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (GameObject obj in grid)
        {
            obj.Draw(gameTime, spriteBatch);
        }
    }

    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in grid)
        {
            obj.Reset();
        }
    }
}
