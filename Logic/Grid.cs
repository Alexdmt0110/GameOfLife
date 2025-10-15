namespace Logic;

public class Grid
{
	private Cell[,] cells;
	private int rows;
	private int cols;
	private GameRules gameRules;

	public int Rows => rows;
	public int Cols => cols;

	public Grid(int rows, int cols, GameRules gameRules)
	{
		this.rows = rows;
		this.cols = cols;
		cells = new Cell[rows, cols];
		this.gameRules = gameRules;

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

	public void NextGeneration()
	{
		bool[,] nextGenState = new bool[rows, cols]; 
		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				Cell currentCell = GetCell(r, c);
				int liveNeighbors = CountLiveNeighbors(r, c);
				nextGenState[r, c] = gameRules.ShouldLive(currentCell.IsAlive, liveNeighbors);
			}
		}

		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				cells[r, c].IsAlive = nextGenState[r, c];
			}
		}
	}

	public void Clear()
	{
		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				cells[r, c].IsAlive = false;
			}
		}
	}
}