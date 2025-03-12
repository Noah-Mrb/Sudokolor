using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.Exceptions
{
    /// <summary>
    /// Exception lancée quand une tentative de modifier
    /// une case non modifiable est arrivée
    /// </summary>
    public class CaseNonModifiableException : Exception
    {
        /// <summary>
        /// créer l'exception
        /// </summary>
        /// <param name="message"> message</param>
        public CaseNonModifiableException(string message) : base(message) { }
    }
}
