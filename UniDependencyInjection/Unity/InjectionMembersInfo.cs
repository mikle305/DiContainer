using System.Collections.Generic;
using System.Reflection;

namespace UniDependencyInjection.Unity
{
    public class InjectionMembersInfo
    {
        public List<MethodInfo> Methods { get; set; }
        public List<PropertyInfo> Properties { get; set; }
    }
}