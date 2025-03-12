using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.Exceptions
{
    /// <summary>
    /// Exception quand une valeur utilisée pour une case 
    /// dépasse les limites. Que cela soit une position
    /// ou la valeur à lui appliquer.
    /// </summary>
    public class HorsLimiteException : Exception
    {
        /// <summary>
        /// Créer l'exception
        /// </summary>
        /// <param name="message">message</param>
        public HorsLimiteException(string message) : base(message) { }
    }
}
