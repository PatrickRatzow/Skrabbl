using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.DataAccess.Queries
{
    public partial class CommandText: ICommandText
    {
        public string GetAllWords => "SELECT * FROM WordList";
        
    }
}
