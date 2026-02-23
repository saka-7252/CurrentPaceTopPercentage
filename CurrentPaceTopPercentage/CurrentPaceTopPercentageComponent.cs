using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace LiveSplit.PercentileTracker
{
    public class DualPercentileComponent : IComponent
    {
        protected InfoTextComponent InternalComponent { get; set; }
        public string ComponentName => "CurrentPaceTopPercentage";

        public enum DisplayMode { GlobalOnly = 0, ReachOnly = 1, Both = 2 }

        private PercentileSettings Settings { get; set; }

        public DualPercentileComponent(LiveSplitState state)
        {
            InternalComponent = new InfoTextComponent("Current Pace (Top)", GetDashValue(DisplayMode.Both));
            Settings = new PercentileSettings();
        }

        private string GetDashValue(DisplayMode mode)
        {
            switch (mode)
            {
                case DisplayMode.GlobalOnly: return "-";
                case DisplayMode.ReachOnly: return "-";
                default: return "- / -";
            }
        }

        private void CalculatePercentiles(LiveSplitState state)
        {
            if (state.CurrentPhase != TimerPhase.Running && state.CurrentPhase != TimerPhase.Paused)
            {
                InternalComponent.InformationValue = GetDashValue(Settings.Mode);
                return;
            }

            int comparisonIndex = state.CurrentSplitIndex - 1;

            if (comparisonIndex < 0)
            {
                UpdateValue(100.0, 100.0);
                return;
            }

            var currentRunSplitTime = state.Run[comparisonIndex].SplitTime.RealTime;
            if (!currentRunSplitTime.HasValue)
            {
                InternalComponent.InformationValue = GetDashValue(Settings.Mode);
                return;
            }

            var segmentToCompare = state.Run[comparisonIndex];
            int fasterRuns = segmentToCompare.SegmentHistory.Values
                .Count(time => time.RealTime.HasValue && time.RealTime.Value < currentRunSplitTime.Value) + 1;

            int totalAttempts = state.Run.AttemptHistory.Count + 1;
            int totalReached = segmentToCompare.SegmentHistory.Count + 1;

            UpdateValue((double)fasterRuns / totalAttempts * 100, (double)fasterRuns / totalReached * 100);
        }

        private void UpdateValue(double global, double reach)
        {
            string format = $"F{Settings.Decimals}";
            switch (Settings.Mode)
            {
                case DisplayMode.GlobalOnly:
                    InternalComponent.InformationValue = $"{global.ToString(format)}%";
                    break;
                case DisplayMode.ReachOnly:
                    InternalComponent.InformationValue = $"{reach.ToString(format)}%";
                    break;
                default:
                    InternalComponent.InformationValue = $"{global.ToString(format)}% / {reach.ToString(format)}%";
                    break;
            }
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            InternalComponent.NameLabel.ForeColor = state.LayoutSettings.TextColor;
            InternalComponent.ValueLabel.ForeColor = state.LayoutSettings.TextColor;
            InternalComponent.NameLabel.HasShadow = state.LayoutSettings.DropShadows;
            InternalComponent.ValueLabel.HasShadow = state.LayoutSettings.DropShadows;

            CalculatePercentiles(state);

            InternalComponent.Update(invalidator, state, width, height, mode);
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion) => InternalComponent.DrawVertical(g, state, width, clipRegion);
        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion) => InternalComponent.DrawHorizontal(g, state, height, clipRegion);
        public float VerticalHeight => InternalComponent.VerticalHeight;
        public float HorizontalWidth => InternalComponent.HorizontalWidth;
        public float MinimumHeight => InternalComponent.MinimumHeight;
        public float MinimumWidth => InternalComponent.MinimumWidth;
        public float PaddingTop => InternalComponent.PaddingTop;
        public float PaddingLeft => InternalComponent.PaddingLeft;
        public float PaddingBottom => InternalComponent.PaddingBottom;
        public float PaddingRight => InternalComponent.PaddingRight;
        public System.Collections.Generic.IDictionary<string, Action> ContextMenuControls => null;
        public void SetSettings(XmlNode settings) => Settings.SetSettings(SettingsHelper.ParseEnum<DisplayMode>(settings["DisplayMode"], DisplayMode.Both), SettingsHelper.ParseInt(settings["Decimals"], 2));
        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            SettingsHelper.CreateSetting(document, parent, "DisplayMode", Settings.Mode);
            SettingsHelper.CreateSetting(document, parent, "Decimals", Settings.Decimals);
            return parent;
        }
        public System.Windows.Forms.Control GetSettingsControl(LayoutMode mode) => Settings;
        public void Dispose() => Settings.Dispose();
    }
}