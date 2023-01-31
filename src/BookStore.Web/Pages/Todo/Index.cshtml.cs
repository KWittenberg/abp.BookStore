using BookStore.Todo;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace BookStore.Web.Pages.Todo;

public class IndexModel : BookStorePageModel
{
    public List<TodoItemDto> TodoItems { get; set; }

    private readonly ITodoAppService _todoAppService;

    public IndexModel(ITodoAppService todoAppService)
    {
        _todoAppService = todoAppService;
    }

    public async Task OnGetAsync()
    {
        TodoItems = await _todoAppService.GetListAsync();
    }
}



//public class IndexModel : AbpPageModel
//{
//    public List<TodoItemDto> TodoItems { get; set; }

//    private readonly TodoAppService _todoService;

//    public IndexModel(TodoAppService todoService)
//    {
//        _todoService = todoService;
//    }

//    public async Task OnGetAsync()
//    {
//        TodoItems = await _todoService.GetListAsync();
//    }
//}