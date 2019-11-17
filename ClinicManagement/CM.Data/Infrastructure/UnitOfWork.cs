using CM.Model.Models.Account;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data.Infrastructure
{
    public class UnitOfWork : IDisposable
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
