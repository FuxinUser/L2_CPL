using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 *Author: ICSC SPYUA
 *Date:2019/12/28
 *Desc:
 */

namespace DBService
{
    public interface IRepository<T>  
    {
        /// <summary>
        /// 插入一筆資料
        /// </summary>
        int Insert(T obj);

        /// <summary>
        /// 更新一筆資料
        /// </summary>
        int Update(T obj, string[] pkValue);

        /// <summary>
        /// 刪除單一筆資料，藉由id
        /// </summary>
        int Delete(string[] pkValue, string condition);

        /// <summary>
        /// 獲取 一筆資料
        /// </summary>
        T Get(string[] pkValue);
        
        /// <summary>
        /// 拿取所有資料
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        /// 拿取所有資料(TOP 筆數)
        /// </summary>
        IEnumerable<T> GetAll(int limitCount);

        /// <summary>
        /// 拿取所有資料(條件篩選)
        /// </summary>
        IEnumerable<T> GetAll(string condition);

        /// <summary>
        /// 拿取所有資料(條件篩選 TOP筆數)
        /// </summary>     
        IEnumerable<T> GetAll(string condition, int limitCount);

        /// <summary>
        /// 拿取所有資料(排序)
        /// </summary>
        IEnumerable<T> GetAllOrderBy(string orderCondition, bool isReadAll = true, int limitCount = 0);
    }
}
