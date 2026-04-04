using UniRx;

public interface ISmellSource
{
    IReadOnlyReactiveProperty<bool> IsSmelling { get; }
}