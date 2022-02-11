using System;

namespace BeardedManStudios.Forge.Networking.Generated
{
    [AttributeUsage(AttributeTargets.All)]
    public class GeneratedRPCAttribute : Attribute
    {
        public readonly string JsonData;

        public GeneratedRPCAttribute(string data)
        {
            JsonData = data;
        }

        public override string ToString()
        {
            return string.Format("d:{0}", JsonData);
        }
    }
}