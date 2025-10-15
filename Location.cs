namespace HCS;

public enum AllRegions
{
    none,
    Blekinge,
    Dalarna,
    Gotland,
    Gävleborg,
    Halland,
    Jämtland,
    Jönköping,
    Kalmar,
    Kronoberg,
    Norrbotten,
    Skåne,
    Stockholm,
    Sörmanland,
    Uppsala,
    Värmland,
    Västerbotten,
    Västernorrland,
    Västmanland,
    Örebro,
    Östergötland,
    Götalandsregionen,
}
public class Location
{
    public string Reception;

    public AllRegions Region;

    public Location(string reception, AllRegions region)
    {
        Reception = reception;
        Region = region;
    }
}