using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CheckComplaint
    {
        public static bool Verify(string zalba)
        {
            string[] nReci = File.ReadAllLines("nedozvoljene_reci.txt");
            foreach (var nRec in nReci)
            {
                if (zalba.Contains(nRec))
                {
                    Console.WriteLine("Nedozvoljena reeeeec!");
                    return true;
                }
            }

            return false;

        }
    }
}
