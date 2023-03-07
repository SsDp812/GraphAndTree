using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_22_3_20
{
    public class City
    {

        public string name;
        public int x, y;
        public City(string name, int x, int y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }
        public void Show()
        {
            Console.WriteLine("{0} {1}, {2}", name, x, y);
        }
        public static int Distance(City firstCity, City secondCity)
        {
            return (int)Math.Sqrt(Math.Pow(secondCity.x - firstCity.x, 2) + Math.Pow(secondCity.y - firstCity.y, 2));
        }
        public int Distance(City secondCity)
        {
            return (int)Math.Sqrt(Math.Pow(secondCity.x - this.x, 2) + Math.Pow(secondCity.y - this.y, 2));
        }
    }
}
