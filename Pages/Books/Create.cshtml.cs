using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Chereches_Mara_Lab2.Data;
using Chereches_Mara_Lab2.Models;

namespace Chereches_Mara_Lab2.Pages.Books
{
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Chereches_Mara_Lab2.Data.Chereches_Mara_Lab2Context _context;

        public CreateModel(Chereches_Mara_Lab2.Data.Chereches_Mara_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
"PublisherName");
            ViewData["FirstName"] = new SelectList(_context.Set<Author>(), "ID",
"FirstName");
            ViewData["LastName"] = new SelectList(_context.Set<Author>(), "ID",
"LastName");

            var book = new Book();
            book.BookCategories = new List<BookCategory>();
            PopulateAssignedCategoryData(_context, book);
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
 {
 var newBook = new Book();
 if (selectedCategories != null)
 {
 newBook.BookCategories = new List<BookCategory>();
 foreach (var cat in selectedCategories)
 {
 var catToAdd = new BookCategory
 {
 CategoryID = int.Parse(cat)
 };
newBook.BookCategories.Add(catToAdd);
 }
 }
 if (await TryUpdateModelAsync<Book>(
 newBook,
 "Book",
 i => i.Title, i => i.Author,
 i => i.Price, i => i.PublishingDate, i => i.PublisherID))
 {
 _context.Book.Add(newBook);
 await _context.SaveChangesAsync();
 return RedirectToPage("./Index");
 }
 PopulateAssignedCategoryData(_context, newBook);
 return Page();
 }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}