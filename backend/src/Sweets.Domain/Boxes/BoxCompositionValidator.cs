using Sweets.Domain.Common;

namespace Sweets.Domain.Boxes;

public static class BoxCompositionValidator
{
    public const int MaxDistinctSweets = 3;
    public static Result Validate(BoxComposition composition)
    {
        var capacity = composition.Capacity;
        var items = composition.Items;

        // 1- Capacity must be positive
        if (capacity <= 0)
        {
            return Result.Failure(CompositionError.InvalidCapacity.ToString(), "Box capacity must be greater than zero.");
        }

        // 2- Must have at least one sweet type
        if (items is null || items.Count == 0)
        {
            return Result.Failure(CompositionError.TooFewSweets.ToString(), "Box must contain at least one sweet.");
        }

        // 3- Must not exceed maximum distinct sweets
        if (items.Count > MaxDistinctSweets)
        {
            return Result.Failure(CompositionError.TooManySweets.ToString(), $"Box cannot contain more than {MaxDistinctSweets} distinct sweets.");
        }

        // 4- No duplicate sweet types in case the user wants different types
        var distinctIds = items.Select(i => i.SweetId).Distinct().Count();
        if (distinctIds != items.Count)
        {
            return Result.Failure(CompositionError.DuplicateSweet.ToString(), "The same sweet cannot appear twice in a box.");
        }

        // 5- Total quantity must match the box capacity
        var totalQuantity = items.Sum(i => i.Quantity);
        if (totalQuantity != capacity)
        {
            return Result.Failure(CompositionError.QuantityMismatch.ToString(), $"Total pieces ({totalQuantity}) must equal box capacity ({capacity}).");
        }

        // 6- The set of quantites must match an allowed pattern : 10/20 , 20/10 , 15/15 , 10/10/10
        var quantities = items.Select(i => i.Quantity).OrderBy(q => q).ToList();
        if (!IsAllowedPattern(capacity, quantities))
        {
            return Result.Failure(CompositionError.UnevenSplit.ToString(), BuildPatternMessage(capacity));
        }
        return Result.Success();
    }

    private static bool IsAllowedPattern(int capacity, List<int> quantities)
    {
        // 1 sweet: full box
        if (quantities.Count == 1)
        {
            return quantities[0] == capacity;
        }

        // 2 sweets: 15/15, 10/20, or 20/10  (sorted: 15/15 or 10/20)
        if (quantities.Count == 2)
        {
            var half = capacity / 2;
            var lower = capacity / 3;   // 10 for capacity 30
            var higher = capacity - lower; // 20 for capacity 30

            var sorted = quantities.OrderBy(q => q).ToList();
            return (sorted[0] == half && sorted[1] == half)
                || (sorted[0] == lower && sorted[1] == higher);
        }

        // 3 sweets: equal thirds
        if (quantities.Count == 3)
        {
            if (capacity % 3 != 0) return false;
            var third = capacity / 3;
            return quantities.All(q => q == third);
        }
        return false;
    }

    private static string BuildPatternMessage(int capacity)
    {
        var third = capacity / 3;
        var twoThirds = capacity - third;
        var half = capacity / 2;

        return $"Invalid split for capacity {capacity}. " +
               $"Allowed: [{capacity}], [{half}/{half}], " +
               $"[{third}/{twoThirds}], [{twoThirds}/{third}], " +
               $"[{third}/{third}/{third}].";
    }
}