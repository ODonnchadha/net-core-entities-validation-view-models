using JurisTempus.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JurisTempus
{
  public class ContactModel : PageModel
  {
    [BindProperty()]
    public ContactViewModel ViewModel { get; set; }

    public void OnGet() { }
    public void OnPost()
    {
      if (ModelState.IsValid)
      {
        return;
      }
    }
  }
}
