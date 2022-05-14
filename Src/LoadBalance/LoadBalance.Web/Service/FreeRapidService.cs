using LoadBalance.Web.Models.Entity;

namespace LoadBalance.Web.Service
{
    public class FreeRapidService : IFreeRapidService
    {
        public List<FreeRapid> LitedFreeRapid()
        {
            throw new NotImplementedException();
        }
    }

    public class FreeRapidTempSevice : IFreeRapidService
    {
        public List<FreeRapid> LitedFreeRapid()
        {
            return new List<FreeRapid> {
                new FreeRapid{
                    Id = 1,
                    City = "臺北市",
                    Town = "士林區",
                    RapidName ="粱耳鼻喉科診所",
                    Address ="臺北市士林區社正路12-1號",
                    PhoneNumber = "02-28168456",
                    Lat = 121.509303,
                    Long = 25.08855
                },
                new FreeRapid{
                    Id = 2,
                    City = "臺北市",
                    Town = "大同區",
                    RapidName ="大同耳鼻喉科診所",
                    Address ="臺北市大同區重慶北路三段250之4號",
                    PhoneNumber = "02-25975833",
                    Lat = 121.513575,
                    Long = 25.071148
                },
                new FreeRapid{
                    Id = 3,
                    City = "臺北市",
                    Town = "大同區",
                    RapidName ="建順診所",
                    Address ="臺北市大同區甘州街51號",
                    PhoneNumber = "02-25573250",
                    Lat = 121.512854,
                    Long = 25.059677
                }
            };
        }
    }

    public interface IFreeRapidService
    {
        public List<FreeRapid> LitedFreeRapid();
    }
}
