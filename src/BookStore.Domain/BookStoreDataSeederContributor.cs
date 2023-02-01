using BookStore.Authors;
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
    private readonly IAuthorRepository _authorRepository;
    private readonly AuthorManager _authorManager;

    public BookStoreDataSeederContributor(IRepository<Book, Guid> bookRepository, IAuthorRepository authorRepository, AuthorManager authorManager)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _authorManager = authorManager;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _bookRepository.GetCountAsync() <= 0)
        {

            var wittenberg = await _authorRepository.InsertAsync(
            await _authorManager.CreateAsync("Tomislav Wittenberg", new DateTime(1937, 01, 08),
                "Tomislav Witenberg je rođen u Baćuru."));

            var zivkovic = await _authorRepository.InsertAsync(
                await _authorManager.CreateAsync("Branko Živković", new DateTime(1925, 01, 01),
                    "Branko Živković je rođen u ..."));


            await _bookRepository.InsertAsync(new Book
            {
                AuthorId = wittenberg.Id, // SET THE AUTHOR
                Name = "Groblja Požeške Doline",
                Type = BookType.Monography,
                PublishDate = new DateTime(1996, 01, 01),
                Price = 29.99f
            },
            autoSave: true);

            await _bookRepository.InsertAsync(new Book
            {
                AuthorId = zivkovic.Id, // SET THE AUTHOR
                Name = "Putopisne Impresije",
                Type = BookType.Monography,
                PublishDate = new DateTime(2004, 01, 01),
                Price = 14.99f
            },
            autoSave: true);
        }
    }
}