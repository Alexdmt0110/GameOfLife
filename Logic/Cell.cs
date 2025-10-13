namespace Logic;

public class Cell
{
	private bool isAlive;

	public bool IsAlive
	{
		get => isAlive;
		set => isAlive = value;
	}

	public Cell(bool isAlive = false)
	{
		this.isAlive = isAlive;
	}
}