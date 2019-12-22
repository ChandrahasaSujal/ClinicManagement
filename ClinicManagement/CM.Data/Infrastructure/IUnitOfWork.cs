using CM.Model.Models;
using CM.Model.Models.Account;
using CM.Model.Models.Medicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        ApplicationDbContext ApplicationDbContext { get; }
        DataRepository<ApplicationUser> ApplicationUserRepository { get; }
        DataRepository<Person> PeopleRepository { get;}
        DataRepository<Medicine> MedicineRepository { get;}
        DataRepository<Category> CategoryRepository { get;}
        DataRepository<Manufacturer> ManufacturerRepository { get;}
        void SaveChanges();
    }
}
