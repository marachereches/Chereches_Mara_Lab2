using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chereches_Mara_Lab2.Data;
using Chereches_Mara_Lab2.Models;
using Chereches_Mara_Lab2.Models.ViewModels;

namespace Chereches_Mara_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Chereches_Mara_Lab2.Data.Chereches_Mara_Lab2Context _context;

        public IndexModel(Chereches_Mara_Lab2.Data.Chereches_Mara_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;
        public CategoriesIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }
        public int BookID { get; set; }
        public async Task OnGetAsync(int? id, int? bookID)
        {
            CategoryData = new CategoriesIndexData();
            CategoryData.Categories = await _context.Category
                .Include(i => i.BookCategories)
                .ThenInclude(i => i.Book)
                .ThenInclude(c => c.Author)
                .OrderBy(i => i.CategoryName)
                .ToListAsync();
            if (id != null)
            {
                CategoryID = id.Value;
                Category category = CategoryData.Categories
                .Where(i => i.ID == id.Value).Single();
                CategoryData.BookCategories = category.BookCategories;
            }
        }

    }
}
