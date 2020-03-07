using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookCycle.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity:class
    {
        /// <summary>
        /// Verilen TEntity'nin Id'sine Göre Getirme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Verilen TEntity'nin Tüm Bilgileri Çekme
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Kullanım şekli örnek : category.Find(x=>x.Name="Edebiyat"); Edebiyat Türündeki Bana Getir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Kullanım şekli örnek : category.SingleOrDefaultAsync(x=>x.Name="Hikaye"); Tek Bir Tane Getirir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

            
        /// <summary>
        /// Verilen TEntity'ye Göre Kaydetme
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Verilen TEntity'ye Göre Toplu Bir Şekilde Kaydetme
        /// </summary>
        /// <param name="entites"></param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<TEntity> entites);


        /// <summary>
        /// Verilen TEntity'ye Göre Silme
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// Verilen TEntity'ye Göre Toplu Silme
        /// </summary>
        /// <param name="entity"></param>
        void RemoveRange(IEnumerable<TEntity> entites);

        /// <summary>
        /// Verilen TEntity'ye Göre Güncelleme
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

    }
}
