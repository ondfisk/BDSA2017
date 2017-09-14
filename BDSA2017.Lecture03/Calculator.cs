namespace BDSA2017.Lecture03
{
    public class Calculator
    {
        public static int Add(int x, int y)
        {
            checked
            {
                y += x;
            }

            return y;
        }

        public static int Negate(int x) => -x;
    }
}
