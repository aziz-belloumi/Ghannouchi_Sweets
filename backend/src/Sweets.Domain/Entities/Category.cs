namespace Sweets.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string NameAr { get; private set; }
    public string NameFr { get; private set; }
    public string Slug { get; private set; }
    public string? ImageUrl { get; private set; }
    public int SortOrder { get; private set; }
    public bool IsActive { get; private set; }

    private Category()
    {
        NameAr = string.Empty;
        NameFr = string.Empty;
        Slug = string.Empty;
    }

    public Category(string nameAr, string nameFr, string slug, string? imageUrl = null, int sortOrder = 0)
    {
        Id = Guid.NewGuid();
        NameAr = nameAr;
        NameFr = nameFr;
        Slug = slug;
        ImageUrl = imageUrl;
        SortOrder = sortOrder;
        IsActive = true;
    }
}