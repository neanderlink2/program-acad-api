using System;

namespace ProgramAcad.Common.Attributes
{
    public sealed class CompilerTypeAttribute : Attribute
    {
        public CompilerTypeAttribute(string compilerTypeIdentifier)
        {
            CompilerType = compilerTypeIdentifier;
        }

        public string CompilerType { get; }
    }
}
