using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Starfish.Base
{
    public class FaultFactory
    {
        public struct CustomServiceFault
        {
            public FaultCode DiagnosticCode { get; set; }
            public FaultException SOAPException { get; set; }
            public string Message { get; set; }
            public string Source { get; set; }
            public bool IsException { get { return SOAPException == null; } }
        }

        public static CustomServiceFault Create(string message, string diagnosticCode)
        {
            return new CustomServiceFault() { Message = message, DiagnosticCode = new FaultCode(diagnosticCode) };
        }
        public static CustomServiceFault CreateException(string message, string diagnosticCode, string source)
        {
            return new CustomServiceFault()
            {
                DiagnosticCode = new FaultCode(diagnosticCode),
                SOAPException = new FaultException(message),
                Source = source,
                Message = message
            };
        }
    }
}
