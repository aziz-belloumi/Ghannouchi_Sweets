namespace Sweets.Domain.Boxes;

public record BoxCompositionItem(Guid SweetId , int Quantity); // record is a class that compares by value, not by reference 

public record BoxComposition(int Capacity , IReadOnlyList<BoxCompositionItem> Items);