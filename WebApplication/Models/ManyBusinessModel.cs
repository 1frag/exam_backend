using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ManyBusinessModel
    {
        public List<BusinessModel> Container { get; set; }

        public ManyBusinessModel(List<BusinessModel> businessModels)
        {
            Container = businessModels;
        }
    }
}