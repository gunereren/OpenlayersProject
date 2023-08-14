using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ParcelManager : IParcelService
    {
        private IParcelDal _parcelDal;

        public ParcelManager(IParcelDal parcelDal)
        {
            _parcelDal = parcelDal;
        }

        public IDataResult<Parcel> GetById(int parcelId)
        {
            return new SuccessDataResult<Parcel>(_parcelDal.Get(p => p.ParcelId == parcelId));
        }

        public IDataResult<List<Parcel>> GetList()
        {
            return new SuccessDataResult<List<Parcel>>(_parcelDal.GetList().ToList());
        }

        public IResult Add(Parcel parcel)
        {
            // Business codes
            _parcelDal.Add(parcel);
            return new SuccessResult(Messages.ParcelAdded);
        }

        public IResult Delete(Parcel parcel)
        {
            _parcelDal.Delete(parcel);
            return new SuccessResult(Messages.ParcelDeleted);
        }

        public IResult Update(Parcel parcel)
        {
            _parcelDal.Update(parcel);
            return new SuccessResult(Messages.ParcelUpdated);
        }
    }
}
