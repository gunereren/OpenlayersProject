using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IParcelService
    // iş yükünü Business içinde burada yapmamız doğru olacak. Diğer yerlerde IParcelService'i implemente ettiğimiz zaman bu altta
    // yazan fonksiyonları implemente ederek kullanacaz.
    {
        IDataResult<Parcel> GetById(int parcelId);
        IDataResult<List<Parcel>> GetList();
        // mesela burada içeriği "Parcel"(Nesne) olan bir Liste türünde "GetList" fonksiyonu yazıyor. Yani fonksiyon bize bir liste 
        // döndürecek ve listenin elemanları Parcel(Nesne) olacak.
        IResult Add(Parcel parcel);
        IResult Delete(Parcel parcel);
        IResult Update(Parcel parcel);
    }
}
