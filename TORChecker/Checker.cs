using System;
using System.Collections.Generic;

namespace TORChecker
{
    public class Checker : IChecker
    {
        public bool IsUsingTor(string IP)
        {
            return new Random(DateTime.Now.Millisecond).Next() % 2 == 0; 
        }
    }
}
