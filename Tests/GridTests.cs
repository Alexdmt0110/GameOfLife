namespace Tests;
using Logic;

public class GridTests
{
	[Fact]
	public void DeadCellWithThreeLiveNeighbors_ShouldBecomeAlive()
	{
		GameRules rules = new GameRules();
		Grid grid = new Grid(3, 3, rules);

		grid.GetCell(0, 1).IsAlive = true;
		grid.GetCell(1, 0).IsAlive = true;
		grid.GetCell(1, 2).IsAlive = true;

		Assert.False(grid.GetCell(1, 1).IsAlive);

		grid.NextGeneration();

		Assert.True(grid.GetCell(1, 1).IsAlive);
	}

	[Fact]
	public void LiveCellWithTwoOrThreeNeighbors_ShouldSurvive()
	{
		GameRules rules = new GameRules();
		Grid grid = new Grid(3, 3, rules);

		grid.GetCell(1, 1).IsAlive = true;

		grid.GetCell(0, 1).IsAlive = true;
		grid.GetCell(1, 0).IsAlive = true;

		grid.NextGeneration();

		Assert.True(grid.GetCell(1, 1).IsAlive); 

		grid = new Grid(3, 3, rules);
		grid.GetCell(1, 1).IsAlive = true;
		grid.GetCell(0, 1).IsAlive = true;
		grid.GetCell(1, 0).IsAlive = true;
		grid.GetCell(1, 2).IsAlive = true;

		grid.NextGeneration();

		Assert.True(grid.GetCell(1, 1).IsAlive); 
	}


	[Fact]
	public void LiveCellWithOneOrFourNeighbors_ShouldDie()
	{
		GameRules rules = new GameRules();
		Grid grid = new Grid(3, 3, rules);

		grid.GetCell(1, 1).IsAlive = true;

		grid.GetCell(0, 1).IsAlive = true;

		grid.NextGeneration();

		Assert.True(!(grid.GetCell(1, 1).IsAlive)); 

		grid = new Grid(3, 3, rules);
		grid.GetCell(1, 1).IsAlive = true;
		grid.GetCell(0, 1).IsAlive = true;
		grid.GetCell(1, 0).IsAlive = true;
		grid.GetCell(1, 2).IsAlive = true;
		grid.GetCell(0,2).IsAlive = true;

		grid.NextGeneration();

		Assert.True(!(grid.GetCell(1, 1).IsAlive)); 
	}

	[Fact]
	public void DeadCellWithNonThreeNeighbors_ShouldStayDead()
	{
		GameRules rules = new GameRules();
		Grid grid = new Grid(3, 3, rules);

		grid.GetCell(1, 1).IsAlive = false;
		grid.GetCell(0, 1).IsAlive = true;

		grid.NextGeneration();

		Assert.True(!(grid.GetCell(1, 1).IsAlive));

		grid = new Grid(3, 3, rules);
		grid.GetCell(1, 1).IsAlive = false;
		grid.GetCell(0, 1).IsAlive = true;
		grid.GetCell(1, 2).IsAlive = true;
		grid.GetCell(0, 0).IsAlive = true;
		grid.GetCell(2, 1).IsAlive = true;

		Assert.False(grid.GetCell(1, 1).IsAlive);
	}

	[Fact]
	public void IsInBounds_ShouldReturnTrue_ForValidCoordinates()
	{
		var grid = new Grid(3, 3, new GameRules());

		Assert.True(grid.IsInBounds(0, 0));
		Assert.True(grid.IsInBounds(2, 2));
		Assert.True(grid.IsInBounds(1, 1));
	}

	[Fact]
	public void IsInBounds_ShouldReturnFalse_ForOutOfBoundsCoordinates()
	{
		var grid = new Grid(3, 3, new GameRules());

		Assert.False(grid.IsInBounds(-1, 0));
		Assert.False(grid.IsInBounds(0, -1));
		Assert.False(grid.IsInBounds(3, 0));
		Assert.False(grid.IsInBounds(0, 3)); 
		Assert.False(grid.IsInBounds(3, 3)); 
	}
}