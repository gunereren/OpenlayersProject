using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Parcel:IEntity
    {
        public int ParcelId { get; set; }                       // Parsel ID
        public string ParcelCity { get; set; }                  // Parsel Şehir
        public string ParcelDistrict { get; set; }              // Parsel İlçe
        public string ParcelNeighbourhood { get; set; }         // Parsel Mahalle
        public string wkt { get; set; }                         // Parsel WKT
    }
}
