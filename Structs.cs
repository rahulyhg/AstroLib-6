namespace AstroLibV4
{
    public class Structs
    {
        public struct Pos
        {
            public double RAd;
            public double DECLd;

            public Pos(double RAd = 0, double DECd = 0)
            {
                this.RAd = RAd;
                this.DECLd = DECd;
            }

            public override string ToString()
            {
                string result = string.Format(" right ascesion (in decimal hours): {0}\n Declination (in decimal degrees): {1}", RAd, DECLd);
                return result;
            }


        }

        public struct Moon
        {
            public double RAd;
            public double DECLd;
            public double AngDiam;
            public double RAdGC;
            public double DECLdGC;

            public Moon(double RAdGC = 0, double DECLdGC = 0, double RAd = 0, double DECd = 0, double AngDiam = 0)
            {
                this.RAd = RAd;
                this.DECLd = DECd;
                this.AngDiam = AngDiam;
                this.RAdGC = RAdGC;
                this.DECLdGC = DECLdGC;
            }

            public override string ToString()
            {
                string result = string.Format("right ascesion (in decimal degrees): {0}\n Declination (in decimal degrees): {1}\n Angular Diameter (in decimal degrees): {2}\n right ascesion (in decimal degrees), geocentric: {3}\n declination (in decimal degrees), geocentric: {4}\n", RAd, DECLd, AngDiam, RAdGC, DECLdGC);
                return result;
            }

        }

        public struct EclCoord
        {
            public double Lon;
            public double Lat;

            public EclCoord(double Lon, double Lat)
            {
                this.Lon = Lon;
                this.Lat = Lat;
            }
        }

        public struct PlanetConst
        {
            public double Tp, Long, Peri, Ecc, Axis, Incl, Node;

            public PlanetConst(double Tp, double Long, double Peri, double Ecc, double Axis, double Incl, double Node)
            {
                this.Tp = Tp;
                this.Long = Long;
                this.Peri = Peri;
                this.Ecc = Ecc;
                this.Axis = Axis;
                this.Incl = Incl;
                this.Node = Node;

            }
        }

        public struct MyDateTime
        {
            public double LocHour;
            public double LocMin;
            public double LocSec;
            public double DS;
            public double ZC;
            public double LocDay;
            public double LocMonth;
            public double LocYear;

            public MyDateTime(double LocHour, double LocMin, double LocSec, double DS, double ZC, double LocDay, double LocMonth, double LocYear)
            {
                this.LocHour = LocHour;
                this.LocMin = LocMin;
                this.LocSec = LocSec;
                this.DS = DS;
                this.ZC = ZC;
                this.LocDay = LocDay;
                this.LocMonth = LocMonth;
                this.LocYear = LocYear;
            }

            public override string ToString()
            {
                string result = string.Format("Local Date\t{0}.{1}.{2}\nLocal Time\t{3}:{4}:{5}", LocYear, LocMonth, LocDay, LocHour, LocMin, LocSec);
                return result;
            }
        }

        public struct MyLoc
        {
            public double Lon;
            public double Lat;
            public double h;

            public MyLoc(double Lon, double Lat, double h)
            {
                this.Lon = Lon;
                this.Lat = Lat;
                this.h = h;
            }

            public override string ToString()
            {
                return string.Format(" Longitude:\t{0}\n Latitude:\t{1}\n Height:\t{2}\n", Lon, Lat, h);
            }
        }
    }
}
