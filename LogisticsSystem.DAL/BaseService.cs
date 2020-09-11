using System;
using System.Linq;
using System.Threading.Tasks;
using LogisticsSystem.IDAL;
using LogisticsSystem.Model;
using System.Data.Entity;

namespace LogisticsSystem.DAL
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        private readonly LogisticsContext _db;

        public BaseService(LogisticsContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(T model, bool saved = true)
        {
            _db.Set<T>().Add(model);
            try
            {
                if (saved)
                    await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task EditAsync(T model, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;//关掉自动校验(ValidateOnSaveEnabled)
            _db.Entry(model).State = EntityState.Modified;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public async Task<T> GetOneById(Guid id)
        {
            return await GetAll().FirstAsync(p => p.Id == id);
        }

        public async Task RemoveAsync(T model, bool saved = true)
        {
            await RemoverAsync(model.Id, saved);
        }


        public async Task RemoverAsync(Guid id, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;//关掉自动校验
            var t = new T() { Id = id };
            _db.Entry(t).State = EntityState.Unchanged;
            t.IsRemove = true;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;//恢复自动校验
            }
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
            _db.Configuration.ValidateOnSaveEnabled = true;//恢复自动校验
        }
        public IQueryable<T> GetAll()
        {
            return _db.Set<T>().Where(p => p.IsRemove == false).AsNoTracking();
        }

        public IQueryable<T> GetAllByPage(int pageSize = 15, int pageIndex = 0)
        {
            return GetAll().Skip(pageSize * pageIndex).Take(pageSize);
        }

        public IQueryable<T> GetAllByPageOrder(int pageSize = 15, int pageIndex = 0, bool asc = true)
        {
            return GetAllOrder(asc).Skip(pageSize * pageIndex).Take(pageSize);
        }

        public IQueryable<T> GetAllOrder(bool asc = true)
        {
            var dates = GetAll();
            dates = asc ? dates.OrderBy(p => p.CreatTime) : dates.OrderByDescending(p => p.CreatTime);
            return dates;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }

}

