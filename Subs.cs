namespace AstroLibV4
{
    class Subs
    {
        public static double NutObl(double GD, double GM, double GY)
        {

            double DJ, T, T2, A, B, L1, l2, D1, D2, M1, M2, N1, N2, DDO;

            DJ = Time.GCD_JD(GD, GM, GY) - 2415020.0d;
            T = DJ / 36525.0d;
            T2 = T * T;
            A = 100.0021358d * T;
            B = 360.0d * (A - Helpers.Int(A));
            L1 = 279.6967d + 0.000303d * T2 + B;
            l2 = 2.0d * Helpers.toRadians(L1);
            A = 1336.855231d * T;
            B = 360.0d * (A - Helpers.Int(A));
            D1 = 270.4342d - 0.001133d * T2 + B;
            D2 = 2.0d * Helpers.toRadians(D1);
            A = 99.99736056d * T;
            B = 360.0d * (A - Helpers.Int(A));
            M1 = 358.4758d - 0.00015d * T2 + B;
            M1 = Helpers.toRadians(M1);
            A = 1325.552359d * T;
            B = 360.0d * (A - Helpers.Int(A));
            M2 = 296.1046d + 0.009192d * T2 + B;
            M2 = Helpers.toRadians(M2);
            A = 5.372616667d * T;
            B = 360.0d * (A - Helpers.Int(A));
            N1 = 259.1833d + 0.002078d * T2 - B;
            N1 = Helpers.toRadians(N1);
            N2 = 2.0d * N1;

            DDO = (9.21d + 0.00091d * T) * Helpers.Cos(N1);
            DDO = DDO + (0.5522d - 0.00029d * T) * Helpers.Cos(l2) - 0.0904d * Helpers.Cos(N2);
            DDO = DDO + 0.0884d * Helpers.Cos(D2) + 0.0216d * Helpers.Cos(l2 + M1);
            DDO = DDO + 0.0183d * Helpers.Cos(D2 - N1) + 0.0113d * Helpers.Cos(D2 + M2);
            DDO = DDO - 0.0093d * Helpers.Cos(l2 - M1) - 0.0066d * Helpers.Cos(l2 - N1);

            return DDO / 3600.0d;

        }

        public static double Obl(double GD, double GM, double GY)
        {
            double A, B, C, D, E;

            A = Time.GCD_JD(GD, GM, GY);
            B = A - 2415020.0d;
            C = (B / 36525.0d) - 1.0d;
            D = C * (46.815d + C * (0.0006d - (C * 0.00181d)));
            E = D / 3600.0d;
            return 23.43929167d - E + NutObl(GD, GM, GY);

        }

        /// <summary>
        /// Local Cal Date and Time to Moon ecliptic longitude (deg)
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
        public static double MoonLong(double LH, double LM, double LS, double DS, double ZC, double DY, double MN, double YR)
        {

            double UT = Time.LDTtoUT(LH, LM, LS, DS, ZC, DY, MN, YR);
            double GD = Time.LDTtoGCDay(LH, LM, LS, DS, ZC, DY, MN, YR);
            double GM = Time.LDTtoGCMonth(LH, LM, LS, DS, ZC, DY, MN, YR);
            double GY = Time.LDTtoGCYear(LH, LM, LS, DS, ZC, DY, MN, YR);
            double T = ((Time.GCD_JD(GD, GM, GY) - 2415020.0d) / 36525.0d) + (UT / 876600.0d);
            double T2 = T * T;

            double M1 = 27.32158213d;
            double M2 = 365.2596407d;
            double M3 = 27.55455094d;
            double M4 = 29.53058868d;
            double M5 = 27.21222039d;
            double M6 = 6798.363307d;
            double Q = Time.GCD_JD(GD, GM, GY) - 2415020.0d + (UT / 24.0d);
            M1 = Q / M1;
            M2 = Q / M2;
            M3 = Q / M3;
            M4 = Q / M4;
            M5 = Q / M5;
            M6 = Q / M6;
            M1 = 360 * (M1 - Helpers.Int(M1));
            M2 = 360 * (M2 - Helpers.Int(M2));
            M3 = 360 * (M3 - Helpers.Int(M3));
            M4 = 360 * (M4 - Helpers.Int(M4));
            M5 = 360 * (M5 - Helpers.Int(M5));
            M6 = 360 * (M6 - Helpers.Int(M6));

            double ML = 270.434164d + M1 - (0.001133d - 1.9E-06d * T) * T2;
            double MS = 358.475833d + M2 - (0.00015d + 3.3E-06d * T) * T2;
            double MD = 296.104608d + M3 + (0.009192d + 1.44E-05d * T) * T2;
            double ME1 = 350.737486d + M4 - (0.001436d - 1.9E-06d * T) * T2;
            double MF = 11.250889d + M5 - (0.003211d + 3E-07d * T) * T2;
            double NA = 259.183275d - M6 + (0.002078d + 2.2E-06d * T) * T2;
            double A = Helpers.toRadians(51.2d + 20.2d * T);
            double S1 = Helpers.Sin(A);
            double S2 = Helpers.Sin(Helpers.toRadians(NA));
            double B = 346.56d + (132.87d - 0.0091731d * T) * T;
            double S3 = 0.003964d * Helpers.Sin(Helpers.toRadians(B));
            double C = Helpers.toRadians(NA + 275.05d - 2.3d * T);
            double S4 = Helpers.Sin(C);
            ML = ML + 0.000233d * S1 + S3 + 0.001964d * S2;
            MS = MS - 0.001778d * S1;
            MD = MD + 0.000817d * S1 + S3 + 0.002541d * S2;
            MF = MF + S3 - 0.024691d * S2 - 0.004328d * S4;
            ME1 = ME1 + 0.002011d * S1 + S3 + 0.001964d * S2;
            double E = 1.0d - (0.002495d + 7.52E-06d * T) * T;
            double E2 = E * E;
            ML = Helpers.toRadians(ML);
            MS = Helpers.toRadians(MS);
            NA = Helpers.toRadians(NA);
            ME1 = Helpers.toRadians(ME1);
            MF = Helpers.toRadians(MF);
            MD = Helpers.toRadians(MD);

            double L = 6.28875d * Helpers.Sin(MD) + 1.274018d * Helpers.Sin(2 * ME1 - MD);
            L = L + 0.658309d * Helpers.Sin(2 * ME1) + 0.213616d * Helpers.Sin(2 * MD);
            L = L - E * 0.185596d * Helpers.Sin(MS) - 0.114336d * Helpers.Sin(2 * MF);
            L = L + 0.058793d * Helpers.Sin(2 * (ME1 - MD));
            L = L + 0.057212d * E * Helpers.Sin(2 * ME1 - MS - MD) + 0.05332d * Helpers.Sin(2 * ME1 + MD);
            L = L + 0.045874d * E * Helpers.Sin(2 * ME1 - MS) + 0.041024d * E * Helpers.Sin(MD - MS);
            L = L - 0.034718d * Helpers.Sin(ME1) - E * 0.030465d * Helpers.Sin(MS + MD);
            L = L + 0.015326d * Helpers.Sin(2 * (ME1 - MF)) - 0.012528d * Helpers.Sin(2 * MF + MD);
            L = L - 0.01098d * Helpers.Sin(2 * MF - MD) + 0.010674d * Helpers.Sin(4 * ME1 - MD);
            L = L + 0.010034d * Helpers.Sin(3 * MD) + 0.008548d * Helpers.Sin(4 * ME1 - 2 * MD);
            L = L - E * 0.00791d * Helpers.Sin(MS - MD + 2 * ME1) - E * 0.006783d * Helpers.Sin(2 * ME1 + MS);
            L = L + 0.005162d * Helpers.Sin(MD - ME1) + E * 0.005d * Helpers.Sin(MS + ME1);
            L = L + 0.003862d * Helpers.Sin(4 * ME1) + E * 0.004049d * Helpers.Sin(MD - MS + 2 * ME1);
            L = L + 0.003996d * Helpers.Sin(2 * (MD + ME1)) + 0.003665d * Helpers.Sin(2 * ME1 - 3 * MD);
            L = L + E * 0.002695d * Helpers.Sin(2 * MD - MS) + 0.002602d * Helpers.Sin(MD - 2 * (MF + ME1));
            L = L + E * 0.002396d * Helpers.Sin(2 * (ME1 - MD) - MS) - 0.002349d * Helpers.Sin(MD + ME1);
            L = L + E2 * 0.002249d * Helpers.Sin(2 * (ME1 - MS)) - E * 0.002125d * Helpers.Sin(2 * MD + MS);
            L = L - E2 * 0.002079d * Helpers.Sin(2 * MS) + E2 * 0.002059d * Helpers.Sin(2 * (ME1 - MS) - MD);
            L = L - 0.001773d * Helpers.Sin(MD + 2 * (ME1 - MF)) - 0.001595d * Helpers.Sin(2 * (MF + ME1));
            L = L + E * 0.00122d * Helpers.Sin(4 * ME1 - MS - MD) - 0.00111d * Helpers.Sin(2 * (MD + MF));
            L = L + 0.000892d * Helpers.Sin(MD - 3 * ME1) - E * 0.000811d * Helpers.Sin(MS + MD + 2 * ME1);
            L = L + E * 0.000761d * Helpers.Sin(4 * ME1 - MS - 2 * MD);
            L = L + E2 * 0.000704d * Helpers.Sin(MD - 2 * (MS + ME1));
            L = L + E * 0.000693d * Helpers.Sin(MS - 2 * (MD - ME1));
            L = L + E * 0.000598d * Helpers.Sin(2 * (ME1 - MF) - MS);
            L = L + 0.00055d * Helpers.Sin(MD + 4 * ME1) + 0.000538d * Helpers.Sin(4 * MD);
            L = L + E * 0.000521d * Helpers.Sin(4 * ME1 - MS) + 0.000486d * Helpers.Sin(2 * MD - ME1);
            L = L + E2 * 0.000717d * Helpers.Sin(MD - 2 * MS);
            double MM = Helpers.Unwind(ML + Helpers.toRadians(L));

            return Helpers.toDegrees(MM);

        }

        /// <summary>
        /// Local Cal Date and Time to Moon ecliptic latitude (deg)
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
        public static double MoonLat(double LH, double LM, double LS, double DS, double ZC, double DY, double MN, double YR)
        {

            double UT = Time.LDTtoUT(LH, LM, LS, DS, ZC, DY, MN, YR);
            double GD = Time.LDTtoGCDay(LH, LM, LS, DS, ZC, DY, MN, YR);
            double GM = Time.LDTtoGCMonth(LH, LM, LS, DS, ZC, DY, MN, YR);
            double GY = Time.LDTtoGCYear(LH, LM, LS, DS, ZC, DY, MN, YR);
            double T = ((Time.GCD_JD(GD, GM, GY) - 2415020.0d) / 36525.0d) + (UT / 876600.0d);
            double T2 = T * T;

            double M1 = 27.32158213d;
            double M2 = 365.2596407d;
            double M3 = 27.55455094d;
            double M4 = 29.53058868d;
            double M5 = 27.21222039d;
            double M6 = 6798.363307d;
            double Q = Time.GCD_JD(GD, GM, GY) - 2415020.0d + (UT / 24.0d);
            M1 = Q / M1;
            M2 = Q / M2;
            M3 = Q / M3;
            M4 = Q / M4;
            M5 = Q / M5;
            M6 = Q / M6;
            M1 = 360 * (M1 - Helpers.Int(M1));
            M2 = 360 * (M2 - Helpers.Int(M2));
            M3 = 360 * (M3 - Helpers.Int(M3));
            M4 = 360 * (M4 - Helpers.Int(M4));
            M5 = 360 * (M5 - Helpers.Int(M5));
            M6 = 360 * (M6 - Helpers.Int(M6));

            double ML = 270.434164d + M1 - (0.001133d - 1.9E-06d * T) * T2;
            double MS = 358.475833d + M2 - (0.00015d + 3.3E-06d * T) * T2;
            double MD = 296.104608d + M3 + (0.009192d + 1.44E-05d * T) * T2;
            double ME1 = 350.737486d + M4 - (0.001436d - 1.9E-06d * T) * T2;
            double MF = 11.250889d + M5 - (0.003211d + 3E-07d * T) * T2;
            double NA = 259.183275d - M6 + (0.002078d + 2.2E-06d * T) * T2;
            double A = Helpers.toRadians(51.2d + 20.2d * T);
            double S1 = Helpers.Sin(A);
            double S2 = Helpers.Sin(Helpers.toRadians(NA));
            double B = 346.56d + (132.87d - 0.0091731d * T) * T;
            double S3 = 0.003964d * Helpers.Sin(Helpers.toRadians(B));
            double C = Helpers.toRadians(NA + 275.05d - 2.3d * T);
            double S4 = Helpers.Sin(C);
            ML = ML + 0.000233d * S1 + S3 + 0.001964d * S2;
            MS = MS - 0.001778d * S1;
            MD = MD + 0.000817d * S1 + S3 + 0.002541d * S2;
            MF = MF + S3 - 0.024691d * S2 - 0.004328d * S4;
            ME1 = ME1 + 0.002011d * S1 + S3 + 0.001964d * S2;
            double E = 1.0d - (0.002495d + 7.52E-06d * T) * T;
            double E2 = E * E;
            ML = Helpers.toRadians(ML);
            MS = Helpers.toRadians(MS);
            NA = Helpers.toRadians(NA);
            ME1 = Helpers.toRadians(ME1);
            MF = Helpers.toRadians(MF);
            MD = Helpers.toRadians(MD);

            double G = 5.128189d * Helpers.Sin(MF) + 0.280606d * Helpers.Sin(MD + MF);
            G = G + 0.277693d * Helpers.Sin(MD - MF) + 0.173238d * Helpers.Sin(2 * ME1 - MF);
            G = G + 0.055413d * Helpers.Sin(2 * ME1 + MF - MD) + 0.046272d * Helpers.Sin(2 * ME1 - MF - MD);
            G = G + 0.032573d * Helpers.Sin(2 * ME1 + MF) + 0.017198d * Helpers.Sin(2 * MD + MF);
            G = G + 0.009267d * Helpers.Sin(2 * ME1 + MD - MF) + 0.008823d * Helpers.Sin(2 * MD - MF);
            G = G + E * 0.008247d * Helpers.Sin(2 * ME1 - MS - MF) + 0.004323d * Helpers.Sin(2 * (ME1 - MD) - MF);
            G = G + 0.0042d * Helpers.Sin(2 * ME1 + MF + MD) + E * 0.003372d * Helpers.Sin(MF - MS - 2 * ME1);
            G = G + E * 0.002472d * Helpers.Sin(2 * ME1 + MF - MS - MD);
            G = G + E * 0.002222d * Helpers.Sin(2 * ME1 + MF - MS);
            G = G + E * 0.002072d * Helpers.Sin(2 * ME1 - MF - MS - MD);
            G = G + E * 0.001877d * Helpers.Sin(MF - MS + MD) + 0.001828d * Helpers.Sin(4 * ME1 - MF - MD);
            G = G - E * 0.001803d * Helpers.Sin(MF + MS) - 0.00175d * Helpers.Sin(3 * MF);
            G = G + E * 0.00157d * Helpers.Sin(MD - MS - MF) - 0.001487d * Helpers.Sin(MF + ME1);
            G = G - E * 0.001481d * Helpers.Sin(MF + MS + MD) + E * 0.001417d * Helpers.Sin(MF - MS - MD);
            G = G + E * 0.00135d * Helpers.Sin(MF - MS) + 0.00133d * Helpers.Sin(MF - ME1);
            G = G + 0.001106d * Helpers.Sin(MF + 3 * MD) + 0.00102d * Helpers.Sin(4 * ME1 - MF);
            G = G + 0.000833d * Helpers.Sin(MF + 4 * ME1 - MD) + 0.000781d * Helpers.Sin(MD - 3 * MF);
            G = G + 0.00067d * Helpers.Sin(MF + 4 * ME1 - 2 * MD) + 0.000606d * Helpers.Sin(2 * ME1 - 3 * MF);
            G = G + 0.000597d * Helpers.Sin(2 * (ME1 + MD) - MF);
            G = G + E * 0.000492d * Helpers.Sin(2 * ME1 + MD - MS - MF) + 0.00045d * Helpers.Sin(2 * (MD - ME1) - MF);
            G = G + 0.000439d * Helpers.Sin(3 * MD - MF) + 0.000423d * Helpers.Sin(MF + 2 * (ME1 + MD));
            G = G + 0.000422d * Helpers.Sin(2 * ME1 - MF - 3 * MD) - E * 0.000367d * Helpers.Sin(MS + MF + 2 * ME1 - MD);
            G = G - E * 0.000353d * Helpers.Sin(MS + MF + 2 * ME1) + 0.000331d * Helpers.Sin(MF + 4 * ME1);
            G = G + E * 0.000317d * Helpers.Sin(2 * ME1 + MF - MS + MD);
            G = G + E2 * 0.000306d * Helpers.Sin(2 * (ME1 - MS) - MF) - 0.000283d * Helpers.Sin(MD + 3 * MF);
            double W1 = 0.0004664d * Helpers.Cos(NA);
            double W2 = 7.54E-05d * Helpers.Cos(C);
            double BM = Helpers.toRadians(G) * (1.0d - W1 - W2);

            return Helpers.toDegrees(BM);

        }

        /// <summary>
        /// Greenwich Day, Month, Year to nunation in longitude
        /// </summary>
        /// <param name="GD"></param>
        /// <param name="GM"></param>
        /// <param name="GY"></param>
        /// <returns></returns>
        public static double NutLong(double GD, double GM, double GY)
        {

            double DJ = Time.GCD_JD(GD, GM, GY) - 2415020.0d;
            double T = DJ / 36525.0d;
            double T2 = T * T;
            double A = 100.0021358d * T;
            double B = 360.0d * (A - Helpers.Int(A));
            double L1 = 279.6967d + 0.000303d * T2 + B;
            double l2 = 2.0d * Helpers.toRadians(L1);
            A = 1336.855231d * T;
            B = 360.0d * (A - Helpers.Int(A));
            double D1 = 270.4342d - 0.001133d * T2 + B;
            double D2 = 2.0d * Helpers.toRadians(D1);
            A = 99.99736056d * T;
            B = 360.0d * (A - Helpers.Int(A));
            double M1 = 358.4758d - 0.00015d * T2 + B;
            M1 = Helpers.toRadians(M1);
            A = 1325.552359d * T;
            B = 360.0d * (A - Helpers.Int(A));
            double M2 = 296.1046d + 0.009192d * T2 + B;
            M2 = Helpers.toRadians(M2);
            A = 5.372616667d * T;
            B = 360.0d * (A - Helpers.Int(A));
            double N1 = 259.1833d + 0.002078d * T2 - B;
            N1 = Helpers.toRadians(N1);
            double N2 = 2 * N1;

            double DP = (-17.2327d - 0.01737d * T) * Helpers.Sin(N1);
            DP = DP + (-1.2729d - 0.00013d * T) * Helpers.Sin(l2) + 0.2088d * Helpers.Sin(N2);
            DP = DP - 0.2037d * Helpers.Sin(D2) + (0.1261d - 0.00031d * T) * Helpers.Sin(M1);
            DP = DP + 0.0675d * Helpers.Sin(M2) - (0.0497d - 0.00012d * T) * Helpers.Sin(l2 + M1);
            DP = DP - 0.0342d * Helpers.Sin(D2 - N1) - 0.0261d * Helpers.Sin(D2 + M2);
            DP = DP + 0.0214d * Helpers.Sin(l2 - M1) - 0.0149d * Helpers.Sin(l2 - D2 + M2);
            DP = DP + 0.0124d * Helpers.Sin(l2 - N1) + 0.0114d * Helpers.Sin(D2 - M2);

            return DP / 3600.0d;

        }

        /// <summary>
        /// Moon ecliptic longitude and Nunation in Longitude to Corrected Longitude
        /// </summary>
        /// <param name="ML"></param>
        /// <param name="NL"></param>
        /// <returns></returns>
        public static double CorLon(double ML, double NL)
        {
            return ML - NL;
        }

        /// <summary>
        /// Local Cal Date and Time to Moon horizontal parallax
        /// </summary>
        /// <param name="LH (local time - hour)"></param>
        /// <param name="LM (local time - minute)"></param>
        /// <param name="LS (local time - second)"></param>
        /// <param name="DS (daylight savings - hours)"></param>
        /// <param name="ZC (zone correction - hours)"></param>
        /// <param name="LD (local date - day)"></param>
        /// <param name="LM (local date - month)"></param>
        /// <param name="LY (local date - year)"></param>
        /// <returns></returns>
        public static double MoonHorPar(double LH, double LM, double LS, double DS, double ZC, double DY, double MN, double YR)
        {

            double UT = Time.LDTtoUT(LH, LM, LS, DS, ZC, DY, MN, YR);
            double GD = Time.LDTtoGCDay(LH, LM, LS, DS, ZC, DY, MN, YR);
            double GM = Time.LDTtoGCMonth(LH, LM, LS, DS, ZC, DY, MN, YR);
            double GY = Time.LDTtoGCYear(LH, LM, LS, DS, ZC, DY, MN, YR);
            double T = ((Time.GCD_JD(GD, GM, GY) - 2415020.0d) / 36525.0d) + (UT / 876600.0d);
            double T2 = T * T;

            double M1 = 27.32158213d;
            double M2 = 365.2596407d;
            double M3 = 27.55455094d;
            double M4 = 29.53058868d;
            double M5 = 27.21222039d;
            double M6 = 6798.363307d;
            double Q = Time.GCD_JD(GD, GM, GY) - 2415020.0d + (UT / 24.0d);
            M1 = Q / M1;
            M2 = Q / M2;
            M3 = Q / M3;
            M4 = Q / M4;
            M5 = Q / M5;
            M6 = Q / M6;
            M1 = 360 * (M1 - Helpers.Int(M1));
            M2 = 360 * (M2 - Helpers.Int(M2));
            M3 = 360 * (M3 - Helpers.Int(M3));
            M4 = 360 * (M4 - Helpers.Int(M4));
            M5 = 360 * (M5 - Helpers.Int(M5));
            M6 = 360 * (M6 - Helpers.Int(M6));

            double ML = 270.434164d + M1 - (0.001133d - 1.9E-06d * T) * T2;
            double MS = 358.475833d + M2 - (0.00015d + 3.3E-06d * T) * T2;
            double MD = 296.104608d + M3 + (0.009192d + 1.44E-05d * T) * T2;
            double ME1 = 350.737486d + M4 - (0.001436d - 1.9E-06d * T) * T2;
            double MF = 11.250889d + M5 - (0.003211d + 3E-07d * T) * T2;
            double NA = 259.183275d - M6 + (0.002078d + 2.2E-06d * T) * T2;
            double A = Helpers.toRadians(51.2d + 20.2d * T);
            double S1 = Helpers.Sin(A);
            double S2 = Helpers.Sin(Helpers.toRadians(NA));
            double B = 346.56d + (132.87d - 0.0091731d * T) * T;
            double S3 = 0.003964 * Helpers.Sin(Helpers.toRadians(B));
            double C = Helpers.toRadians(NA + 275.05d - 2.3d * T);
            double S4 = Helpers.Sin(C);
            ML = ML + 0.000233d * S1 + S3 + 0.001964d * S2;
            MS = MS - 0.001778d * S1;
            MD = MD + 0.000817d * S1 + S3 + 0.002541d * S2;
            MF = MF + S3 - 0.024691d * S2 - 0.004328d * S4;
            ME1 = ME1 + 0.002011d * S1 + S3 + 0.001964d * S2;
            double E = 1.0d - (0.002495d + 7.52E-06d * T) * T;
            double E2 = E * E;
            ML = Helpers.toRadians(ML);
            MS = Helpers.toRadians(MS);
            NA = Helpers.toRadians(NA);
            ME1 = Helpers.toRadians(ME1);
            MF = Helpers.toRadians(MF);
            MD = Helpers.toRadians(MD);

            double PM = 0.950724d + 0.051818d * Helpers.Cos(MD) + 0.009531d * Helpers.Cos(2 * ME1 - MD);
            PM = PM + 0.007843d * Helpers.Cos(2 * ME1) + 0.002824d * Helpers.Cos(2 * MD);
            PM = PM + 0.000857d * Helpers.Cos(2 * ME1 + MD) + E * 0.000533d * Helpers.Cos(2 * ME1 - MS);
            PM = PM + E * 0.000401d * Helpers.Cos(2 * ME1 - MD - MS);
            PM = PM + E * 0.00032d * Helpers.Cos(MD - MS) - 0.000271d * Helpers.Cos(ME1);
            PM = PM - E * 0.000264d * Helpers.Cos(MS + MD) - 0.000198d * Helpers.Cos(2 * MF - MD);
            PM = PM + 0.000173d * Helpers.Cos(3 * MD) + 0.000167d * Helpers.Cos(4 * ME1 - MD);
            PM = PM - E * 0.000111d * Helpers.Cos(MS) + 0.000103d * Helpers.Cos(4 * ME1 - 2 * MD);
            PM = PM - 8.4E-05d * Helpers.Cos(2 * MD - 2 * ME1) - E * 8.3E-05d * Helpers.Cos(2 * ME1 + MS);
            PM = PM + 7.9E-05d * Helpers.Cos(2 * ME1 + 2 * MD) + 7.2E-05d * Helpers.Cos(4 * ME1);
            PM = PM + E * 6.4E-05d * Helpers.Cos(2 * ME1 - MS + MD) - E * 6.3E-05d * Helpers.Cos(2 * ME1 + MS - MD);
            PM = PM + E * 4.1E-05d * Helpers.Cos(MS + ME1) + E * 3.5E-05d * Helpers.Cos(2 * MD - MS);
            PM = PM - 3.3E-05d * Helpers.Cos(3 * MD - 2 * ME1) - 3E-05d * Helpers.Cos(MD + ME1);
            PM = PM - 2.9E-05d * Helpers.Cos(2 * (MF - ME1)) - E * 2.9E-05d * Helpers.Cos(2 * MD + MS);
            PM = PM + E2 * 2.6E-05d * Helpers.Cos(2 * (ME1 - MS)) - 2.3E-05d * Helpers.Cos(2 * (MF - ME1) + MD);
            PM = PM + E * 1.9E-05d * Helpers.Cos(4 * ME1 - MS - MD);

            return PM;

        }

        /// <summary>
        /// Moon Horizontal Parallax (MoonHP) to Moon-Earth-Distance
        /// </summary>
        /// <param name="MHP"></param>
        /// <returns></returns>
        public static double EarthMoonDist(double MHP)
        {
            return 6378.14d / (Helpers.Sin(Helpers.toRadians(MHP)));
        }

        /// <summary>
        /// Local Cal Date and Time to Moon angular diameter
        /// </summary>
        /// <param name="LH (local time - hour)"></param>
        /// <param name="LM (local time - minute)"></param>
        /// <param name="LS (local time - second)"></param>
        /// <param name="DS (daylight savings - hours)"></param>
        /// <param name="ZC (zone correction - hours)"></param>
        /// <param name="LD (local date - day)"></param>
        /// <param name="LM (local date - month)"></param>
        /// <param name="LY (local date - year)"></param>
        /// <returns></returns>
        public static double MoonSize(double LH, double LM, double LS, double DS, double ZC, double DY, double MN, double YR)
        {

            double HP = Helpers.toRadians(MoonHorPar(LH, LM, LS, DS, ZC, DY, MN, YR));
            double R = 6378.14d / Helpers.Sin(HP);
            double TH = 384401d * 0.5181d / R;
            return TH;

        }

        /// <summary>
        /// Takes a bunch of stuff, gives Moon RA in degrees
        /// </summary>
        /// <param name="ELD (CorLon)"></param>
        /// <param name="ELM"></param>
        /// <param name="ELS"></param>
        /// <param name="BD (MoonLat)"></param>
        /// <param name="BM"></param>
        /// <param name="BS"></param>
        /// <param name="GD (GD day)"></param>
        /// <param name="GM (GD month)"></param>
        /// <param name="GY (GD year)"></param>
        /// <returns></returns>
        public static double EclCoo_RA(double ELD, double ELM, double ELS, double BD, double BM, double BS, double GD, double GM, double GY)
        {
            double A = Helpers.toRadians(Helpers.toDecDeg(ELD, ELM, ELS));
            double B = Helpers.toRadians(Helpers.toDecDeg(BD, BM, BS));
            double C = Helpers.toRadians(Obl(GD, GM, GY));
            double D = Helpers.Sin(A) * Helpers.Cos(C) - Helpers.Tan(B) * Helpers.Sin(C);
            double E = Helpers.Cos(A);
            double F = Helpers.toDegrees(Helpers.Atan2(E, D));
            return F - 360.0d * Helpers.Int(F / 360.0d);


        }

        /// <summary>
        /// Takes a bunch of stuff, gives Moon Decl in degrees
        /// </summary>
        /// <param name="ELD (CorLon)"></param>
        /// <param name="ELM"></param>
        /// <param name="ELS"></param>
        /// <param name="BD (MoonLat)"></param>
        /// <param name="BM"></param>
        /// <param name="BS"></param>
        /// <param name="GD (GD day)"></param>
        /// <param name="GM (GD month)"></param>
        /// <param name="GY (GD year)"></param>
        /// <returns></returns>
        public static double EclCoo_Dec(double ELD, double ELM, double ELS, double BD, double BM, double BS, double GD, double GM, double GY)
        {

            double A = Helpers.toRadians(Helpers.toDecDeg(ELD, ELM, ELS));
            double B = Helpers.toRadians(Helpers.toDecDeg(BD, BM, BS));
            double C = Helpers.toRadians(Obl(GD, GM, GY));
            double D = Helpers.Sin(B) * Helpers.Cos(C) + Helpers.Cos(B) * Helpers.Sin(C) * Helpers.Sin(A);
            return Helpers.toDegrees(Helpers.Asin(D));

        }

        public static double TrueAnomaly(double AM, double EC)
        {
            double TP = 6.283185308d;
            double M = AM - TP * Helpers.Int(AM / TP);
            double AE = M;
            here1:
            double D = AE - (EC * Helpers.Sin(AE)) - M;
            if (Helpers.Abs(D) < 0.000001d)
            {
                goto here2;
            }
            else { }
            D = D / (1d - (EC * Helpers.Cos(AE)));
            AE = AE - D;
            goto here1;

            here2:
            double A = Helpers.Sqrt((1d + EC) / (1d - EC)) * Helpers.Tan(AE / 2d);
            double AT = 2d * Helpers.Atan(A);
            return AT;
        }

        public static double EccentricAnomaly(double AM, double EC)
        {
            double TP = 6.283185308;
            double M = AM - TP * Helpers.Int(AM / TP);
            double AE = M;
            here1:
            double D = AE - (EC * Helpers.Sin(AE)) - M;

            if (Helpers.Abs(D) < 0.000001d)
            {
                goto here2;
            }
            else { }

            D = D / (1d - (EC * Helpers.Cos(AE)));
            AE = AE - D;
            goto here1;

            here2:
            return AE;
        }

        public static double SunLong(double LCH, double LCM, double LCS, double DS, double ZC, double LD, double LM, double LY)
        {
            double AA = Time.LDTtoGCDay(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double BB = Time.LDTtoGCMonth(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double CC = Time.LDTtoGCYear(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double UT = Time.LDTtoUT(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double DJ = Time.GCD_JD(AA, BB, CC) - 2415020d;
            double T = (DJ / 36525d) + (UT / 876600d);
            double T2 = T * T;
            double A = 100.0021359d * T;
            double B = 360d * (A - Helpers.Int(A));
            double L = 279.69668d + 0.0003025d * T2 + B;
            A = 99.99736042d * T;
            B = 360d * (A - Helpers.Int(A));
            double M1 = 358.47583d - (0.00015d + 0.0000033d * T) * T2 + B;
            double EC = 0.01675104 - 0.0000418 * T - 0.000000126 * T2;

            double AM = Helpers.toRadians(M1);
            double AT = TrueAnomaly(AM, EC);
            double AE = EccentricAnomaly(AM, EC);

            A = 62.55209472 * T;
            B = 360d * (A - Helpers.Int(A));
            double A1 = Helpers.toRadians(153.23d + B);
            A = 125.1041894 * T;
            B = 360d * (A - Helpers.Int(A));
            double B1 = Helpers.toRadians(216.57d + B);
            A = 91.56766028d * T;
            B = 360d * (A - Helpers.Int(A));
            double C1 = Helpers.toRadians(312.69d + B);
            A = 1236.853095d * T;
            B = 360d * (A - Helpers.Int(A));
            double D1 = Helpers.toRadians(350.74d - 0.00144d * T2 + B);
            double E1 = Helpers.toRadians(231.19d + 20.2d * T);
            A = 183.1353208d * T;
            B = 360d * (A - Helpers.Int(A));
            double H1 = Helpers.toRadians(353.4d + B);

            double D2 = 0.00134d * Helpers.Cos(A1) + 0.00154d * Helpers.Cos(B1) + 0.002d * Helpers.Cos(C1);
            D2 = D2 + 0.00179 * Helpers.Sin(D1) + 0.00178 * Helpers.Sin(E1);
            double D3 = 0.00000543 * Helpers.Sin(A1) + 0.00001575 * Helpers.Sin(B1);
            D3 = D3 + 0.00001627 * Helpers.Sin(C1) + 0.00003076 * Helpers.Cos(D1);
            D3 = D3 + 0.00000927 * Helpers.Sin(H1);

            double SR = AT + Helpers.toRadians(L - M1 + D2);
            double TP = 6.283185308d;
            SR = SR - TP * Helpers.Int(SR / TP);
            return Helpers.toDegrees(SR);

        }

        public static double SunMeanAnomaly(double LCH, double LCM, double LCS, double DS, double ZC, double LD, double LM, double LY)
        {
            double AA = Time.LDTtoGCDay(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double BB = Time.LDTtoGCMonth(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double CC = Time.LDTtoGCYear(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double UT = Time.LDTtoUT(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double DJ = Time.GCD_JD(AA, BB, CC) - 2415020.0;
            double T = (DJ / 36525.0) + (UT / 876600.0);
            double T2 = T * T;
            double A = 100.0021359 * T;
            double B = 360.0 * (A - Helpers.Int(A));
            double M1 = 358.47583 - (0.00015 + 3.3E-06 * T) * T2 + B;
            double AM = Helpers.Unwind(Helpers.toRadians(M1));
            return AM;
        }

        public static double SunDist(double LCH, double LCM, double LCS, double DS, double ZC, double LD, double LM, double LY)
        {
            double AA = Time.LDTtoGCDay(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double BB = Time.LDTtoGCMonth(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double CC = Time.LDTtoGCYear(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double UT = Time.LDTtoUT(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double DJ = Time.GCD_JD(AA, BB, CC) - 2415020.0;
            double T = (DJ / 36525.0) + (UT / 876600.0);
            double T2 = T * T;
            double A = 100.0021359 * T;
            double B = 360.0 * (A - Helpers.Int(A));
            double L = 279.69668 + 0.0003025 * T2 + B;
            A = 99.99736042 * T;
            B = 360.0 * (A - Helpers.Int(A));
            double M1 = 358.47583 - (0.00015 + 3.3E-06 * T) * T2 + B;
            double EC = 0.01675104 - 4.18E-05 * T - 1.26E-07 * T2;

            double AM = Helpers.toRadians(M1);
            double AT = TrueAnomaly(AM, EC);
            double AE = EccentricAnomaly(AM, EC);

            A = 62.55209472 * T;
            B = 360.0 * (A - Helpers.Int(A));
            double A1 = Helpers.toRadians(153.23 + B);
            A = 125.1041894 * T;
            B = 360.0 * (A - Helpers.Int(A));
            double B1 = Helpers.toRadians(216.57 + B);
            A = 91.56766028 * T;
            B = 360.0 * (A - Helpers.Int(A));
            double C1 = Helpers.toRadians(312.69 + B);
            A = 1236.853095 * T;
            B = 360.0 * (A - Helpers.Int(A));
            double D1 = Helpers.toRadians(350.74 - 0.00144 * T2 + B);
            double E1 = Helpers.toRadians(231.19 + 20.2 * T);
            A = 183.1353208 * T;
            B = 360.0 * (A - Helpers.Int(A));
            double H1 = Helpers.toRadians(353.4 + B);

            double D2 = 0.00134 * Helpers.Cos(A1) + 0.00154 * Helpers.Cos(B1) + 0.002 * Helpers.Cos(C1);
            D2 = D2 + 0.00179 * Helpers.Sin(D1) + 0.00178 * Helpers.Sin(E1);
            double D3 = 5.43E-06 * Helpers.Sin(A1) + 1.575E-05 * Helpers.Sin(B1);
            D3 = D3 + 1.627E-05 * Helpers.Sin(C1) + 3.076E-05 * Helpers.Cos(D1);
            D3 = D3 + 9.27E-06 * Helpers.Sin(H1);

            double RR = 1.0000002 * (1.0 - EC * Helpers.Cos(AE)) + D3;
            return RR;
        }

        public static Structs.EclCoord PlanetLonLat(double LCH, double LCM, double LCS, double DS, double ZC, double DY, double MN, double YR, string S)
        {
            #region constants
            const double a11 = 178.179078d, a12 = 415.2057519d, a13 = 0.0003011d, a14 = 0d;
            const double a21 = 75.899697d, a22 = 1.5554889d, a23 = 0.0002947d, a24 = 0d;
            const double a31 = 0.20561421d, a32 = 0.00002046d, a33 = -0.00000003d, a34 = 0d;
            const double a41 = 7.002881d, a42 = 0.0018608d, a43 = -0.0000183d, a44 = 0d;
            const double a51 = 47.145944d, a52 = 1.1852083d, a53 = 0.0001739d, a54 = 0d;
            const double a61 = 0.3870986d, a62 = 6.74d, a63 = -0.42d;

            const double b11 = 342.767053d, b12 = 162.5533664d, b13 = 0.0003097d, b14 = 0d;
            const double b21 = 130.163833d, b22 = 1.4080361d, b23 = -0.0009764d, b24 = 0d;
            const double b31 = 0.00682069d, b32 = -0.00004774d, b33 = 0.000000091d, b34 = 0d;
            const double b41 = 3.393631d, b42 = 0.0010058d, b43 = -0.000001d, b44 = 0d;
            const double b51 = 75.779647d, b52 = 0.89985d, b53 = 0.00041d, b54 = 0d;
            const double b61 = 0.7233316d, b62 = 16.92d, b63 = -4.4d;

            const double c11 = 293.737334d, c12 = 53.17137642d, c13 = 0.0003107d, c14 = 0d;
            const double c21 = 334.218203d, c22 = 1.8407584d, c23 = 0.0001299d, c24 = -0.00000119d;
            const double c31 = 0.0933129d, c32 = 0.000092064d, c33 = -0.000000077d, c34 = 0d;
            const double c41 = 1.850333d, c42 = -0.000675d, c43 = 0.0000126d, c44 = 0d;
            const double c51 = 48.786442d, c52 = 0.7709917d, c53 = -0.0000014d, c54 = -0.00000533d;
            const double c61 = 1.5236883d, c62 = 9.36d, c63 = -1.52d;

            const double d11 = 238.049257d, d12 = 8.434172183d, d13 = 0.0003347d, d14 = -0.00000165d;
            const double d21 = 12.720972d, d22 = 1.6099617d, d23 = 0.00105627d, d24 = -0.00000343d;
            const double d31 = 0.04833475d, d32 = 0.00016418d, d33 = -0.0000004676d, d34 = -0.0000000017d;
            const double d41 = 1.308736d, d42 = -0.0056961d, d43 = 0.0000039d, d44 = 0d;
            const double d51 = 99.443414d, d52 = 1.01053d, d53 = 0.00035222d, d54 = -0.00000851d;
            const double d61 = 5.202561d, d62 = 196.74d, d63 = -9.4;

            const double e11 = 266.564377d, e12 = 3.398638567d, e13 = 0.0003245d, e14 = -0.0000058d;
            const double e21 = 91.098214d, e22 = 1.9584158d, e23 = 0.00082636d, e24 = 0.00000461d;
            const double e31 = 0.05589232d, e32 = -0.0003455d, e33 = -0.000000728d, e34 = 0.00000000074d;
            const double e41 = 2.492519d, e42 = -0.0039189d, e43 = -0.00001549d, e44 = 0.00000004d;
            const double e51 = 112.790414d, e52 = 0.8731951d, e53 = -0.00015218d, e54 = -0.00000531d;
            const double e61 = 9.554747d, e62 = 165.6d, e63 = -8.88d;

            const double f11 = 244.19747d, f12 = 1.194065406d, f13 = 0.000316d, f14 = -0.0000006d;
            const double f21 = 171.548692d, f22 = 1.4844328d, f23 = 0.0002372d, f24 = -0.00000061d;
            const double f31 = 0.0463444d, f32 = -0.00002658d, f33 = 0.000000077d, f34 = 0d;
            const double f41 = 0.772464d, f42 = 0.0006253d, f43 = 0.0000395d, f44 = 0d;
            const double f51 = 73.477111d, f52 = 0.4986678d, f53 = 0.0013117d, f54 = 0d;
            const double f61 = 19.21814d, f62 = 65.8d, f63 = -7.19d;

            const double g11 = 84.457994d, g12 = 0.6107942056d, g13 = 0.0003205d, g14 = -0.0000006d;
            const double g21 = 46.727364d, g22 = 1.4245744d, g23 = 0.00039082d, g24 = -0.000000605d;
            const double g31 = 0.00899704d, g32 = 0.00000633d, g33 = -0.000000002d, g34 = 0d;
            const double g41 = 1.779242d, g42 = -0.0095436d, g43 = -0.0000091d, g44 = 0d;
            const double g51 = 130.681389d, g52 = 1.098935d, g53 = 0.00024987d, g54 = -0.000004718d;
            const double g61 = 30.10957d, g62 = 62.2d, g63 = -6.87;
            #endregion

            int IP, I, J; // K;
            double[,] PL = new double[8, 10];
            double[] AP = new double[8];

            IP = 0;
            double B = Time.LDTtoUT(LCH, LCM, LCS, DS, ZC, DY, MN, YR);
            double GD = Time.LDTtoGCDay(LCH, LCM, LCS, DS, ZC, DY, MN, YR);
            double GM = Time.LDTtoGCMonth(LCH, LCM, LCS, DS, ZC, DY, MN, YR);
            double GY = Time.LDTtoGCYear(LCH, LCM, LCS, DS, ZC, DY, MN, YR);
            double A = Time.GCD_JD(GD, GM, GY);
            double T = ((A - 2415020d) / 36525d) + (B / 876600d);

            switch (S.ToUpper())
            #region -
            {
                case "MERCURY":
                    IP = 1;
                    break;
                case "VENUS":
                    IP = 2;
                    break;
                case "MARS":
                    IP = 3;
                    break;
                case "JUPITER":
                    IP = 4;
                    break;
                case "SATURN":
                    IP = 5;
                    break;
                case "URANUS":
                    IP = 6;
                    break;
                case "NEPTUNE":
                    IP = 7;
                    break;
                default:
                    IP = 0;
                    break;
            }
            #endregion

            #region build matrices
            I = 1;
            double A0 = a11, A1 = a12, A2 = a13, A3 = a14;
            double B0 = a21, B1 = a22, B2 = a23, B3 = a24;
            double C0 = a31, C1 = a32, C2 = a33, C3 = a34;
            double D0 = a41, D1 = a42, D2 = a43, D3 = a44;
            double E0 = a51, E1 = a52, E2 = a53, E3 = a54;
            double F = a61;
            double G = a62;
            double H = a63;


            double AA = A1 * T;
            B = 360d * (AA - Helpers.Int(AA));
            double C = A0 + B + (A3 * T + A2) * T * T;
            PL[I, 1] = C - 360d * Helpers.Int(C / 360d);
            PL[I, 2] = (A1 * 0.009856263d) + (A2 + A3) / 36525d;
            PL[I, 3] = ((B3 * T + B2) * T + B1) * T + B0;
            PL[I, 4] = ((C3 * T + C2) * T + C1) * T + C0;
            PL[I, 5] = ((D3 * T + D2) * T + D1) * T + D0;
            PL[I, 6] = ((E3 * T + E2) * T + E1) * T + E0;
            PL[I, 7] = F;
            PL[I, 8] = G;
            PL[I, 9] = H;

            I = 2;
            A0 = b11; A1 = b12; A2 = b13; A3 = b14;
            B0 = b21; B1 = b22; B2 = b23; B3 = b24;
            C0 = b31; C1 = b32; C2 = b33; C3 = b34;
            D0 = b41; D1 = b42; D2 = b43; D3 = b44;
            E0 = b51; E1 = b52; E2 = b53; E3 = b54;
            F = b61; G = b62; H = b63;

            AA = A1 * T;
            B = 360d * (AA - Helpers.Int(AA));
            C = A0 + B + (A3 * T + A2) * T * T;
            PL[I, 1] = C - 360d * Helpers.Int(C / 360d);
            PL[I, 2] = (A1 * 0.009856263d) + (A2 + A3) / 36525d;
            PL[I, 3] = ((B3 * T + B2) * T + B1) * T + B0;
            PL[I, 4] = ((C3 * T + C2) * T + C1) * T + C0;
            PL[I, 5] = ((D3 * T + D2) * T + D1) * T + D0;
            PL[I, 6] = ((E3 * T + E2) * T + E1) * T + E0;
            PL[I, 7] = F;
            PL[I, 8] = G;
            PL[I, 9] = H;


            I = 3;
            A0 = c11; A1 = c12; A2 = c13; A3 = c14;
            B0 = c21; B1 = c22; B2 = c23; B3 = c24;
            C0 = c31; C1 = c32; C2 = c33; C3 = c34;
            D0 = c41; D1 = c42; D2 = c43; D3 = c44;
            E0 = c51; E1 = c52; E2 = c53; E3 = c54;
            F = c61; G = c62; H = c63;

            AA = A1 * T;
            B = 360d * (AA - Helpers.Int(AA));
            C = A0 + B + (A3 * T + A2) * T * T;
            PL[I, 1] = C - 360d * Helpers.Int(C / 360d);
            PL[I, 2] = (A1 * 0.009856263d) + (A2 + A3) / 36525d;
            PL[I, 3] = ((B3 * T + B2) * T + B1) * T + B0;
            PL[I, 4] = ((C3 * T + C2) * T + C1) * T + C0;
            PL[I, 5] = ((D3 * T + D2) * T + D1) * T + D0;
            PL[I, 6] = ((E3 * T + E2) * T + E1) * T + E0;
            PL[I, 7] = F;
            PL[I, 8] = G;
            PL[I, 9] = H;


            I = 4;
            A0 = d11; A1 = d12; A2 = d13; A3 = d14;
            B0 = d21; B1 = d22; B2 = d23; B3 = d24;
            C0 = d31; C1 = d32; C2 = d33; C3 = d34;
            D0 = d41; D1 = d42; D2 = d43; D3 = d44;
            E0 = d51; E1 = d52; E2 = d53; E3 = d54;
            F = d61; G = d62; H = d63;

            AA = A1 * T;
            B = 360d * (AA - Helpers.Int(AA));
            C = A0 + B + (A3 * T + A2) * T * T;
            PL[I, 1] = C - 360d * Helpers.Int(C / 360d);
            PL[I, 2] = (A1 * 0.009856263) + (A2 + A3) / 36525d;
            PL[I, 3] = ((B3 * T + B2) * T + B1) * T + B0;
            PL[I, 4] = ((C3 * T + C2) * T + C1) * T + C0;
            PL[I, 5] = ((D3 * T + D2) * T + D1) * T + D0;
            PL[I, 6] = ((E3 * T + E2) * T + E1) * T + E0;
            PL[I, 7] = F;
            PL[I, 8] = G;
            PL[I, 9] = H;


            I = 5;
            A0 = e11; A1 = e12; A2 = e13; A3 = e14;
            B0 = e21; B1 = e22; B2 = e23; B3 = e24;
            C0 = e31; C1 = e32; C2 = e33; C3 = e34;
            D0 = e41; D1 = e42; D2 = e43; D3 = e44;
            E0 = e51; E1 = e52; E2 = e53; E3 = e54;
            F = e61; G = e62; H = e63;

            AA = A1 * T;
            B = 360d * (AA - Helpers.Int(AA));
            C = A0 + B + (A3 * T + A2) * T * T;
            PL[I, 1] = C - 360d * Helpers.Int(C / 360d);
            PL[I, 2] = (A1 * 0.009856263d) + (A2 + A3) / 36525d;
            PL[I, 3] = ((B3 * T + B2) * T + B1) * T + B0;
            PL[I, 4] = ((C3 * T + C2) * T + C1) * T + C0;
            PL[I, 5] = ((D3 * T + D2) * T + D1) * T + D0;
            PL[I, 6] = ((E3 * T + E2) * T + E1) * T + E0;
            PL[I, 7] = F;
            PL[I, 8] = G;
            PL[I, 9] = H;


            I = 6;
            A0 = f11; A1 = f12; A2 = f13; A3 = f14;
            B0 = f21; B1 = f22; B2 = f23; B3 = f24;
            C0 = f31; C1 = f32; C2 = f33; C3 = f34;
            D0 = f41; D1 = f42; D2 = f43; D3 = f44;
            E0 = f51; E1 = f52; E2 = f53; E3 = f54;
            F = f61; G = f62; H = f63;

            AA = A1 * T;
            B = 360d * (AA - Helpers.Int(AA));
            C = A0 + B + (A3 * T + A2) * T * T;
            PL[I, 1] = C - 360d * Helpers.Int(C / 360d);
            PL[I, 2] = (A1 * 0.009856263d) + (A2 + A3) / 36525d;
            PL[I, 3] = ((B3 * T + B2) * T + B1) * T + B0;
            PL[I, 4] = ((C3 * T + C2) * T + C1) * T + C0;
            PL[I, 5] = ((D3 * T + D2) * T + D1) * T + D0;
            PL[I, 6] = ((E3 * T + E2) * T + E1) * T + E0;
            PL[I, 7] = F;
            PL[I, 8] = G;
            PL[I, 9] = H;


            I = 7;
            A0 = g11; A1 = g12; A2 = g13; A3 = g14;
            B0 = g21; B1 = g22; B2 = g23; B3 = g24;
            C0 = g31; C1 = g32; C2 = g33; C3 = g34;
            D0 = g41; D1 = g42; D2 = g43; D3 = g44;
            E0 = g51; E1 = g52; E2 = g53; E3 = g54;
            F = g61; G = g62; H = g63;

            AA = A1 * T;
            B = 360d * (AA - Helpers.Int(AA));
            C = A0 + B + (A3 * T + A2) * T * T;
            PL[I, 1] = C - 360d * Helpers.Int(C / 360d);
            PL[I, 2] = (A1 * 0.009856263d) + (A2 + A3) / 36525d;
            PL[I, 3] = ((B3 * T + B2) * T + B1) * T + B0;
            PL[I, 4] = ((C3 * T + C2) * T + C1) * T + C0;
            PL[I, 5] = ((D3 * T + D2) * T + D1) * T + D0;
            PL[I, 6] = ((E3 * T + E2) * T + E1) * T + E0;
            PL[I, 7] = F;
            PL[I, 8] = G;
            PL[I, 9] = H;
            #endregion


            double LI = 0; double TP = 2d * System.Math.PI;
            double MS = SunMeanAnomaly(LCH, LCM, LCS, DS, ZC, DY, MN, YR);
            double SR = Helpers.toRadians(SunLong(LCH, LCM, LCS, DS, ZC, DY, MN, YR));
            double RE = SunDist(LCH, LCM, LCS, DS, ZC, DY, MN, YR); double LG = SR + System.Math.PI;

            //declaration has to be outside of local scope of loops!
            double QA, QB, QC, QD, QE, QF, QG;
            double SA, CA;
            double VK;
            double J1, J2, J3, J4, J5, J6;
            double J8, J9, VJ, UU, UV, UW;
            double JA, JB, JC;
            double VD, VE, VF, VG;

            double EC, AM, AT, PVV, LP, OM, LO, SO, CO, INN, SP, Y, PS, PD;
            double CI, RD = 0, RH;
            double LL = 0;
            double L0, V0, S0, P0;

            double L1, l2;

            double EP;
            double BP;



            //For K = 1 To 2: Loop didn't make sense, probably legacy or artificial scope creation          

            for (J = 1; J < 8; J++)
            {
                AP[J] = Helpers.toRadians(PL[J, 1] - PL[J, 3] - LI * PL[J, 2]);
            }

            QA = 0; QB = 0; QC = 0; QD = 0; QE = 0; QF = 0; QG = 0;

            #region IP switch               
            switch (IP)
            {
                case 1:
                    #region ...
                    //L4685
                    QA = QA = 0.00204d * Helpers.Cos(5 * AP[2] - 2 * AP[1] + 0.21328d);
                    QA = QA + 0.00103d * Helpers.Cos(2 * AP[2] - AP[1] - 2.8046d);
                    QA = QA + 0.00091d * Helpers.Cos(2 * AP[4] - AP[1] - 0.64582d);
                    QA = QA + 0.00078d * Helpers.Cos(5 * AP[2] - 3 * AP[1] + 0.17692d);

                    QB = 0.000007525d * Helpers.Cos(2 * AP[4] - AP[1] + 0.925251d);
                    QB = QB + 0.000006802d * Helpers.Cos(5 * AP[2] - 3 * AP[1] - 4.53642d);
                    QB = QB + 0.000005457d * Helpers.Cos(2 * AP[2] - 2 * AP[1] - 1.24246d);
                    QB = QB + 0.000003569d * Helpers.Cos(5 * AP[2] - AP[1] - 1.35699d);
                    //end of L4685 (RETURN)
                    break;
                #endregion
                case 2:
                    #region ...
                    //L4735
                    QC = 0.00077d * Helpers.Sin(4.1406d + T * 2.6227d); QC = Helpers.toRadians(QC); QE = QC;

                    QA = 0.00313d * Helpers.Cos(2 * MS - 2 * AP[2] - 2.587d);
                    QA = QA + 0.00198d * Helpers.Cos(3 * MS - 3 * AP[2] + 0.044768d);
                    QA = QA + 0.00136d * Helpers.Cos(MS - AP[2] - 2.0788d);
                    QA = QA + 0.00096d * Helpers.Cos(3 * MS - 2 * AP[2] - 2.3721d);
                    QA = QA + 0.00082d * Helpers.Cos(AP[4] - AP[2] - 3.6318d);

                    QB = 0.000022501d * Helpers.Cos(2 * MS - 2 * AP[2] - 1.01592d);
                    QB = QB + 0.000019045d * Helpers.Cos(3 * MS - 3 * AP[2] + 1.61577d);
                    QB = QB + 0.000006887d * Helpers.Cos(AP[4] - AP[2] - 2.06106d);
                    QB = QB + 0.000005172d * Helpers.Cos(MS - AP[2] - 0.508065d);
                    QB = QB + 0.00000362d * Helpers.Cos(5 * MS - 4 * AP[2] - 1.81877d);
                    QB = QB + 0.000003283d * Helpers.Cos(4 * MS - 4 * AP[2] + 1.10851d);
                    QB = QB + 0.000003074d * Helpers.Cos(2 * AP[4] - 2 * AP[2] - 0.962846d);
                    //end of L4735 (RETURN)
                    break;
                #endregion
                case 3:
                    #region ...
                    //L4810
                    A = 3 * AP[4] - 8 * AP[3] + 4 * MS; SA = Helpers.Sin(A); CA = Helpers.Cos(A);
                    QC = -(0.01133 * SA + 0.00933 * CA); QC = Helpers.toRadians(QC); QE = QC;

                    QA = 0.00705d * Helpers.Cos(AP[4] - AP[3] - 0.85448d);
                    QA = QA + 0.00607d * Helpers.Cos(2 * AP[4] - AP[3] - 3.2873d);
                    QA = QA + 0.00445d * Helpers.Cos(2 * AP[4] - 2 * AP[3] - 3.3492d);
                    QA = QA + 0.00388d * Helpers.Cos(MS - 2 * AP[3] + 0.35771d);
                    QA = QA + 0.00238d * Helpers.Cos(MS - AP[3] + 0.61256d);
                    QA = QA + 0.00204d * Helpers.Cos(2 * MS - 3 * AP[3] + 2.7688d);
                    QA = QA + 0.00177d * Helpers.Cos(3 * AP[3] - AP[2] - 1.0053d);
                    QA = QA + 0.00136d * Helpers.Cos(2 * MS - 4 * AP[3] + 2.6894d);
                    QA = QA + 0.00104d * Helpers.Cos(AP[4] + 0.30749d);

                    QB = 0.000053227d * Helpers.Cos(AP[4] - AP[3] + 0.717864d);
                    QB = QB + 0.000050989d * Helpers.Cos(2 * AP[4] - 2 * AP[3] - 1.77997d);
                    QB = QB + 0.000038278d * Helpers.Cos(2 * AP[4] - AP[3] - 1.71617d);
                    QB = QB + 0.000015996d * Helpers.Cos(MS - AP[3] - 0.969618d);
                    QB = QB + 0.000014764d * Helpers.Cos(2 * MS - 3 * AP[3] + 1.19768d);
                    QB = QB + 0.000008966d * Helpers.Cos(AP[4] - 2 * AP[3] + 0.761225d);
                    QB = QB + 0.000007914d * Helpers.Cos(3 * AP[4] - 2 * AP[3] - 2.43887d);
                    QB = QB + 0.000007004d * Helpers.Cos(2 * AP[4] - 3 * AP[3] - 1.79573d);
                    QB = QB + 0.00000662d * Helpers.Cos(MS - 2 * AP[3] + 1.97575d);
                    QB = QB + 0.00000493d * Helpers.Cos(3 * AP[4] - 3 * AP[3] - 1.33069d);
                    QB = QB + 0.000004693d * Helpers.Cos(3 * MS - 5 * AP[3] + 3.32665d);
                    QB = QB + 0.000004571d * Helpers.Cos(2 * MS - 4 * AP[3] + 4.27086d);
                    QB = QB + 0.000004409d * Helpers.Cos(3 * AP[4] - AP[3] - 2.02158d);
                    //end of L4810 (RETURN)
                    break;
                #endregion
                case 4:
                    #region ...
                    //L4945
                    J1 = T / 5d + 0.1; J2 = Helpers.Unwind(4.14473 + 52.9691 * T);
                    J3 = Helpers.Unwind(4.641118 + 21.32991 * T);
                    J4 = Helpers.Unwind(4.250177 + 7.478172 * T);
                    J5 = 5d * J3 - 2d * J2; J6 = 2d * J2 - 6d * J3 + 3d * J4;
                    //...L4980
                    double J7 = J3 - J2, U1 = Helpers.Sin(J3), U2 = Helpers.Cos(J3), U3 = Helpers.Sin(2d * J3);
                    double U4 = Helpers.Cos(2d * J3), U5 = Helpers.Sin(J5), U6 = Helpers.Cos(J5);
                    double U7 = Helpers.Sin(2d * J5), U8 = Helpers.Sin(J6), U9 = Helpers.Sin(J7);
                    double UA = Helpers.Cos(J7), UB = Helpers.Sin(2d * J7), UC = Helpers.Cos(2d * J7);
                    double UD = Helpers.Sin(3d * J7), UE = Helpers.Cos(3d * J7), UF = Helpers.Sin(4d * J7);
                    double UG = Helpers.Cos(4d * J7), VH = Helpers.Cos(5d * J7);
                    //...
                    QC = (0.331364 - (0.010281 + 0.004692 * J1) * J1) * U5;
                    QC = QC + (0.003228 - (0.064436 - 0.002075 * J1) * J1) * U6;
                    QC = QC - (0.003083 + (0.000275 - 0.000489 * J1) * J1) * U7;
                    QC = QC + 0.002472 * U8 + 0.013619 * U9 + 0.018472 * UB;
                    QC = QC + 0.006717 * UD + 0.002775 * UF + 0.006417 * UB * U1;
                    QC = QC + (0.007275 - 0.001253 * J1) * U9 * U1 + 0.002439 * UD * U1;
                    QC = QC - (0.035681 + 0.001208 * J1) * U9 * U2 - 0.003767 * UC * U1;
                    QC = QC - (0.033839 + 0.001125 * J1) * UA * U1 - 0.004261 * UB * U2;
                    QC = QC + (0.001161 * J1 - 0.006333) * UA * U2 + 0.002178 * U2;
                    QC = QC - 0.006675 * UC * U2 - 0.002664 * UE * U2 - 0.002572 * U9 * U3;
                    QC = QC - 0.003567 * UB * U3 + 0.002094 * UA * U4 + 0.003342 * UC * U4;
                    QC = Helpers.toRadians(QC);

                    QD = (3606 + (130 - 43 * J1) * J1) * U5 + (1289 - 580 * J1) * U6;
                    QD = QD - 6764 * U9 * U1 - 1110 * UB * U1 - 224 * UD * U1 - 204 * U1;
                    QD = QD + (1284 + 116 * J1) * UA * U1 + 188 * UC * U1;
                    QD = QD + (1460 + 130 * J1) * U9 * U2 + 224 * UB * U2 - 817 * U2;
                    QD = QD + 6074 * U2 * UA + 992 * UC * U2 + 508 * UE * U2 + 230 * UG * U2;
                    QD = QD + 108 * VH * U2 - (956 + 73 * J1) * U9 * U3 + 448 * UB * U3;
                    QD = QD + 137 * UD * U3 + (108 * J1 - 997) * UA * U3 + 480 * UC * U3;
                    QD = QD + 148 * UE * U3 + (99 * J1 - 956) * U9 * U4 + 490 * UB * U4;
                    QD = QD + 158 * UD * U4 + 179 * U4 + (1024 + 75 * J1) * UA * U4;
                    QD = QD - 437 * UC * U4 - 132 * UE * U4; QD = QD * 0.0000001;

                    VK = (0.007192 - 0.003147 * J1) * U5 - 0.004344 * U1;
                    VK = VK + (J1 * (0.000197 * J1 - 0.000675) - 0.020428) * U6;
                    VK = VK + 0.034036 * UA * U1 + (0.007269 + 0.000672 * J1) * U9 * U1;
                    VK = VK + 0.005614 * UC * U1 + 0.002964 * UE * U1 + 0.037761 * U9 * U2;
                    VK = VK + 0.006158 * UB * U2 - 0.006603 * UA * U2 - 0.005356 * U9 * U3;
                    VK = VK + 0.002722 * UB * U3 + 0.004483 * UA * U3;
                    VK = VK - 0.002642 * UC * U3 + 0.004403 * U9 * U4;
                    VK = VK - 0.002536 * UB * U4 + 0.005547 * UA * U4 - 0.002689 * UC * U4;
                    QE = QC - (Helpers.toRadians(VK) / PL[IP, 4]);

                    QF = 205 * UA - 263 * U6 + 693 * UC + 312 * UE + 147 * UG + 299 * U9 * U1;
                    QF = QF + 181 * UC * U1 + 204 * UB * U2 + 111 * UD * U2 - 337 * UA * U2;
                    QF = QF - 111 * UC * U2; QF = QF * 0.000001d;
                    //...end of L4980 (RETURN on L5190)
                    //end of L4945
                    break;
                #endregion
                case 5:
                    #region ...
                    //L4945
                    J1 = T / 5d + 0.1; J2 = Helpers.Unwind(4.14473 + 52.9691 * T);
                    J3 = Helpers.Unwind(4.641118 + 21.32991 * T);
                    J4 = Helpers.Unwind(4.250177 + 7.478172 * T);
                    J5 = 5d * J3 - 2d * J2; J6 = 2d * J2 - 6d * J3 + 3d * J4;
                    //...L4980
                    J7 = J3 - J2; U1 = Helpers.Sin(J3); U2 = Helpers.Cos(J3); U3 = Helpers.Sin(2d * J3);
                    U4 = Helpers.Cos(2d * J3); U5 = Helpers.Sin(J5); U6 = Helpers.Cos(J5);
                    U7 = Helpers.Sin(2d * J5); U8 = Helpers.Sin(J6); U9 = Helpers.Sin(J7);
                    UA = Helpers.Cos(J7); UB = Helpers.Sin(2d * J7); UC = Helpers.Cos(2d * J7);
                    UD = Helpers.Sin(3d * J7); UE = Helpers.Cos(3d * J7); UF = Helpers.Sin(4d * J7);
                    UG = Helpers.Cos(4d * J7); VH = Helpers.Cos(5d * J7);
                    //......L5200
                    double UI = Helpers.Sin(3 * J3), UJ = Helpers.Cos(3 * J3), UK = Helpers.Sin(4 * J3);
                    double UL = Helpers.Cos(4 * J3), VI = Helpers.Cos(2 * J5), UN = Helpers.Sin(5 * J7);
                    J8 = J4 - J3;
                    double UO = Helpers.Sin(2 * J8), UP = Helpers.Cos(2 * J8);
                    double UQ = Helpers.Sin(3 * J8), UR = Helpers.Cos(3 * J8);

                    QC = 0.007581 * U7 - 0.007986 * U8 - 0.148811 * U9;
                    QC = QC - (0.814181 - (0.01815 - 0.016714 * J1) * J1) * U5;
                    QC = QC - (0.010497 - (0.160906 - 0.0041 * J1) * J1) * U6;
                    QC = QC - 0.015208 * UD - 0.006339 * UF - 0.006244 * U1;
                    QC = QC - 0.0165 * UB * U1 - 0.040786 * UB;
                    QC = QC + (0.008931 + 0.002728 * J1) * U9 * U1 - 0.005775 * UD * U1;
                    QC = QC + (0.081344 + 0.003206 * J1) * UA * U1 + 0.015019 * UC * U1;
                    QC = QC + (0.085581 + 0.002494 * J1) * U9 * U2 + 0.014394 * UC * U2;
                    QC = QC + (0.025328 - 0.003117 * J1) * UA * U2 + 0.006319 * UE * U2;
                    QC = QC + 0.006369 * U9 * U3 + 0.009156 * UB * U3 + 0.007525 * UQ * U3;
                    QC = QC - 0.005236 * UA * U4 - 0.007736 * UC * U4 - 0.007528 * UR * U4;
                    QC = Helpers.toRadians(QC);

                    QD = (-7927 + (2548 + 91 * J1) * J1) * U5;
                    QD = QD + (13381 + (1226 - 253 * J1) * J1) * U6 + (248 - 121 * J1) * U7;
                    QD = QD - (305 + 91 * J1) * VI + 412 * UB + 12415 * U1;
                    QD = QD + (390 - 617 * J1) * U9 * U1 + (165 - 204 * J1) * UB * U1;
                    QD = QD + 26599 * UA * U1 - 4687 * UC * U1 - 1870 * UE * U1 - 821 * UG * U1;
                    QD = QD - 377 * VH * U1 + 497 * UP * U1 + (163 - 611 * J1) * U2;
                    QD = QD - 12696 * U9 * U2 - 4200 * UB * U2 - 1503 * UD * U2 - 619 * UF * U2;
                    QD = QD - 268 * UN * U2 - (282 + 1306 * J1) * UA * U2;
                    QD = QD + (-86 + 230 * J1) * UC * U2 + 461 * UO * U2 - 350 * U3;
                    QD = QD + (2211 - 286 * J1) * U9 * U3 - 2208 * UB * U3 - 568 * UD * U3;
                    QD = QD - 346 * UF * U3 - (2780 + 222 * J1) * UA * U3;
                    QD = QD + (2022 + 263 * J1) * UC * U3 + 248 * UE * U3 + 242 * UQ * U3;
                    QD = QD + 467 * UR * U3 - 490 * U4 - (2842 + 279 * J1) * U9 * U4;
                    QD = QD + (128 + 226 * J1) * UB * U4 + 224 * UD * U4;
                    QD = QD + (-1594 + 282 * J1) * UA * U4 + (2162 - 207 * J1) * UC * U4;
                    QD = QD + 561 * UE * U4 + 343 * UG * U4 + 469 * UQ * U4 - 242 * UR * U4;
                    QD = QD - 205 * U9 * UI + 262 * UD * UI + 208 * UA * UJ - 271 * UE * UJ;
                    QD = QD - 382 * UE * UK - 376 * UD * UL; QD = QD * 0.0000001;

                    VK = (0.077108 + (0.007186 - 0.001533 * J1) * J1) * U5;
                    VK = VK - 0.007075 * U9;
                    VK = VK + (0.045803 - (0.014766 + 0.000536 * J1) * J1) * U6;
                    VK = VK - 0.072586 * U2 - 0.075825 * U9 * U1 - 0.024839 * UB * U1;
                    VK = VK - 0.008631 * UD * U1 - 0.150383 * UA * U2;
                    VK = VK + 0.026897 * UC * U2 + 0.010053 * UE * U2;
                    VK = VK - (0.013597 + 0.001719 * J1) * U9 * U3 + 0.011981 * UB * U4;
                    VK = VK - (0.007742 - 0.001517 * J1) * UA * U3;
                    VK = VK + (0.013586 - 0.001375 * J1) * UC * U3;
                    VK = VK - (0.013667 - 0.001239 * J1) * U9 * U4;
                    VK = VK + (0.014861 + 0.001136 * J1) * UA * U4;
                    VK = VK - (0.013064 + 0.001628 * J1) * UC * U4;
                    QE = QC - (Helpers.toRadians(VK) / PL[IP, 4]);

                    QF = 572 * U5 - 1590 * UB * U2 + 2933 * U6 - 647 * UD * U2;
                    QF = QF + 33629 * UA - 344 * UF * U2 - 3081 * UC + 2885 * UA * U2;
                    QF = QF - 1423 * UE + (2172 + 102 * J1) * UC * U2 - 671 * UG;
                    QF = QF + 296 * UE * U2 - 320 * VH - 267 * UB * U3 + 1098 * U1;
                    QF = QF - 778 * UA * U3 - 2812 * U9 * U1 + 495 * UC * U3 + 688 * UB * U1;
                    QF = QF + 250 * UE * U3 - 393 * UD * U1 - 856 * U9 * U4 - 228 * UF * U1;
                    QF = QF + 441 * UB * U4 + 2138 * UA * U1 + 296 * UC * U4 - 999 * UC * U1;
                    QF = QF + 211 * UE * U4 - 642 * UE * U1 - 427 * U9 * UI - 325 * UG * U1;
                    QF = QF + 398 * UD * UI - 890 * U2 + 344 * UA * UJ + 2206 * U9 * U2;
                    QF = QF - 427 * UE * UJ; QF = QF * 0.000001;

                    QG = 0.000747 * UA * U1 + 0.001069 * UA * U2 + 0.002108 * UB * U3;
                    QG = QG + 0.001261 * UC * U3 + 0.001236 * UB * U4 - 0.002075 * UC * U4;
                    QG = Helpers.toRadians(QG);
                    //......end of L5200 (RETURN)
                    //...end of L4980 (RETURN on L5190)
                    //end of L4945
                    break;
                #endregion
                case 6:
                    #region...
                    //L4945
                    J1 = T / 5d + 0.1; J2 = Helpers.Unwind(4.14473 + 52.9691 * T);
                    J3 = Helpers.Unwind(4.641118 + 21.32991 * T);
                    J4 = Helpers.Unwind(4.250177 + 7.478172 * T);
                    J5 = 5d * J3 - 2d * J2; J6 = 2d * J2 - 6d * J3 + 3d * J4;
                    //...L5505
                    J8 = Helpers.Unwind(1.46205 + 3.81337 * T); J9 = 2 * J8 - J4;
                    VJ = Helpers.Sin(J9); UU = Helpers.Cos(J9); UV = Helpers.Sin(2 * J9);
                    UW = Helpers.Cos(2 * J9);
                    //...
                    JA = J4 - J2; JB = J4 - J3; JC = J8 - J4;
                    QC = (0.864319 - 0.001583 * J1) * VJ;
                    QC = QC + (0.082222 - 0.006833 * J1) * UU + 0.036017 * UV;
                    QC = QC - 0.003019 * UW + 0.008122 * Helpers.Sin(J6); QC = Helpers.toRadians(QC);

                    VK = 0.120303 * VJ + 0.006197 * UV;
                    VK = VK + (0.019472 - 0.000947 * J1) * UU;
                    QE = QC - (Helpers.toRadians(VK) / PL[IP, 4]);

                    QD = (163 * J1 - 3349) * VJ + 20981 * UU + 1311 * UW; QD = QD * 0.0000001;

                    QF = -0.003825 * UU;

                    QA = (-0.038581 + (0.002031 - 0.00191 * J1) * J1) * Helpers.Cos(J4 + JB);
                    QA = QA + (0.010122 - 0.000988 * J1) * Helpers.Sin(J4 + JB);
                    A = (0.034964 - (0.001038 - 0.000868 * J1) * J1) * Helpers.Cos(2 * J4 + JB);
                    QA = A + QA + 0.005594 * Helpers.Sin(J4 + 3 * JC) - 0.014808 * Helpers.Sin(JA);
                    QA = QA - 0.005794 * Helpers.Sin(JB) + 0.002347 * Helpers.Cos(JB);
                    QA = QA + 0.009872 * Helpers.Sin(JC) + 0.008803 * Helpers.Sin(2 * JC);
                    QA = QA - 0.004308 * Helpers.Sin(3 * JC);

                    double UX = Helpers.Sin(JB), UY = Helpers.Cos(JB), UZ = Helpers.Sin(J4);
                    double VA = Helpers.Cos(J4), VB = Helpers.Sin(2 * J4), VC = Helpers.Cos(2 * J4);
                    QG = (0.000458 * UX - 0.000642 * UY - 0.000517 * Helpers.Cos(4 * JC)) * UZ;
                    QG = QG - (0.000347 * UX + 0.000853 * UY + 0.000517 * Helpers.Sin(4 * JB)) * VA;
                    QG = QG + 0.000403 * (Helpers.Cos(2 * JC) * VB + Helpers.Sin(2 * JC) * VC);
                    QG = Helpers.toRadians(QG);

                    QB = -25948 + 4985 * Helpers.Cos(JA) - 1230 * VA + 3354 * UY;
                    QB = QB + 904 * Helpers.Cos(2 * JC) + 894 * (Helpers.Cos(JC) - Helpers.Cos(3 * JC));
                    QB = QB + (5795 * VA - 1165 * UZ + 1388 * VC) * UX;
                    QB = QB + (1351 * VA + 5702 * UZ + 1388 * VB) * UY;
                    QB = QB * 0.000001;
                    //...end of L5505 (RETURN) 
                    //end of L4945
                    break;
                #endregion
                case 7:
                    #region ...
                    //L4945
                    J1 = T / 5d + 0.1; J2 = Helpers.Unwind(4.14473 + 52.9691 * T);
                    J3 = Helpers.Unwind(4.641118 + 21.32991 * T);
                    J4 = Helpers.Unwind(4.250177 + 7.478172 * T);
                    J5 = 5d * J3 - 2d * J2; J6 = 2d * J2 - 6d * J3 + 3d * J4;
                    //...L5505
                    J8 = Helpers.Unwind(1.46205 + 3.81337 * T); J9 = 2 * J8 - J4;
                    VJ = Helpers.Sin(J9); UU = Helpers.Cos(J9); UV = Helpers.Sin(2 * J9);
                    UW = Helpers.Cos(2 * J9);
                    //......L5675
                    JA = J8 - J2; JB = J8 - J3; JC = J8 - J4;
                    QC = (0.001089 * J1 - 0.589833) * VJ;
                    QC = QC + (0.004658 * J1 - 0.056094) * UU - 0.024286 * UV;
                    QC = Helpers.toRadians(QC);

                    VK = 0.024039 * VJ - 0.025303 * UU + 0.006206 * UV;
                    VK = VK - 0.005992 * UW; QE = QC - (Helpers.toRadians(VK) / PL[IP, 4]);

                    QD = 4389 * VJ + 1129 * UV + 4262 * UU + 1089 * UW;
                    QD = QD * 0.0000001;

                    QF = 8189 * UU - 817 * VJ + 781 * UW; QF = QF * 0.000001;

                    VD = Helpers.Sin(2 * JC); VE = Helpers.Cos(2 * JC);
                    VF = Helpers.Sin(J8); VG = Helpers.Cos(J8);
                    QA = -0.009556 * Helpers.Sin(JA) - 0.005178 * Helpers.Sin(JB);
                    QA = QA + 0.002572 * VD - 0.002972 * VE * VF - 0.002833 * VD * VG;

                    QG = 0.000336 * VE * VF + 0.000364 * VD * VG; QG = Helpers.toRadians(QG);

                    QB = -40596 + 4992 * Helpers.Cos(JA) + 2744 * Helpers.Cos(JB);
                    QB = QB + 2044 * Helpers.Cos(JC) + 1051 * VE; QB = QB * 0.000001;
                    //......end of L5675 (RETURN)
                    //...end of L5505
                    //end of L4945
                    break;
                #endregion
                case 8:
                    //L4945
                    J1 = T / 5d + 0.1; J2 = Helpers.Unwind(4.14473 + 52.9691 * T);
                    J3 = Helpers.Unwind(4.641118 + 21.32991 * T);
                    J4 = Helpers.Unwind(4.250177 + 7.478172 * T);
                    J5 = 5d * J3 - 2d * J2; J6 = 2d * J2 - 6d * J3 + 3d * J4;
                    //...5190
                    //...end of 5190 (immediate RETURN)
                    break;
                default:
                    break;
            }
            #endregion

            EC = PL[IP, 4] + QD; AM = AP[IP] + QE; AT = TrueAnomaly(AM, EC);
            PVV = (PL[IP, 7] + QF) * (1d - EC * EC) / (1d + EC * Helpers.Cos(AT));
            LP = Helpers.toDegrees(AT) + PL[IP, 3] + Helpers.toDegrees(QC - QE); LP = Helpers.toRadians(LP);
            OM = Helpers.toRadians(PL[IP, 6]); LO = LP - OM;
            SO = Helpers.Sin(LO); CO = Helpers.Cos(LO);
            INN = Helpers.toRadians(PL[IP, 5]); PVV = PVV + QB;
            SP = SO * Helpers.Sin(INN); Y = SO * Helpers.Cos(INN);
            PS = Helpers.Asin(SP) + QG; SP = Helpers.Sin(PS);
            PD = Helpers.Atan2(CO, Y) + OM + Helpers.toRadians(QA);
            PD = Helpers.Unwind(PD);
            CI = Helpers.Cos(PS); RD = PVV * CI;
            LL = PD - LG;
            RH = RE * RE + PVV * PVV - 2d * RE * PVV * CI * Helpers.Cos(LL);
            RH = Helpers.Sqrt(RH); LI = RH * 0.005775518d;

            L0 = PD;
            V0 = RH;
            S0 = PS;
            P0 = PVV;

            L1 = Helpers.Sin(LL);
            l2 = Helpers.Cos(LL);

            if (IP < 3)
            {
                EP = Helpers.Atan(-1d * RD * L1 / (RE - RD * l2)) + LG + System.Math.PI;
            }
            else
            {
                EP = Helpers.Atan(RE * L1 / (RD - RE * l2)) + PD;
            }

            EP = Helpers.Unwind(EP);
            EP = Helpers.toDegrees(Helpers.Unwind(EP)); //maybe delete double unwind?
            BP = Helpers.Atan(RD * SP * Helpers.Sin(EP - PD) / (CI * RE * L1));


            Structs.EclCoord result = new Structs.EclCoord(EP, BP);

            return result;
        }

        public static double RAHA(double RH, double RM, double RS, double LCH, double LCM, double LCS, double DS, double ZC, double LD, double LM, double LY, double L)
        {
            double functionReturnValue = 0;

            double A = Time.LDTtoUT(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double B = Time.LDTtoGCDay(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double C = Time.LDTtoGCMonth(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double D = Time.LDTtoGCYear(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double E = Time.UTGST(A, 0, 0, B, C, D);
            double F = Time.GSTLST(E, 0, 0, L);
            double G = Helpers.toDecHour(RH, RM, RS);
            double H = F - G;
            if ((H < 0))
            {
                functionReturnValue = 24 + H;
            }
            else {
                functionReturnValue = H;
            }
            return functionReturnValue;

        }

        public static double ParallaxHA(double HH, double HM, double HS, double DD, double DM, double DS, bool SW, double GP, double HT, double HP)
        {
            double A = Helpers.toRadians(GP), C1 = Helpers.Cos(A), S1 = Helpers.Sin(A);
            double U = Helpers.Atan(0.996647 * S1 / C1);
            double C2 = Helpers.Cos(U), S2 = Helpers.Sin(U), B = HT / 6378160d;
            double RS = (0.996647 * S2) + (B * S1);
            double RC = C2 + (B * C1), TP = 6.283185308;

            double RP = 1 / Helpers.Sin(Helpers.toRadians(HP));
            double X = Helpers.toRadians(15 * (Helpers.toDecHour(HH, HM, HS))), X1 = X;
            double Y = Helpers.toRadians(Helpers.DMStoDD(DD, DM, DS)), Y1 = Y;

            double result;

            if (SW == true)
            {
                //GoSub L2870
                double CX = Helpers.Cos(X), SY = Helpers.Sin(Y), CY = Helpers.Cos(Y);
                double AA = (RC * Helpers.Sin(X)) / ((RP * CY) - (RC * CX));
                double DX = Helpers.Atan(AA), P = X + DX, CP = Helpers.Cos(P);
                P = P - TP * Helpers.Int(P / TP);
                double Q = Helpers.Atan(CP * (RP * SY - RS) / (RP * CY * CX - RC));

                //GoTo L2895
                result = Helpers.toDegrees(P) / 15;
            }
            else
            {
                double P1 = 0, Q1 = 0;
                //GoSub L2870
                double CX = Helpers.Cos(X), SY = Helpers.Sin(Y), CY = Helpers.Cos(Y);
                double AA = (RC * Helpers.Sin(X)) / ((RP * CY) - (RC * CX));
                double DX = Helpers.Atan(AA), P = X + DX, CP = Helpers.Cos(P);
                P = P - TP * Helpers.Int(P / TP);
                double Q = Helpers.Atan(CP * (RP * SY - RS) / (RP * CY * CX - RC));
                //back
                double P2 = P - X, Q2 = Q - Y;
                AA = Helpers.Abs(P2 - P1);
                double BB = Helpers.Abs(Q2 - Q1);

                if ((AA < 0.000001) && (BB < 0.000001))
                {
                    P = X1 - P2; Q = Y1 - Q2; X = X1; Y = Y1;
                    result = Helpers.toDegrees(P) / 15;
                }
                else
                {
                    X = X1 - P2; Y = Y1 - Q2; P1 = P2; Q1 = Q2;

                    CX = Helpers.Cos(X); SY = Helpers.Sin(Y); CY = Helpers.Cos(Y);
                    AA = (RC * Helpers.Sin(X)) / ((RP * CY) - (RC * CX));
                    DX = Helpers.Atan(AA); P = X + DX; CP = Helpers.Cos(P);
                    P = P - TP * Helpers.Int(P / TP);
                    Q = Helpers.Atan(CP * (RP * SY - RS) / (RP * CY * CX - RC));

                    result = Helpers.toDegrees(P) / 15;
                }
            }
            return result;
        }

        public static double HARA(double HH, double HM, double HS, double LCH, double LCM, double LCS, double DS, double ZC, double LD, double LM, double LY, double L)
        {
            double A = Time.LDTtoUT(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double B = Time.LDTtoGCDay(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double C = Time.LDTtoGCMonth(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double D = Time.LDTtoGCYear(LCH, LCM, LCS, DS, ZC, LD, LM, LY);
            double E = Time.UTGST(A, 0, 0, B, C, D);
            double F = Time.GSTLST(E, 0, 0, L);
            double G = Helpers.toDecHour(HH, HM, HS);
            double H = F - G;
            double result;

            if (H < 0) { result = 24 + H; }
            else { result = H; }

            return result;
        }

        public static double ParallaxDec(double HH, double HM, double HS, double DD, double DM, double DS, bool SW, double GP, double HT, double HP)
        {
            double A = Helpers.toRadians(GP), C1 = Helpers.Cos(A), S1 = Helpers.Sin(A);
            double U = Helpers.Atan(0.996647 * S1 / C1);
            double C2 = Helpers.Cos(U), S2 = Helpers.Sin(U), B = HT / 6378160d;
            double RS = (0.996647 * S2) + (B * S1);
            double RC = C2 + (B * C1), TP = 6.283185308;

            double RP = 1 / Helpers.Sin(Helpers.toRadians(HP));
            double X = Helpers.toRadians(15 * (Helpers.toDecHour(HH, HM, HS))), X1 = X;
            double Y = Helpers.toRadians(Helpers.DMStoDD(DD, DM, DS)), Y1 = Y;

            double result;

            if (SW == true)
            {
                //GoSub L2870
                double CX = Helpers.Cos(X), SY = Helpers.Sin(Y), CY = Helpers.Cos(Y);
                double AA = (RC * Helpers.Sin(X)) / ((RP * CY) - (RC * CX));
                double DX = Helpers.Atan(AA), P = X + DX, CP = Helpers.Cos(P);
                P = P - TP * Helpers.Int(P / TP);
                double Q = Helpers.Atan(CP * (RP * SY - RS) / (RP * CY * CX - RC));

                //GoTo L2895
                result = Helpers.toDegrees(Q);
            }
            else
            {
                double P1 = 0, Q1 = 0;
                //GoSub L2870
                double CX = Helpers.Cos(X), SY = Helpers.Sin(Y), CY = Helpers.Cos(Y);
                double AA = (RC * Helpers.Sin(X)) / ((RP * CY) - (RC * CX));
                double DX = Helpers.Atan(AA), P = X + DX, CP = Helpers.Cos(P);
                P = P - TP * Helpers.Int(P / TP);
                double Q = Helpers.Atan(CP * (RP * SY - RS) / (RP * CY * CX - RC));
                //back
                double P2 = P - X, Q2 = Q - Y;
                AA = Helpers.Abs(P2 - P1);
                double BB = Helpers.Abs(Q2 - Q1);

                if ((AA < 0.000001) && (BB < 0.000001))
                {
                    P = X1 - P2; Q = Y1 - Q2; X = X1; Y = Y1;
                    result = Helpers.toDegrees(Q);
                }
                else
                {
                    X = X1 - P2; Y = Y1 - Q2; P1 = P2; Q1 = Q2;

                    CX = Helpers.Cos(X); SY = Helpers.Sin(Y); CY = Helpers.Cos(Y);
                    AA = (RC * Helpers.Sin(X)) / ((RP * CY) - (RC * CX));
                    DX = Helpers.Atan(AA); P = X + DX; CP = Helpers.Cos(P);
                    P = P - TP * Helpers.Int(P / TP);
                    Q = Helpers.Atan(CP * (RP * SY - RS) / (RP * CY * CX - RC));

                    result = Helpers.toDegrees(Q);
                }
            }
            return result;
        }


    }
}
