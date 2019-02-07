//File name: Id.java
//Created by Joshua Brock on 9/24/2018

public class Id implements ArithmeticExpression
{

	private char ch;
	
	public Id(char ch)
	{
		//Postcondition: Creates a new ID using the character provided.
		this.ch = ch;
	}

	@Override
	public int evaluate()
	{
		//Returns the value associated with said Id
		return Memory.fetch (ch);
	}
	
	public char getChar ()
	{
		//Returns the character for said Id
		return ch;
	}
}