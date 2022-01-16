using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly ShopContext _shopContext;

        public CommentRepository(ShopContext shopContext):base(shopContext)
        {
            _shopContext = shopContext;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _shopContext.Comments
                .Include(c => c.Product)
                .Select(c => new CommentViewModel 
                {
                    Id= c.Id,
                    Name=c.Name,
                    Email=c.Email,
                    Message=c.Message,
                    IsConfirmed=c.IsConfirmed,
                    IsCanceled=c.IsCanceled,
                    CreationDate=c.CreationDate.ToFarsi(),
                    ProductId=c.ProductId,
                    ProductName=c.Product.Name
                });

            if(!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(c => c.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(c => c.Email.Contains(searchModel.Email));

            return query.OrderByDescending(c => c.Id).ToList();
        }
    }
}
