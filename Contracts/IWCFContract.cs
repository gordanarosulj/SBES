using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Contracts
{
	[ServiceContract]
	public interface IWCFContract
	{
		[OperationContract]
		void TestCommunication();

		[OperationContract]
		void UnesiZalbu(string zalba, string subName, string issuer);
      // [OperationContract]
     //   void UnesiZalbu();
	}
}
