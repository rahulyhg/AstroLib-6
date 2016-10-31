namespace AstroLibV4
{
    class Time
    {
        /// <summary>
        /// GCD (Day, Month, Year) to Julian Date
        /// </summary>
        /// <param name="Day"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public static double GCD_JD(double Day, double Month, double Year)
        {
            double Y, M, A, B, C, D;

            if ((Month < 3))
            {
                Y = Year - 1;
                M = Month + 12;
            }
            else {
                Y = Year;
                M = Month;
            }

            if ((Year > 1582))
            {
                A = Helpers.Fix(Y / 100);
                B = 2 - A + Helpers.Fix(A / 4);
            }
            else {
                if ((Year == 1582) & (Month > 10))
                {
                    A = Helpers.Fix(Y / 100);
                    B = 2 - A + Helpers.Fix(A / 4);
                }
                else {
                    if ((Year == 1582) & (Month == 10) & (Day >= 15))
                    {
                        A = Helpers.Fix(Y / 100);
                        B = 2 - A + Helpers.Fix(A / 4);
                    }
                    else {
                        B = 0;
                    }
                }
            }

            if ((Y < 0))
            {
                C = Helpers.Fix((365.25 * Y) - 0.75);
            }
            else {
                C = Helpers.Fix(365.25 * Y);
            }

            D = Helpers.Fix(30.6001 * (M + 1));
            return B + C + D + Day + 1720994.5;

        }

        /// <summary>
        /// Julian Date to GC Day
        /// </summary>
        /// <param name="JD"></param>
        /// <returns></returns>
        /// <param name="JD"></param>
        /// <returns></returns>
        public static double JDtoGCDay(double JD)
        {

            double I = Helpers.Fix(JD + 0.5d);
            double F = JD + 0.5d - I;
            double A = Helpers.Fix((I - 1867216.25d) / 36524.25d);
            double B;

            if ((I > 2299160))
            {
                B = I + 1 + A - Helpers.Fix(A / 4);
            }
            else {
                B = I;
            }

            double C = B + 1524;
            double D = Helpers.Fix((C - 122.1d) / 365.25d);
            double E = Helpers.Fix(365.25d * D);
            double G = Helpers.Fix((C - E) / 30.6001d);
            return C - E + F - Helpers.Fix(30.6001d * G);

        }

        /// <summary>
        /// Julian Date to GC Month
        /// </summary>
        /// <param name="JD"></param>
        /// <returns></returns>
        /// <param name="JD"></param>
        /// <returns></returns>
        public static double JDtoGCMonth(double JD)
        {
            double functionReturnValue = 0;

            double I = Helpers.Fix(JD + 0.5);
            double F = JD + 0.5 - I;
            double A = Helpers.Fix((I - 1867216.25) / 36524.25);
            double B;

            if ((I > 2299160))
            {
                B = I + 1 + A - Helpers.Fix(A / 4);
            }
            else {
                B = I;
            }

            double C = B + 1524;
            double D = Helpers.Fix((C - 122.1) / 365.25);
            double E = Helpers.Fix(365.25 * D);
            double G = Helpers.Fix((C - E) / 30.6001);

            if ((G < 13.5))
            {
                functionReturnValue = G - 1;
            }
            else {
                functionReturnValue = G - 13;
            }
            return functionReturnValue;

        }

        /// <summary>
        /// Julian Date to GC Year
        /// </summary>
        /// <param name="JD"></param>
        /// <returns></returns>
        public static double JDtoGCYear(double JD)
        {
            double functionReturnValue = 0;

            double I = Helpers.Fix(JD + 0.5);
            double F = JD + 0.5 - I;
            double A = Helpers.Fix((I - 1867216.25) / 36524.25);
            double B;

            if ((I > 2299160))
            {
                B = I + 1 + A - Helpers.Fix(A / 4);
            }
            else {
                B = I;
            }

            double C = B + 1524;
            double D = Helpers.Fix((C - 122.1) / 365.25);
            double E = Helpers.Fix(365.25 * D);
            double G = Helpers.Fix((C - E) / 30.6001);
            double H;

            if ((G < 13.5))
            {
                H = G - 1;
            }
            else {
                H = G - 13;
            }

            if ((H > 2.5))
            {
                functionReturnValue = D - 4716;
            }
            else {
                functionReturnValue = D - 4715;
            }
            return functionReturnValue;

        }

        /// <summary>
        /// Local Cal Date and Time to Greenwich Cal Date DAY
        /// </summary>
        /// <param name="LCH (local time - hour)"></param>
        /// <param name="LCM (local time - minute)"></param>
        /// <param name="LCS (local time - second)"></param>
        /// <param name="DS (daylight savings - hours)"></param>
        /// <param name="ZC (zone correction - hours)"></param>
        /// <param name="LD (local date - day)"></param>
        /// <param name="LM (local date - month)"></param>
        /// <param name="LY (local date - year)"></param>
        /// <returns></returns>
        public static double LDTtoGCDay(double LCH, double LCM, double LCS, double DS, double ZC, double LD, double LM, double LY)
        {

            double A = Helpers.toDecHour(LCH, LCM, LCS);
            double B = A - DS - ZC;
            double C = LD + (B / 24);
            double D = GCD_JD(C, LM, LY);
            double E = JDtoGCDay(D);
            return Helpers.Fix(E);

        }

        /// <summary>
        /// Local Cal Date and Time to Greenwich Cal Date MONTH
        /// </summary>
        /// <param name="LCH (local time - hour)"></param>
        /// <param name="LCM (local time - minute)"></param>
        /// <param name="LCS (local time - second)"></param>
        /// <param name="DS (daylight savings - hours)"></param>
        /// <param name="ZC (zone correction - hours)"></param>
        /// <param name="LD (local date - day)"></param>
        /// <param name="LM (local date - month)"></param>
        /// <param name="LY (local date - year)"></param>
        /// <returns></returns>
        public static double LDTtoGCMonth(double LCH, double LCM, double LCS, double DS, double ZC, double LD, double LM, double LY)
        {

            double A = Helpers.toDecHour(LCH, LCM, LCS);
            double B = A - DS - ZC;
            double C = LD + (B / 24);
            double D = GCD_JD(C, LM, LY);
            return JDtoGCMonth(D);

        }

        /// <summary>
        /// Local Cal Date and Time to Greenwich Cal Date YEAR
        /// </summary>
        /// <param name="LCH (local time - hour)"></param>
        /// <param name="LCM (local time - minute)"></param>
        /// <param name="LCS (local time - second)"></param>
        /// <param name="DS (daylight savings - hours)"></param>
        /// <param name="ZC (zone correction - hours)"></param>
        /// <param name="LD (local date - day)"></param>
        /// <param name="LM (local date - month)"></param>
        /// <param name="LY (local date - year)"></param>
        /// <returns></returns>
        public static double LDTtoGCYear(double LCH, double LCM, double LCS, double DS, double ZC, double LD, double LM, double LY)
        {

            double A = Helpers.toDecHour(LCH, LCM, LCS);
            double B = A - DS - ZC;
            double C = LD + (B / 24);
            double D = GCD_JD(C, LM, LY);
            return JDtoGCYear(D);

        }

        /// <summary>
        /// Local Cal Date and Time to UT
        /// </summary>
        /// <param name="LCH (local time - hour)"></param>
        /// <param name="LCM (local time - minute)"></param>
        /// <param name="LCS (local time - second)"></param>
        /// <param name="DS (daylight savings - hours)"></param>
        /// <param name="ZC (zone correction - hours)"></param>
        /// <param name="LD (local date - day)"></param>
        /// <param name="LM (local date - month)"></param>
        /// <param name="LY (local date - year)"></param>
        /// <returns></returns>
        public static double LDTtoUT(double LCH, double LCM, double LCS, double DS, double ZC, double LD, double LM, double LY)
        {

            double A = Helpers.toDecHour(LCH, LCM, LCS);
            double B = A - DS - ZC;
            double C = LD + (B / 24.0d);
            double D = GCD_JD(C, LM, LY);
            double E = JDtoGCDay(D);
            double E1 = Helpers.Fix(E);
            return 24.0d * (E - E1);

        }


        public static double UTGST(double UH, double UM, double US, double GD, double GM, double GY)
        {

            double A = GCD_JD(GD, GM, GY);
            double B = A - 2451545;
            double C = B / 36525;
            double D = 6.697374558 + (2400.051336 * C) + (2.5862E-05 * C * C);
            double E = D - (24 * Helpers.Int(D / 24));
            double F = Helpers.toDecHour(UH, UM, US);
            double G = F * 1.002737909;
            double H = E + G;
            return H - (24 * Helpers.Int(H / 24));

        }

        public static double GSTLST(double GH, double GM, double GS, double L)
        {

            double A = Helpers.toDecHour(GH, GM, GS);
            double B = L / 15.0;
            double C = A + B;
            return C - (24.0 * Helpers.Int(C / 24));

        }

        public static double UTLct(double UH, double UM, double US, double DS, double ZC, double GD, double GM, double GY)
        {
            double A = Helpers.toDecHour(UH, UM, US);
            double B = A + ZC;
            double C = B + DS;
            double D = Time.GCD_JD(GD, GM, GY) + (C / 24);
            double E = Time.JDtoGCDay(D);
            double E1 = Helpers.Fix(E);

            return 24 * (E - E1);
        }

    }
}
