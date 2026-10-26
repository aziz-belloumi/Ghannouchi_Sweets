namespace Sweets.Domain.Entities;

public class Sweet
{
    public Guid Id { get; private set; }
    public string NameAr { get; private set; }
    public string NameFr { get; private set; }
    public string? ImageUrl { get; private set; }
    public decimal PricePerPiece { get; private set; }
    public bool IsActive { get; private set; }

    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    private Sweet()
    {
        NameAr = string.Empty;
        NameFr = string.Empty;
    }

    public Sweet(
        string nameAr,
        string nameFr,
        decimal pricePerPiece,
        Guid categoryId,
        string? imageUrl = null
        )
    {
        if (pricePerPiece <= 0)
            throw new ArgumentException("Price per piece must be positive.", nameof(pricePerPiece));

        Id = Guid.NewGuid();
        NameAr = nameAr;
        NameFr = nameFr;
        PricePerPiece = pricePerPiece;
        CategoryId = categoryId;
        ImageUrl = imageUrl;
        IsActive = true;
    }
}