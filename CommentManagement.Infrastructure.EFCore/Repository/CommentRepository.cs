using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _commentContext;

        public CommentRepository(CommentContext commentContext):base(commentContext)
        {
            _commentContext = commentContext;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _commentContext.Comments
                //.Include(c => c.Product)
                .Select(c => new CommentViewModel 
                {
                    Id= c.Id,
                    Name=c.Name,
                    Email=c.Email,
                    Message=c.Message,
                    IsConfirmed=c.IsConfirmed,
                    IsCanceled=c.IsCanceled,
                    CreationDate=c.CreationDate.ToFarsi()
                    //ProductId=c.ProductId,
                    //ProductName=c.Product.Name
                });

            if(!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(c => c.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(c => c.Email.Contains(searchModel.Email));

            return query.OrderByDescending(c => c.Id).ToList();
        }
    }
}
