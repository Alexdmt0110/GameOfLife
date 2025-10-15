namespace Tests;
using Logic;

public class GameRulesTests
{
	[Fact]
	public void CellShouldLive()
	{
		GameRules gameRules = new GameRules();
		gameRules.SetSurvivalRange(2, 3);
		var isAlive1 = gameRules.ShouldLive(true, 3);
		var isAlive2 = gameRules.ShouldLive(true, 7);
		Assert.True(isAlive1);
		Assert.False(isAlive2);
	}

	[Fact]
	public void CellShouldBeBorn()
	{
		GameRules gameRules = new GameRules();
		gameRules.SetBirthCondition(3);
		var isAlive1 = gameRules.ShouldLive(false, 3);
		var isAlive2 = gameRules.ShouldLive(false, 2);
		Assert.True(isAlive1);
		Assert.False(isAlive2);
	}

	[Fact]
	public void AliveCellSurvivesAtMinAndMax()
	{
		GameRules gameRules = new GameRules();
		gameRules.SetSurvivalRange(2, 3);

		Assert.True(gameRules.ShouldLive(true, 2));
		Assert.True(gameRules.ShouldLive(true, 3)); 
	}

	[Fact]
	public void AliveCellDiesWithTooFewOrTooManyNeighbors()
	{
		GameRules gameRules = new GameRules();
		gameRules.SetSurvivalRange(2, 3);

		Assert.False(gameRules.ShouldLive(true, 1));
		Assert.False(gameRules.ShouldLive(true, 4));
	}

	[Fact]
	public void DeadCellBornOnlyWithExactBirthCondition()
	{
		GameRules gameRules = new GameRules();
		gameRules.SetBirthCondition(3);

		for (int i = 0; i <= 8; i++)
		{
			bool expected = (i == 3);
			Assert.Equal(expected, gameRules.ShouldLive(false, i));
		}
	}
	
	[Theory]
	[InlineData(-1, 3)]
	[InlineData(3, 9)]
	[InlineData(4, 2)]
	public void SetSurvivalRange_InvalidValues_Throws(int min, int max)
	{
		GameRules gameRules = new GameRules();

		Assert.Throws<ArgumentException>(() => gameRules.SetSurvivalRange(min, max));
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(9)]
	public void SetBirthCondition_InvalidValue_Throws(int invalidCondition)
	{
		GameRules gameRules = new GameRules();

		Assert.Throws<ArgumentException>(() => gameRules.SetBirthCondition(invalidCondition));
	}


}