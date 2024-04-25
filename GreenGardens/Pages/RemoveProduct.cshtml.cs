using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GreenGardens.Pages
{
    public class RemoveProductModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}



/*

@page "{id:int}"
@model ToDoExampleAndy.Pages.DeleteModel

<h2>Confirm Deletion</h2>
<p>Are you sure you want to delete this item?</p>
<p><strong>Description:</strong> @Model.Item.Description</p>

<form method="post">
    <input type="hidden" asp-for="Item.Id" />
    <button type="submit" class="btn btn-danger">Delete</button>
    <a asp-page="Index" class="btn btn-secondary">Cancel</a>
</form>





 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using System.Threading.Tasks;
using System.Linq;

namespace ToDoExampleAndy.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _dbConnection;

        public TaskModel Item { get; set; }

        public DeleteModel(AppDbContext context)
        {
            _dbConnection = context;
        }

        public void OnGet(int id)
        {
            // Retrieve the item to be deleted
            Item = _dbConnection.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var itemToDelete = _dbConnection.Tasks.FirstOrDefault(t => t.Id == id);
            if (itemToDelete != null)
            {
                _dbConnection.Tasks.Remove(itemToDelete);
                await _dbConnection.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                // Handle the case where the item does not exist
                return NotFound();
            }
        }
    }
}
 */