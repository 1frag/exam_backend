using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IRegistrationService
    {
        public bool SaveBusiness(BusinessModel businessModel);
        public bool DeleteBusiness(BusinessModel businessModel);
        public List<BusinessModel> GetBusiness(string byOwnerEmail, string byName);
        public List<BusinessModel> GetAllBusinesses();
    }
}