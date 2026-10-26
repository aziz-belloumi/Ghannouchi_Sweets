namespace Sweets.Domain.Boxes;

public enum CompositionError
{
    None = 0,
    TooFewSweets,
    TooManySweets,
    DuplicateSweet,
    UnevenSplit,
    QuantityMismatch,
    InvalidCapacity
}