using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace BlogManagement.Infrastructure.Configuration.Permissions
{
    public class BlogPermissionsExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Articles",new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.ListArticles,"لیست مقالات"),
                        new PermissionDto(BlogPermissions.SearchArticles,"جستجو در مقالات"),
                        new PermissionDto(BlogPermissions.CreateArticle,"ایجاد مقاله"),
                        new PermissionDto(BlogPermissions.EditArticle,"ویرایش مقاله"),
                        new PermissionDto(BlogPermissions.RemoveArticle,"حذف مقاله"),
                        new PermissionDto(BlogPermissions.RestoreArticle,"بازیابی مقاله")
                    }
                },
                {
                    "Article_Categories", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.ListArticleCategories,"لیست گروه مقالات"),
                        new PermissionDto(BlogPermissions.SearchArticleCategories,"جستجو در گروه مقالات"),
                        new PermissionDto(BlogPermissions.CreateArticleCategory,"ایجاد گروه مقاله"),
                        new PermissionDto(BlogPermissions.EditArticleCategory,"ویرایش گروه مقاله")
                    }
                }
            };
        }
    }
}
