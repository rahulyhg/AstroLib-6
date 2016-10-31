namespace AstroLibV4
{
    public class Pos
    {
        /// <summary>
        /// Takes a local DateTime-struct and returns the Moon's right ascension (RAd), declination (DECLd) and Angular Diameter (AngDiam) in degrees as a MoonStruct.
        /// </summary>
        /// <param name="DT"></param>
        /// <returns></returns>
        public static Structs.Moon MoonTG(Structs.MyDateTime DT, Structs.MyLoc Loc)
        {

            double GDay = Time.LDTtoGCDay(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double GMonth = Time.LDTtoGCMonth(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double GYear = Time.LDTtoGCYear(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double UT = Time.LDTtoUT(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double MLon = Subs.MoonLong(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double MLat = Subs.MoonLat(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double NaL = Subs.NutLong(GDay, GMonth, GYear);
            double CorLon = MLon + NaL;
            double HP = Subs.MoonHorPar(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double EMdist = 6378.14d / Helpers.Sin(Helpers.toRadians(HP));
            double MS = Subs.MoonSize(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double Mra = Subs.EclCoo_RA(CorLon, 0, 0, MLat, 0, 0, GDay, GMonth, GYear);
            double Mdec = Subs.EclCoo_Dec(CorLon, 0, 0, MLat, 0, 0, GDay, GMonth, GYear);

            double Lon = Loc.Lon;
            double Lat = Loc.Lat;
            double h = Loc.h;

            double LCT = Time.UTLct(UT, 0, 0, DT.DS, DT.ZC, GDay, GMonth, GYear);
            double HA = Subs.RAHA((Mra / 15), 0, 0, LCT, 0, 0, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear, Lon);
            double CorHA = Subs.ParallaxHA(HA, 0, 0, Mdec, 0, 0, true, Lat, h, HP);
            double CorRA = Subs.HARA(CorHA, 0, 0, LCT, 0, 0, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear, Lon);
            double CorDec = Subs.ParallaxDec(HA, 0, 0, Mdec, 0, 0, true, Lat, h, HP);

            Structs.Moon result = new Structs.Moon(Mra, Mdec, CorRA, CorDec, MS);

            return result;

        }

        /// <summary>
        /// Takes a local DateTime-struct and returns the Sun's right ascension (RAd) and declination (DECLd) in degrees as a PosStruct.
        /// </summary>
        /// <param name="DT"></param>
        /// <returns></returns>
        public static Structs.Pos SunPosTG(Structs.MyDateTime DT)
        {
            double GDay = Time.LDTtoGCDay(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double GMonth = Time.LDTtoGCMonth(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double GYear = Time.LDTtoGCYear(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double SL = Subs.SunLong(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);

            double Sra = Subs.EclCoo_RA(SL, 0, 0, 0, 0, 0, GDay, GMonth, GYear);
            double Sdec = Subs.EclCoo_Dec(SL, 0, 0, 0, 0, 0, GDay, GMonth, GYear);

            Structs.Pos result = new Structs.Pos(Sra, Sdec);

            return result;
        }

        /// <summary>
        /// Takes a local DateTime-struct the planet name and returns the planet's right ascension (RAd) and declination (DECLd) in degrees as a PosStruct.
        /// </summary>
        /// <param name="DT"></param>
        /// <param name="planetName"></param>
        /// <returns></returns>
        public static Structs.Pos PlanetPosTG(Structs.MyDateTime DT, string planetName)
        {
            double GDay = Time.LDTtoGCDay(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double GMonth = Time.LDTtoGCMonth(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double GYear = Time.LDTtoGCYear(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);
            double UT = Time.LDTtoUT(DT.LocHour, DT.LocMin, DT.LocSec, DT.DS, DT.ZC, DT.LocDay, DT.LocMonth, DT.LocYear);

            #region  create collection of constants
            System.Collections.Generic.Dictionary<string, double[]> PlanetDict = new System.Collections.Generic.Dictionary<string, double[]>();
            double[] jup = new double[] { 11.857911, 337.917132, 14.6633, 0.048907, 5.20278, 1.3035, 100.595, 196.74, -9.40 };
            double[] mar = new double[] { 1.880765, 109.09646, 336.217, 0.093348, 1.523689, 1.8497, 49.632, 9.36, -1.52 };
            double[] mer = new double[] { 0.24085, 75.5671, 77.612, 0.205627, 0.387098, 7.0051, 48.449, 6.74, -0.42 };
            double[] nep = new double[] { 165.84539, 326.895127, 23.07, 0.010483, 30.1985, 1.7673, 131.879, 62.20, -6.87 };
            double[] sat = new double[] { 29.310579, 172.398316, 89.567, 0.053853, 9.51134, 2.4873, 113.752, 165.60, -8.88 };
            double[] ura = new double[] { 84.039492, 356.135400, 172.884833, 0.046321, 19.21814, 0.773059, 73.926961, 65.80, -7.19 };
            double[] ven = new double[] { 0.615207, 272.30044, 131.54, 0.006812, 0.723329, 3.3947, 76.769, 16.92, -4.40 };
            double[] ear = new double[] { 0.999996, 99.556772, 103.2055, 0.016671, 0.999985, 0, 0, 0, 0 };
            PlanetDict.Add("JUPITER", jup);
            PlanetDict.Add("MARS", mar);
            PlanetDict.Add("MERCURY", mer);
            PlanetDict.Add("NEPTUNE", nep);
            PlanetDict.Add("SATURN", sat);
            PlanetDict.Add("URANUS", ura);
            PlanetDict.Add("VENUS", ven);
            PlanetDict.Add("EARTH", ear);
            #endregion

            #region pull our planet's constants
            string planetNameU = planetName.ToUpper();
            double[] consts = PlanetDict[planetNameU];
            double Tp = consts[0];
            double Long = consts[1];
            double Peri = consts[2];
            double Ecc = consts[3];
            double Axis = consts[4];
            double Incl = consts[5];
            double Node = consts[6];
            #endregion

            double D = Time.GCD_JD(GDay + (UT / 24.0), GMonth, GYear) - Time.GCD_JD(0, 1, 2010);
            double Np = 360d * D / (365.242191d * Tp);
            Np = Np - 360 * Helpers.Int(Np / 360.0d);
            double Mp = Np + Long - Peri;
            double Lp = Np + (360 * Ecc * Helpers.Sin(Helpers.toRadians(Mp)) / System.Math.PI) + Long;
            Lp = Lp - 360 * Helpers.Int(Lp / 360);
            double Ta = Lp - Peri;
            double r = Axis * (1 - System.Math.Pow(Ecc, 2)) / (1 + Ecc * Helpers.Cos(Helpers.toRadians(Ta)));

            #region pull Earth's constants
            double[] consts2 = PlanetDict["EARTH"];
            double E_Tp = consts2[0];
            double E_Long = consts2[1];
            double E_Peri = consts2[2];
            double E_Ecc = consts2[3];
            double E_Axis = consts2[4];
            #endregion

            double Ne = 360 * D / (365.2421 * E_Tp);
            Ne = Ne - 360 * Helpers.Int(Ne / 360);
            double Me = Ne + E_Long - E_Peri;
            double Le = Ne + E_Long + 360 * E_Ecc * Helpers.Sin(Helpers.toRadians(Me)) / System.Math.PI;
            Le = Le - 360 * Helpers.Int(Le / 360);
            double E_Ta = Le - E_Peri;
            double R = E_Axis * (1 - System.Math.Pow(E_Ecc, 2)) / (1 + E_Ecc * Helpers.Cos(Helpers.toRadians(E_Ta)));
            double Lp_Node = Helpers.toRadians(Lp - Node);
            double psi = Helpers.Asin(Helpers.Sin(Lp_Node) * Helpers.Sin(Helpers.toRadians(Incl)));
            double y = Helpers.Sin(Lp_Node) * Helpers.Cos(Helpers.toRadians(Incl));
            double x = Helpers.Cos(Lp_Node);
            double ld = Helpers.toDegrees(Helpers.Atan2(x, y)) + Node;
            double rd = r * Helpers.Cos(psi);
            double Le_Ld = Helpers.toRadians(Le - ld);
            double type1 = Helpers.Atan2(R - rd * Helpers.Cos(Le_Ld), rd * Helpers.Sin(Le_Ld));
            double type2 = Helpers.Atan2(rd - R * Helpers.Cos(Le_Ld), R * Helpers.Sin(-Le_Ld));
            double A = (rd < 1 ? type1 : type2);
            double lambda = (rd < 1 ? 180 + Le + Helpers.toDegrees(A) : Helpers.toDegrees(A) + ld);
            lambda = lambda - 360 * Helpers.Int(lambda / 360);
            double beta = Helpers.toDegrees(System.Math.Atan(rd * Helpers.Tan(psi) * Helpers.Sin(Helpers.toRadians(lambda - ld))) / R * Helpers.Sin(-Le_Ld));
            double RA = Subs.EclCoo_RA(lambda, 0, 0, beta, 0, 0, GDay, GMonth, GYear);
            double DEC = Subs.EclCoo_Dec(lambda, 0, 0, beta, 0, 0, GDay, GMonth, GYear);


            Structs.Pos result = new Structs.Pos(RA, DEC);
            return result;
        }
    }
}
