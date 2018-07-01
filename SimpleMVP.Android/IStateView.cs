using System;
namespace SimpleMVP.Droid
{
    public interface IStateView
    {
        bool IsVisibleAndRunning { get; }
    }
}
