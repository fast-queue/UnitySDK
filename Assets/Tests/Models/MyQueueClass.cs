using System;

public class MyQueueClass : FQ.BaseBody
{
	public MyQueueClass (string name, int ranking)
	{
		this.name = name;
		this.ranking = ranking;
	}
	public string name;
	public int ranking;

}


