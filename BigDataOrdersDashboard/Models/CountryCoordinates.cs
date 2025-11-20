namespace BigDataOrdersDashboard.Models
{
    public class CountryCoordinates
    {
        private static readonly Dictionary<string, (double Lat, double Lon)> _coords = new()
        {
            { "Türkiye", (39.9208, 32.8541) },
            { "Fransa", (48.8566, 2.3522) },
            { "Almanya", (52.5200, 13.4050) },
            { "İspanya", (40.4168, -3.7038) },
            { "İtalya", (41.9028, 12.4964) },
            { "Hollanda", (52.3676, 4.9041) },
            { "Belçika", (50.8503, 4.3517) },
            { "Avusturya", (48.2100, 16.3700) },
            { "Macaristan", (47.4979, 19.0402) },
            { "Polonya", (52.2297, 21.0122) },
            { "Slovakya", (48.1486, 17.1077) },
            { "Sırbistan", (44.7872, 20.4573) },
            { "Bulgaristan", (42.6977, 23.3219) }
        };

        public static double GetLat(string country)
            => _coords.ContainsKey(country) ? _coords[country].Lat : 0;

        public static double GetLon(string country)
            => _coords.ContainsKey(country) ? _coords[country].Lon : 0;
    }
}