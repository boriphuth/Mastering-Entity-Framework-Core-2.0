﻿using MasteringEFCore.Transactions.Starter.Data;
using MasteringEFCore.Transactions.Starter.Models;
using MasteringEFCore.Transactions.Starter.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Starter.Handlers
{
    public class GetPostByPublishedYearQueryHandler : IPostQueryHandler<GetPostByPublishedYearQuery>
    {
        private readonly BlogContext _context;

        public GetPostByPublishedYearQueryHandler(BlogContext context)
        {
            this._context = context;
        }

        public IEnumerable<Post> Handle(GetPostByPublishedYearQuery query)
        {
            return query.IncludeData
                        ? _context.Posts
                            .Where(x => x.PublishedDateTime.Year.Equals(query.Year))
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category).ToList()
                        : _context.Posts
                            .Where(x => x.PublishedDateTime.Year.Equals(query.Year))
                            .ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync(GetPostByPublishedYearQuery query)
        {
            return query.IncludeData
                        ? await _context.Posts
                            .Where(x => x.PublishedDateTime.Year.Equals(query.Year))
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category).ToListAsync()
                        : await _context.Posts
                            .Where(x => x.PublishedDateTime.Year.Equals(query.Year))
                            .ToListAsync();
        }
    }
}
