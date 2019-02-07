//File name: Interpreter.java
//Created by Joshua Brock on 9/24/2018

import java.io.FileNotFoundException;


public class Interpreter
{
	public static void main(String[] args)
	{
		//Essentially our main method to perform everything else already coded.
		try
		{
			Parser p = new Parser("C:\\Users\\Gibus the Wat\\IdeaProjects\\JuliaToJava\\src\\expr4.txt");
			Program program = p.parse();
			program.execute();
			Memory.displayMemory();
		}
		catch (FileNotFoundException e)
		{
			e.printStackTrace();
		}
	}
}