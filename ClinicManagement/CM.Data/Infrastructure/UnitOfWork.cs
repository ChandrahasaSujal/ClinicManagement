using CM.Data.Utils;
using CM.Model.Models;
using CM.Model.Models.Account;
using CM.Model.Models.Invoice;
using CM.Model.Models.Medicine;
using System;
using System.Data.Entity;

namespace CM.Data.Infrastructure
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region "Private Member(s)"

        private bool _disposed = false;
        private readonly DbContext _context;
        


        #endregion

        public UnitOfWork()
        {
            _context = ApplicationDbContext.Create();
        }

        #region "Public Member(s)"

        private ApplicationDbContext _applicationDbContext;
        public ApplicationDbContext ApplicationDbContext
        {
            get
            {
                if (_applicationDbContext == null)
                    this._applicationDbContext = _context as ApplicationDbContext;
                return _applicationDbContext;
            }
        }


        private DataRepository<ApplicationUser> _applicationUserRepository;
        public DataRepository<ApplicationUser> ApplicationUserRepository
        {
            get
            {
                if (this._applicationUserRepository == null)
                    this._applicationUserRepository = new DataRepository<ApplicationUser>(ApplicationDbContext);
                return _applicationUserRepository;
            }
        }

        private DataRepository<Person> peopleRepository;

        public DataRepository<Person> PeopleRepository
        {
            get
            {
                if (this.peopleRepository == null)
                    this.peopleRepository = new DataRepository<Person>(ApplicationDbContext);
                return peopleRepository;
            }
        }

        private DataRepository<Medicine> medicineRepository;

        public DataRepository<Medicine> MedicineRepository
        {
            get
            {
                if (this.medicineRepository == null)
                    this.medicineRepository = new DataRepository<Medicine>(ApplicationDbContext);
                return medicineRepository;
            }
        }

        private DataRepository<Category>categoryRepository;

        public DataRepository<Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                    this.categoryRepository = new DataRepository<Category>(ApplicationDbContext);
                return categoryRepository;
            }
        }

        private DataRepository<Manufacturer> manufacturerRepository;

        public DataRepository<Manufacturer> ManufacturerRepository
        {
            get
            {
                if (this.manufacturerRepository == null)
                    this.manufacturerRepository = new DataRepository<Manufacturer>(ApplicationDbContext);
                return manufacturerRepository;
            }
        }

        private DataRepository<Invoice> invoiceRepository;

        public DataRepository<Invoice> InvoiceRepository
        {
            get
            {
                if (this.invoiceRepository == null)
                    this.invoiceRepository = new DataRepository<Invoice>(ApplicationDbContext);
                return invoiceRepository;
            }
            
        }

        private DataRepository<PurchasedItem> purchasedItemRepository;

        public DataRepository<PurchasedItem> PurchasedItemRepository
        {
            get
            {
                if (this.purchasedItemRepository == null)
                    this.purchasedItemRepository = new DataRepository<PurchasedItem>(ApplicationDbContext);
                return purchasedItemRepository;
            }

        }



        /// <summary>
        /// Save all th entity changed in context
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);   
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
