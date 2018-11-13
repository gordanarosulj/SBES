using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    //zalbe ne treba da budu digitalno potpisane
    //izbacim korisniku meni 1.upisi zalbu
    //                       2. log out
    //                  ako je nadzor onda da mu ponudim da iscita, zabrani itd.. znaci switch case za korisnika ili nadzor
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            /// Define the expected service certificate. It is required to establish cmmunication using certificates.
            string srvCertCN = "wcfservice";
            string clntCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/Receiver"),
                                      new X509CertificateEndpointIdentity(srvCert));
            
            using (WCFClient proxy = new WCFClient(binding, address))
            {
                /// 1. Communication test
                proxy.TestCommunication();
                Console.WriteLine("TestCommunication() finished. Press <enter> to continue ...");
                X509Certificate2 clntCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clntCertCN);
               

                //proverim da li je nadzor ili klijent
                //switch (typeUser) pa meni za svakog


                while (true)
                {
                    Console.WriteLine("Meni:\n1. Unesite zalbu.\n2.Exit.");
                    string unos = Console.ReadLine();
                    string un = Formatter.ParseName(clntCert.SubjectName.Name.ToString());
                    switch(unos) {
                        case "1":
                            Console.WriteLine("Unesite zalbu:");
                            string zalba = Console.ReadLine();
                            proxy.UnesiZalbu(zalba, un, clntCert.Issuer.ToString());
                            break;
                        case "2":
                            break;
                        default:
                            Console.WriteLine("Odaberite neku stavku iz menija...");
                            break;
                    }
                }


            }
        }
    }
}
