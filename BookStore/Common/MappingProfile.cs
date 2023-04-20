using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Application.BookOperations.GetBookDetail;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStore.Application.BookOperations.CreateBook.CreateBookCommand;
using BookStore.Application.BookOperations.GetBooks;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.BookOperations.CreateBook;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.Author.Name} {src.Author.Surname}"))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));
            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.Author.Name} {src.Author.Surname}"))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<Author, AuthorViewModel>();
            CreateMap<Author, AuthorDetailViewModel>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Date.ToString("dd/MM/yyyy")));
            CreateMap<AuthorViewModel, Author>();
            CreateMap<CreateAuthorModel, Author>();
        }
    }
}
