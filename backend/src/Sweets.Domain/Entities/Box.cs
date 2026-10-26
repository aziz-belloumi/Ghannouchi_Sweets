namespace Sweets.Domain.Entities;

public enum BoxPriceMode
{
    Fixed = 0,
    PerPiece = 1
}

public class Box
{
    public Guid Id { get; private set; }
    public string NameAr { get; private set; }
    public string NameFr { get; private set; }
    public string? ImageUrl { get; private set; }
    public int Capacity { get; private set; }
    public BoxPriceMode PriceMode { get; private set; }
    public decimal? FixedPrice { get; private set; }
    public int SortOrder { get; private set; }
    public bool IsActive { get; private set; }

    private Box()
    {
        NameAr = string.Empty;
        NameFr = string.Empty;
    }

    public Box(
        string nameAr,
        string nameFr,
        int capacity,
        BoxPriceMode priceMode,
        decimal? fixedPrice = null,
        string? imageUrl = null,
        int sortOrder = 0)
    {
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be positive.", nameof(capacity));

        if (priceMode == BoxPriceMode.Fixed && (fixedPrice is null || fixedPrice <= 0))
            throw new ArgumentException("Fixed price required and must be positive.", nameof(fixedPrice));

        Id = Guid.NewGuid();
        NameAr = nameAr;
        NameFr = nameFr;
        Capacity = capacity;
        PriceMode = priceMode;
        FixedPrice = fixedPrice;
        ImageUrl = imageUrl;
        SortOrder = sortOrder;
        IsActive = true;
    }
}