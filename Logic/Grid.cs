namespace Logic;

public class Grid
{
	private Cell[,] cells;
	private int rows;
	private int cols;

	public int Rows => rows;
	public int Cols => cols;

	public Grid(int rows, int cols)
	{
		this.rows = rows;
		this.cols = cols;
		cells = new Cell[rows, cols];

		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				cells[r, c] = new Cell();
			}
		}
	}

	public bool IsInBounds(int row, int col)
	{
		return row >= 0 && row < rows && col >= 0 && col < cols;
	}

	public Cell GetCell(int row, int col)
	{
		if (!IsInBounds(row, col))
		{
			throw new ArgumentOutOfRangeException($"{nameof(row)}, {nameof(col)}", "Cell position is out of bounds.");
		}
			
		return cells[row, col];
	}

	public int CountLiveNeighbors(int row, int col)
	{
		int liveNeighbors = 0;

		for (int r = row - 1; r <= row + 1; r++)
		{
			for (int c = col - 1; c <= col + 1; c++)
			{
				if(IsInBounds(r, c) && !(r == row && c == col) && cells[r, c].IsAlive)
				{
					liveNeighbors++;
				}
			}
		}
		return liveNeighbors;
	}
}