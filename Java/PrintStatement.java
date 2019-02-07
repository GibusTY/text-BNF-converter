//File name: PrintStatement.java
//Created by Joshua Brock on 9/26/2018

public class PrintStatement implements Statemnt
{
    private ArithmeticExpression arithmeticExpression;

    public PrintStatement(ArithmeticExpression arithmeticExpression)
    {
        //default constructor
		this.arithmeticExpression = arithmeticExpression;
    }

	@Override
    public void execute()
    {
		//Prints an output of its ArithmeticExpression's value, if any.
        if(arithmeticExpression == null)
            System.out.println();
        else
            System.out.println(arithmeticExpression.evaluate());
    }
}
