using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.Content;

public class Grid
{
    Texture2D _texture;
    int _width;
    int _height;
    int _rows;
    int _columns;
    Vector2[,] _grid;
    public Grid(Texture2D texture, int width, int height, int rows, int columns)
    {
        _texture = texture;
        _width = width;
        _height = height;
        _rows = rows;
        _columns = columns;
        CreateGrid();
    }
    void CreateGrid()
    {
        _grid = new Vector2[_rows, _columns];
        for (int row = 0; row < _rows; row++)
        {
            for(int column = 0; column < _columns; column++)
            {
                _grid[row, column] = new Vector2(_width * column, _height * row);
            }
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        for (int row = 0; row < _rows; row++)
        {
            for(int column = 0; column < _columns; column++)
            {
                spriteBatch.Draw(_texture, _grid[row, column], Color.White);
            }
        }
    }
}
