using System;

namespace MyBus.Models
{
    public readonly struct NavigationButton
    {
        public Type TargetViewType { get; }
        public string ShortName { get; }
        public string Description { get; }

        public NavigationButton(Type targetViewType, string shortName, string description)
        {
            TargetViewType = targetViewType;
            ShortName = shortName;
            Description = description;
        }
    }
}
