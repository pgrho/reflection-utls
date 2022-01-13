namespace Shipwreck.ReflectionUtils;

public static class ExpressionTypeHelper
{
    public static bool IsAlwaysMet(this ExpressionType testingComparison, ExpressionType foundBinaryType, int compareToOperand)
    {
        switch (testingComparison)
        {
            case ExpressionType.Equal:
                return foundBinaryType == ExpressionType.Equal && compareToOperand == 0;

            case ExpressionType.NotEqual:
                return foundBinaryType == ExpressionType.NotEqual && compareToOperand == 0
                    || foundBinaryType == ExpressionType.Equal && compareToOperand != 0
                    || foundBinaryType == ExpressionType.GreaterThan && compareToOperand >= 0
                    || foundBinaryType == ExpressionType.GreaterThanOrEqual && compareToOperand > 0
                    || foundBinaryType == ExpressionType.LessThan && compareToOperand <= 0
                    || foundBinaryType == ExpressionType.LessThanOrEqual && compareToOperand < 0;

            case ExpressionType.LessThan:
                return foundBinaryType == ExpressionType.Equal && compareToOperand < 0
                    || foundBinaryType == ExpressionType.LessThan && compareToOperand <= 0
                    || foundBinaryType == ExpressionType.LessThanOrEqual && compareToOperand < 0;

            case ExpressionType.LessThanOrEqual:
                return foundBinaryType == ExpressionType.Equal && compareToOperand <= 0
                    || foundBinaryType == ExpressionType.LessThan && compareToOperand <= 0
                    || foundBinaryType == ExpressionType.LessThanOrEqual && compareToOperand <= 0;

            case ExpressionType.GreaterThan:
                return foundBinaryType == ExpressionType.Equal && compareToOperand > 0
                    || foundBinaryType == ExpressionType.GreaterThan && compareToOperand >= 0
                    || foundBinaryType == ExpressionType.GreaterThanOrEqual && compareToOperand > 0;

            case ExpressionType.GreaterThanOrEqual:
                return foundBinaryType == ExpressionType.Equal && compareToOperand >= 0
                    || foundBinaryType == ExpressionType.GreaterThan && compareToOperand >= 0
                    || foundBinaryType == ExpressionType.GreaterThanOrEqual && compareToOperand >= 0;

            default:
                return false;
        }
    }
}
