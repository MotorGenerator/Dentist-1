using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    
    class Receiver
    {
        public void Action(ICommand e)
        {
            e.Execute();
        }
    }
}
