using System;

namespace BeardedManStudios.Forge.Networking.Generated
{
    [AttributeUsage(AttributeTargets.All)]
    public class GeneratedRPCVariableNamesAttribute : Attribute
    {
        public readonly string JsonData;

        public GeneratedRPCVariableNamesAttribute(string data)
        {
            JsonData = data;
        }

        public override string ToString()
        {
            return string.Format("d:{0}", JsonData);
        }
    }
}