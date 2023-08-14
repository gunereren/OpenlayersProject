using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IParcelDal : IEntityRepository<Parcel>
    // Bunun içerisinde temel veri erişim operasyonlarını oluşturuyor olacaz. (insert, update, delete...)
    {
    }
}
