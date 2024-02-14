namespace VideoSharp;

/// <summary>
/// Represents an <i>x</i>, <i>y</i> square
/// </summary>
/// <param name="Width">Width (<i>x</i>)</param>
/// <param name="Height">Height (<i>y</i>)</param>
public record Dimension(
    int Width,
    int Height
);
