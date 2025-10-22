namespace HCS;
// enums för att ange i vilken region en användare arbetar, p.g.a. tidbegränsingar och misskommunikation 
// så används dem inte i booking systememt för att kunna se VAR en patient eller en doktor har ett möte
public enum AllRegions
{
    none,
    Blekinge,
    //Dalarna,
    //Gotland,
    //Gävleborg,
    Halland,
    //Jämtland,
    //Jönköping,
    //Kalmar,
    Kronoberg,
    //Norrbotten,
    Skåne,
    //Stockholm,
    //Sörmanland,
    //Uppsala,
    //Värmland,
    //Västerbotten,
    //Västernorrland,
    //Västmanland,
    //Örebro,
    //Östergötland,
    //Götalandsregionen,
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

    public string ToFileString(string reception, AllRegions region)
    {
        return $"{Reception}; {Region}";
    }
}