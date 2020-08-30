using System.Collections.Generic;

namespace Nombok.Template.Configuration
{
   public class TemplateConfig
   {
      public string BaseLocation { get; set; }
      public HashSet<string> Locations { get; } = new HashSet<string>();
   }
}

