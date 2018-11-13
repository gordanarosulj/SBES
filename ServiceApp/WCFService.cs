using Contracts;
using Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp
{
    public class WCFService : IWCFContract
    {
        public void TestCommunication()
        {
            Console.WriteLine("Communication established.");
        }

        public void UnesiZalbu(string zalba, string subName, string issuer)
        {
            /// Get client's certificate from storage. It is expected that clients sign messages using the certificate with subjectName in the following format "<username>_sign" 			
            //X509Certificate2 clientCertificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, "wcfclient_sign");
            Console.WriteLine("Zalba: {0}", zalba);
            Console.WriteLine("Proveravanje da li sadrzi nedozvoljene reci.......");

            if (!CheckComplaint.Verify(zalba))
            {
                Console.WriteLine("Zalba je prihvacena.");
            }
            else
            {
                Console.WriteLine("Zalba sadrzi nedozvoljenu rec.");
                //string[] nReci = File.ReadAllLines("nevalidne_zalbe.txt");

                //TextWriter tw = new StreamWriter("nevalidne_zalbee.txt");

                //File.AppendAllText(@"C:\\Users\\User\\Desktop\\SBES\\Projekat\\ServiceApp\\bin\\Debug\\nevalidne_zalbe.txt", subName + zalba + Environment.NewLine);
                using (StreamWriter sw = File.AppendText("nevalidne_zalbe.txt"))
                {
                    sw.WriteLine("CN: {0}", subName);
                    sw.WriteLine("Issuer: {0}", issuer);
                    sw.WriteLine("Complaint: {0}\n", zalba);
                }

            }

            //return false;
            //a .ttxt sa nedozvoljenim recima u formatu 'rec, rec, rec, ..' i sa formaterom ih ubacim u List<string> nedozvoljene_reci
            //string.contains => zalba.contains


        }
    }
}
