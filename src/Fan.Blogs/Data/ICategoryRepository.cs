﻿using Fan.Blogs.Models;
using Fan.Data;
using Fan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fan.Blogs.Data
{
    /// <summary>
    /// Contract for a category repository.
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// Deletes a <see cref="Category"/> by id and re-categorize its posts to the given 
        /// default category id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="defaultCategoryId">
        /// This is the BlogSettings DefaultCategoryId, I choose to have it pass in
        /// from BLL for convenience instead of querying Meta for it.
        /// </param>
        Task DeleteAsync(int id, int defaultCategoryId);

        /// <summary>
        /// Returns all categories or empty list if no categories found.
        /// </summary>
        Task<List<Category>> GetListAsync();
    }
}