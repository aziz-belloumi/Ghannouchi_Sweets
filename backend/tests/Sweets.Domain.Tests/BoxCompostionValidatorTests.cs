using Sweets.Domain.Boxes;
using Xunit;

namespace Sweets.Domain.Tests;

public class BoxCompositionValidatorTests
{
    private static BoxComposition Build(int capacity, params (Guid, int)[] items)
    {
        var list = items.Select(x => new BoxCompositionItem(x.Item1, x.Item2)).ToList();
        return new BoxComposition(capacity, list);
    }

    [Fact]
    public void FullBox_OneSweet_IsValid()
    {
        var g = Guid.NewGuid();
        var box = Build(30, (g, 30));
        var result = BoxCompositionValidator.Validate(box);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void TwoSweets_HalfHalf_IsValid()
    {
        var box = Build(30, (Guid.NewGuid(), 15), (Guid.NewGuid(), 15));
        var result = BoxCompositionValidator.Validate(box);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void TwoSweets_TenTwenty_IsValid()
    {
        var box = Build(30, (Guid.NewGuid(), 10), (Guid.NewGuid(), 20));
        Assert.True(BoxCompositionValidator.Validate(box).IsSuccess);
    }

    [Fact]
    public void TwoSweets_TwentyTen_IsValid()
    {
        var box = Build(30, (Guid.NewGuid(), 20), (Guid.NewGuid(), 10));
        Assert.True(BoxCompositionValidator.Validate(box).IsSuccess);
    }

    [Fact]
    public void ThreeSweets_Equal_IsValid()
    {
        var box = Build(30,
            (Guid.NewGuid(), 10),
            (Guid.NewGuid(), 10),
            (Guid.NewGuid(), 10));
        Assert.True(BoxCompositionValidator.Validate(box).IsSuccess);
    }

    // ---- invalid cases ----

    [Fact]
    public void WrongTotal_IsInvalid()
    {
        var box = Build(30, (Guid.NewGuid(), 15), (Guid.NewGuid(), 10));
        var result = BoxCompositionValidator.Validate(box);
        Assert.False(result.IsSuccess);
        Assert.Equal(nameof(CompositionError.QuantityMismatch), result.ErrorCode);
    }

    [Fact]
    public void DuplicateSweet_IsInvalid()
    {
        var g = Guid.NewGuid();
        var box = Build(30, (g, 15), (g, 15));
        var result = BoxCompositionValidator.Validate(box);
        Assert.False(result.IsSuccess);
        Assert.Equal(nameof(CompositionError.DuplicateSweet), result.ErrorCode);
    }

    [Fact]
    public void FourSweets_IsInvalid()
    {
        var box = Build(30,
            (Guid.NewGuid(), 8),
            (Guid.NewGuid(), 8),
            (Guid.NewGuid(), 7),
            (Guid.NewGuid(), 7));
        Assert.False(BoxCompositionValidator.Validate(box).IsSuccess);
    }

    [Fact]
    public void Empty_IsInvalid()
    {
        var box = new BoxComposition(30, new List<BoxCompositionItem>());
        Assert.False(BoxCompositionValidator.Validate(box).IsSuccess);
    }

    [Fact]
    public void UnevenSplit_TwentyFiveFive_IsInvalid()
    {
        var box = Build(30, (Guid.NewGuid(), 25), (Guid.NewGuid(), 5));
        Assert.False(BoxCompositionValidator.Validate(box).IsSuccess);
    }
}