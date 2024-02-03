using System;
namespace Workintechrestapiproje.Business.City
{
    public class CityService : BaseService, ICityService
    {

        public List<string> GetCities()
        {
            var cities = new List<string>()
            {
                "Ankara",
                "İstanbul",
                "İzmir",
                "Adana",
                "Adıyaman",
                "Afyonkarahisar",
                "Ağrı",
            };

            return cities;
        }
    }

}

