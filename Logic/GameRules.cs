namespace Logic;

public class GameRules
{
	private int survivalMin = 2;
	private int survivalMax = 3;
	private int birthCondition = 3;

	public int SurvivalMin
	{
		get => survivalMin;
		set => survivalMin = value;
	}

	public int SurvivalMax
	{
		get => survivalMax;
		set => survivalMax = value;
	}

	public int BirthCondition
	{
		get => birthCondition;
		set => birthCondition = value;
	}

	public void SetSurvivalRange(int min, int max)
	{
		if (min < 0 || max < 0 || min > max || max > 8)
		{
			throw new ArgumentException("Invalid survival range. Min and max must be between 0 and 8, and min must be less than or equal to max.");
		}
		survivalMin = min;
		survivalMax = max;
	}

	public void SetBirthCondition(int condition)
	{
		if (condition < 0 || condition > 8)
		{
			throw new ArgumentException("Invalid birth condition. Condition must be between 0 and 8.");
		}
		birthCondition = condition;
	}

	public bool ShouldLive(bool isCurrentlyAlive, int liveNeighbors)
	{
		if (isCurrentlyAlive)
		{
			return liveNeighbors >= survivalMin && liveNeighbors <= survivalMax;
		}
		else
		{
			return liveNeighbors == birthCondition;
		}
	}
}