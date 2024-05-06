using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Cactus.Pages
{
    [Authorize(Roles = "Patron, Author")]
    public class UninterestingAuthorModel : PageModel
    {
        private readonly IUninterestingAuthorService uninterestingAuthorService;
        public UninterestingAuthorModel(IUninterestingAuthorService uninterestingAuthorService) {
            this.uninterestingAuthorService = uninterestingAuthorService;
        }
        public IEnumerable<UninterestingAuthor> UninterestingAuthors { get; set; }
        public string UninterestingAuthorsMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            BaseResponse<IEnumerable<UninterestingAuthor>> uninterestings =
                await uninterestingAuthorService
                .GetUninterestingAuthorsViewAsync(Convert.ToInt32(User.FindFirstValue("Id")));
            if (uninterestings.StatusCode == 200) {
                UninterestingAuthors = uninterestings.Data;
            }
            else UninterestingAuthorsMessage = uninterestings.Description;
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveUninterestingAuthor(int authorId) {
            BaseResponse<bool> response = await uninterestingAuthorService.RemoveUninterestingAuthor(Convert.ToInt32(User.FindFirstValue("Id")), authorId);
            return RedirectToPage("UninterestingAuthor");
        }
    }
}
