using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalWithDB
{
    class Medicine
    {

        //  Properties  //
        public string Name { get; }
        public string Country { get; }
        public decimal Price { get; private set; }
        public DateTime ExpiryDate { get; }
        public byte[] Picture {   get; private set; }

        // End Properties  //

        //Constructor//

        public Medicine(string name, string country, int price, DateTime expiryDate)
        {
            Name = name;
            Country = country;
            Price = price;
            ExpiryDate = expiryDate;
        }

        //End Constructor//

        // Methods //

        public void AddPicture(byte[] pic)
        {
            Picture = pic;
        }
        // TODO
        public void ChangePrice(decimal newPrice)
        {

        }

        //End Methods //


    }
}
