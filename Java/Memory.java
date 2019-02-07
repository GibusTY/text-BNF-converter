//File name: Memory.java
//Created by Joshua Brock on 9/24/2018

import java.util.ArrayList;

public class Memory
{

	private static ArrayList<Character> id = new ArrayList<Character>();
	private static ArrayList<Integer> mem = new ArrayList<Integer>();
	
	public static int fetch (char ch)
	{
		//Returns the data for a given character. Throws an exception if the given data isn't found.
		assert(getIndex(ch) != -1);
		return mem.get(getIndex(ch));
	}
	
	public static void store (char ch, int value)
	{
		//If character Id isn't present, add it and its value to our list, else override the old value.
		if(getIndex(ch) == -1)
		{
			id.add(ch);
			mem.add(value);
		}
		else
		{
			mem.set(getIndex(ch), value);
		}
	}
	
	private static int getIndex (char ch)
	{
		//Returns the index for a given character. Throws an exception if the given data isn't found.
		for(int i = 0; i < id.size(); i++)
		{
			if(id.get(i) == ch){
				return i;
			}
		}
		return -1;
	}
	
	public static void displayMemory()
	{
		//Returns every Id ever stored here and their value at the time of displaying. (in order)
		for (int i = 0; i < id.size(); i++)
			System.out.println (id.get(i) + ": " + mem.get(i));
	}
}