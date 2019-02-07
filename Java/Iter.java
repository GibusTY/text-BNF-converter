//File name: Iter.java
//Created by Joshua Brock on 9/26/2018

public class Iter {

    private ArithmeticExpression arithbegin, arithend;

    public Iter(ArithmeticExpression arithbegin, ArithmeticExpression arithend)
    {
		//Default constructor
		assert(arithbegin != null && arithend != null);
        this.arithbegin = arithbegin;
        this.arithend = arithend;
    }

    public ArithmeticExpression getBegin(){
		//Returns our beginning value
        return arithbegin;
    }

    public ArithmeticExpression getEnd(){
		//Returns our end value
        return arithend;
    }
}
