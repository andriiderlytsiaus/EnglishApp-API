using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.WordModels
{
    public  class Meaning
    {
        public string? PartOfSpeech { get; set; }
        public DefinitionInfo[]? Definitions { get; set; }
    }
}
