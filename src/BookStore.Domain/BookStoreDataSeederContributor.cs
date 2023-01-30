using BookStore.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace BookStore;

public class BookStoreDataSeederContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Book, Guid> _bookRepository;

    public BookStoreDataSeederContributor(IRepository<Book, Guid> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _bookRepository.GetCountAsync() <= 0)
        {

            await _bookRepository.InsertAsync(new Book
            {
                Name = "Groblja Požeške Doline",
                Type = BookType.Monography,
                PublishDate = new DateTime(1996, 01, 01),
                Price = 29.99f
            },
            autoSave: true);

            await _bookRepository.InsertAsync(new Book
            {
                Name = "Putopisne Impresije",
                Type = BookType.Monography,
                PublishDate = new DateTime(2004, 01, 01),
                Price = 14.99f
            },
            autoSave: true);
        }
    }
}