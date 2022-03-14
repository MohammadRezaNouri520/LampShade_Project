namespace ShopManagement.Infrastructure.Configuration.Permissions
{
    public static class ShopPermissions
    {
        // Products
        public const int ListProducts = 10;
        public const int SearchProducts = 11;
        public const int CreateProduct = 12;
        public const int EditProduct = 13;

        // Product Categories
        public const int ListProductCategories = 20;
        public const int SearchProductCategories = 21;
        public const int CreateProductCategory = 22;
        public const int EditProductCategory = 23;

        // Product Pictures
        public const int ListProductPictures = 30;
        public const int SearchProductPictures = 31;
        public const int CreateProductPicture = 32;
        public const int EditProductPicture = 33;
        public const int RemoveProductPicture = 34;
        public const int RestoreProductPicture = 35;

        // Slides
        public const int ListSlides = 40;
        public const int CreateSlide = 41;
        public const int EditSlide = 42;
        public const int RemoveSlide = 43;
        public const int RestoreSlide = 44;
    }
}
