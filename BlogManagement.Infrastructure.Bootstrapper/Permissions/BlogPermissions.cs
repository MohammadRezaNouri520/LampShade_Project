namespace BlogManagement.Infrastructure.Configuration.Permissions
{
    public static class BlogPermissions
    {
        // Article
        public const int ListArticles = 90;
        public const int SearchArticles = 91;
        public const int CreateArticle = 92;
        public const int EditArticle = 93;
        public const int RemoveArticle = 94;
        public const int RestoreArticle = 95;

        // Article Category
        public const int ListArticleCategories = 100;
        public const int SearchArticleCategories = 101;
        public const int CreateArticleCategory= 102;
        public const int EditArticleCategory= 103;
    }
}
