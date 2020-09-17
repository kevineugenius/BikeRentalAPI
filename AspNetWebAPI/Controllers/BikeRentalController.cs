using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace AspNetWebAPI.Controllers
{
    public class BikeRentalController : ApiController
    {
        private List<BikeData> dataset = new List<BikeData>();

        //https://localhost:44304/api/BikeRental/Get
        public JsonResult<BikeResponse> Get()
        {
            var returnMe = new BikeResponse();
            returnMe.initialDataSet = new List<List<object>>();
            var object1 = new List<object>();
            object1.Add(1);
            object1.Add("Hello");
            returnMe.initialDataSet.Add(object1);

            var object2 = new List<object>();
            object2.Add(2);
            object2.Add("World");
            returnMe.initialDataSet.Add(object2);

            var object3 = new List<object>();
            object3.Add(3);
            object3.Add("Friday Tomorrow!");
            returnMe.initialDataSet.Add(object3);

            return Json(returnMe);
        }

        //https://localhost:44304/api/BikeRental/GetAll
        public JsonResult<List<BikeData>> GetAll()
        {
            if (dataset.Count == 0)
            {
                LoadData();
            }
            return Json(dataset); 
        }

        // https://localhost:44304/api/BikeRental/Get2?request=rentals
        public JsonResult<BikeResponse> Get2(string request)
        {
            if (dataset.Count == 0)
            {
                // Load the dummy data first
                LoadData();
            }
            var resp = new BikeResponse();
            resp.initialDataSet = new List<List<object>>();
            switch (request)
            {
                case "registered": 
                    {
                        
                        break;
                    }
                case "casual":
                    {

                        break;
                    }
                case "rentals":
                    {
                        foreach(var d in dataset)
                        {
                            var obj = new List<object>();
                            obj.Add(d.date.Ticks);
                            obj.Add(d.count);
                            resp.initialDataSet.Add(obj);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return Json(resp);
        }

        private void LoadData()
        {
            try
            {
                var dataAsArray = File.ReadAllLines(
                    HttpContext.Current.Server.MapPath(@"~/hour.csv"));                
                dataset = BikeData.buildFromStrArr(dataAsArray);
            }
            catch (IOException e)
            {
                Console.WriteLine("File could not be read");
                Console.WriteLine(e.Message);
            }
        }
    }

    public class BikeResponse
    {
        public string format = "date";
        public List<List<object>> initialDataSet;
    }

    public class BikeData
    {
        // data from:
        // https://archive.ics.uci.edu/ml/datasets/bike+sharing+dataset
        public int index; //instant
        public DateTime date; //dteday
        public int season; //season, (1:winter, 2:spring, 3:summer, 4:fall)
        public int year; //yr 0: 2011, 1: 2012, etc
        public int month; //mnth 1-12
        public int hour; //hr 0-23
        public int holiday; //holiday, 0: no, 1: yes
        public int weekday; //weekday, 0:sunday - 6:saturday
        public int workingday; //workingday if not weekend or holiday, 1, else 0
        /*  - 1: Clear, Few clouds, Partly cloudy, Partly cloudy
            - 2: Mist + Cloudy, Mist + Broken clouds, Mist + Few clouds, Mist
            - 3: Light Snow, Light Rain + Thunderstorm + Scattered clouds, Light Rain + Scattered clouds
            - 4: Heavy Rain + Ice Pallets + Thunderstorm + Mist, Snow + Fog
         */
        public int weather; //weathersit
        public double temp; //temp C* normalized by (t-t_min)/(t_max-t_min), t_min=-8, t_max=+39
        public double feelslike; //atemp C* normalized including humidity (t-t_min)/(t_max-t_min), t_min=-16, t_max=+50
        public double humidity; //hum decimal representation of percentage
        public double windspeed; //windspeed normalized, divided by 67 (why?)
        public int casual; //casual, number of unregistered users(?)
        public int registered; //registered, num of registered users
        public int count; //cnt, total rental bikes out at the moment

        public static List<BikeData> buildFromStrArr(string[] data)
        {
            List<BikeData> result = new List<BikeData>();
            // skip header line
            for (int i = 1; i < 5; i++) //shortened for ease of view
            {
                var temp = new BikeData();
                var currentLine = data[i].Split(',');
                temp.index = Int32.Parse(currentLine[0]);
                temp.date = Convert.ToDateTime(currentLine[1]);
                temp.season = Int32.Parse(currentLine[2]);
                temp.year = Int32.Parse(currentLine[3]);
                temp.month = Int32.Parse(currentLine[4]);
                temp.holiday = Int32.Parse(currentLine[5]);
                temp.hour = Int32.Parse(currentLine[6]);
                temp.weekday = Int32.Parse(currentLine[7]);
                temp.workingday = Int32.Parse(currentLine[8]);
                temp.weather = Int32.Parse(currentLine[9]);
                temp.temp = Double.Parse(currentLine[10]);
                temp.feelslike = Double.Parse(currentLine[11]);
                temp.humidity = Double.Parse(currentLine[12]);
                temp.windspeed = Double.Parse(currentLine[13]);
                temp.casual = Int32.Parse(currentLine[14]);
                temp.registered = Int32.Parse(currentLine[15]);
                temp.count = Int32.Parse(currentLine[16]);
                result.Add(temp);
            }
            return result;
        }
    }
}
