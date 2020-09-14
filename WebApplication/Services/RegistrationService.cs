using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class RegistrationService : IRegistrationService
    {
        private readonly List<BusinessModel> _storage = new List<BusinessModel>();

        public bool SaveBusiness(BusinessModel businessModel)
        {
            lock (_storage)
            {
                if (_storage.Any(model => model.Name == businessModel.Name))
                {
                    return false;
                }

                _storage.Add(businessModel);
                return true;
            }
        }

        public bool DeleteBusiness(BusinessModel businessModel)
        {
            lock (businessModel)
            {
                foreach (var model in _storage.Where(model => model.Name == businessModel.Name))
                {
                    _storage.Remove(model);
                    return true;
                }

                return false;
            }
        }

        public List<BusinessModel> GetBusiness(string byOwnerEmail, string byName)
        {
            lock (_storage)
            {
                var retVal = new List<BusinessModel>();
                foreach (var model in _storage)
                {
                    if (model.Name == byName) retVal.Add(model);
                    else if (model.OwnerEmail == byOwnerEmail) retVal.Add(model);
                }

                return retVal;
            }
        }

        public List<BusinessModel> GetAllBusinesses()
        {
            lock (_storage)
            {
                var retVal = new List<BusinessModel>();
                retVal.AddRange(_storage);
                return retVal;
            }
        }
    }
}