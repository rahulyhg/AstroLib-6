namespace AstroLibV4
{
    class Helpers
    {
        /// <summary>
        /// Same as Math.Truncate
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int Fix(double input)
        {
            return (int)System.Math.Truncate(input);
        }

        /// <summary>
        /// Works like Math.Truncate, except if INPUT is negative, returns the first negative INT that is smaller or equal to INPUT. E. g. INPUT -4.2; OUTPUT -5.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int Int(double input)
        {
            int result;
            if (input < 0)
            {
                result = (int)System.Math.Truncate(input - 1);
            }
            else result = (int)System.Math.Truncate(input);

            return result;
        }

        /// <summary>
        /// Shortcut to Math.Cos(). Takes and returns radians.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double Cos(double input)
        {
            return System.Math.Cos(input);
        }

        /// <summary>
        /// Shortcut to Math.Sin(). Takes and returns radians.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double Sin(double input)
        {
            return System.Math.Sin(input);
        }

        /// <summary>
        /// Shortcut to Math.Asin(). Takes and returns radians.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double Asin(double input)
        {
            return System.Math.Asin(input);
        }

        /// <summary>
        /// Shortcut to Math.Tan(). Takes and returns radians.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double Tan(double input)
        {
            return System.Math.Tan(input);
        }

        /// <summary>
        /// Shortcut to Math.Atan(). Takes and returns radians.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double Atan(double input)
        {
            return System.Math.Atan(input);
        }

        /// <summary>
        /// Shortcut to Math.Abs(). Returns the absolute value (int) of a double.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double Abs(double input)
        {
            return System.Math.Abs(input);
        }

        /// <summary>
        /// Shortcut to Math.Atan2(). Takes and returns radians. Arguments switched.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public static double Atan2(double X, double Y)
        {

            return System.Math.Atan2(Y, X);

        }

        /// <summary>
        /// Shortcut to Math.Sqrt(). Returns square root of input.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double Sqrt(double input)
        {
            return System.Math.Sqrt(input);
        }

        /// <summary>
        /// Takes input and adds or subtracts until it is in range(rangeMin, rangeMax).
        /// </summary>
        /// <param name="input"></param>
        /// <param name="rangeMin"></param>
        /// <param name="rangeMax"></param>
        /// <returns></returns>
        public static double PutInRange(double input, int rangeMin, int rangeMax)
        {
            double result;
            result = input % rangeMax;
            if (result < rangeMin) result += rangeMax;
            else { }
            return result;
        }

        /// <summary>
        /// Overloaded. Takes input and adds or subtracts until it is in range(0, rangeMax).
        /// </summary>
        /// <param name="input"></param>
        /// <param name="rangeMax"></param>
        /// <returns></returns>
        public static double PutInRange(double input, int rangeMax)
        {
            double result;
            result = input % rangeMax;
            if (result < 0) result += rangeMax;
            else { }
            return result;
        }

        /// <summary>
        /// Real modulo-operation.
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        public static double RealModulo(double dividend, int divisor)
        {
            double result;
            result = dividend % divisor;
            if (result < 0) result += divisor;
            else { }
            return result;
        }

        /// <summary>
        /// Decimal Degrees to Decimal Radians
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double toRadians(double input)
        {
            return input * 0.01745329252d;
        }

        /// <summary>
        /// Decimal Radians to Decimal Degrees
        /// </summary>
        /// <param name="W"></param>
        /// <returns></returns>
        public static double toDegrees(double W)
        {
            return W * 57.29577951d;
        }

        /// <summary>
        /// Degree, Minute, Seconds to Decimal Degrees
        /// </summary>
        /// <param name="D"></param>
        /// <param name="M"></param>
        /// <param name="S"></param>
        /// <returns></returns>
        public static double toDecDeg(double D, double M, double S)
        {
            double A, B, C;
            double functionReturnValue = 0.0d;

            A = Abs(S) / 60.0d;
            B = (Abs(M) + A) / 60.0d;
            C = Abs(D) + B;

            if (((D < 0) | (M < 0) | (S < 0)))
            {
                functionReturnValue = -C;
            }
            else {
                functionReturnValue = C;
            }
            return functionReturnValue;

        }

        /// <summary>
        /// Hour, Minute, Second to Decimal Hour
        /// </summary>
        /// <param name="H"></param>
        /// <param name="M"></param>
        /// <param name="S"></param>
        /// <returns></returns>
        public static double toDecHour(double H, double M, double S)
        {
            double functionReturnValue = 0.0d;


            double A = Abs(S) / 60.0d;
            double B = (Abs(M) + A) / 60.0d;
            //double C = Abs(H) + B;
            double C = H + B;

            if (((H < 0) | (M < 0) | (S < 0)))
            {
                functionReturnValue = -C;
            }
            else {
                functionReturnValue = C;
            }
            return functionReturnValue;

        }

        public static double Unwind(double W)
        {
            return W - (2 * System.Math.PI) * Int(W / (2 * System.Math.PI));
        }

        public static double DMStoDD(double D, double M, double S)
        {
            double functionReturnValue = 0;

            double A = Abs(S) / 60;
            double B = (Abs(M) + A) / 60;
            double C = Abs(D) + B;

            if (((D < 0) | (M < 0) | (S < 0)))
            {
                functionReturnValue = -C;
            }
            else {
                functionReturnValue = C;
            }
            return functionReturnValue;

        }



    }
}
