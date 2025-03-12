using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outils.Exceptions
{
    /// <summary>
    /// Exception levée quand une partie est introuvable
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class PartieIntrouvableException : Exception
    {
        /// <summary>
        /// génère l'exception
        /// </summary>
        public PartieIntrouvableException() { }
    }
}
