using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(LiveSplit.PercentileTracker.PercentileFactory))]

namespace LiveSplit.PercentileTracker
{
    public class PercentileFactory : IComponentFactory
    {
        public string ComponentName => "CurrentPaceTopPercentage";
        public string Description => "Shows your current pace percentile (Global/Reach) compared to history.";
        public ComponentCategory Category => ComponentCategory.Information;

        public IComponent Create(LiveSplitState state) => new DualPercentileComponent(state);

        public string UpdateName => ComponentName;
        public string XMLURL => "";
        public string UpdateURL => "";
        public Version Version => Version.Parse("1.1.0");
    }
}