using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BattleShipCodingTest.Shared.Exceptions
{
    public class BattleShipApiException : Exception
    {
        public BattleShipApiException(string message) : base(message) { }

        public BattleShipApiException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public static void ThrowIfEntityNotFound(object targetObject, string entityName)
        {
            if (targetObject == null)
            {
                ThrowException(entityName);
            }

            var enurable = targetObject as IEnumerable<object>;
            if (enurable != null && !enurable.Any())
            {
                ThrowException(entityName);
            }
        }

        private static void ThrowException(string entityName)
        {
            throw new BattleShipApiException("Not Found", entityName);
        }
    }
}
