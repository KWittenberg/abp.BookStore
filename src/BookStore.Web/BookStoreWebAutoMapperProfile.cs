﻿using AutoMapper;
using BookStore.Books;

namespace BookStore.Web;

public class BookStoreWebAutoMapperProfile : Profile
{
    public BookStoreWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        
        CreateMap<BookDto, CreateUpdateBookDto>();
    }
}