namespace Sweets.Domain.Boxes;

public enum CompostionError
{
    None = 0,
    TooFewSweets,
    TooManySweets,
    DuplicateSweet,
    UnevenSplit,
    QuantityMismatch,
    InvalidCapacity
}