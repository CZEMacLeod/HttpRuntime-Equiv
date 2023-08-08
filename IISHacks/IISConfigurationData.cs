namespace IISHacks;

internal class IISConfigurationData : IIISEnvironmentFeature
{
    internal IISConfigurationData()
    {

    }
    public string FullApplicationPath { get; internal init; }
}