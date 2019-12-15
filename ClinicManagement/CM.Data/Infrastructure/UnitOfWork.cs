using CM.Model.Models;
using CM.Model.Models.Account;
using CM.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private DataRepository<Person> personRepository;

        public DataRepository<Person> PeopleRepository
        {
            get
            {
                if (this.personRepository == null)
                    this.personRepository = new DataRepository<Person>(ApplicationDbContext);
                return personRepository;
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
