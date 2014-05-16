// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JavaScriptSerializerUtility.cs" company="">
//   
// </copyright>
// <summary>
//   Setup a JavaScriptSerializer
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LocalSearch
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Script.Serialization;

    /// <summary>
    ///     Setup a JavaScriptSerializer
    /// </summary>
    public static class JavaScriptSerializerUtility
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get dictionary from key chain.
        /// </summary>
        /// <param name="keyChains">
        /// The key chains.
        /// </param>
        /// <param name="group">
        /// The group.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<Dictionary<string, object>> GetDictionaryFromKeyChain(
            string[] keyChains, Dictionary<string, object> group)
        {
            object items = group[keyChains[0]];
            var arrayItems = items as ArrayList;
            if (arrayItems != null)
            {
                foreach (object item in arrayItems)
                {
                    if (keyChains.Length > 1)
                    {
                        foreach (var dict in
                            GetDictionaryFromKeyChain(keyChains.Skip(1).ToArray(), item as Dictionary<string, object>))
                        {
                            yield return dict;
                        }
                    }
                    else
                    {
                        yield return item as Dictionary<string, object>;
                    }
                }
            }
            else
            {
                if (keyChains.Length > 1)
                {
                    foreach (var dict in
                        GetDictionaryFromKeyChain(keyChains.Skip(1).ToArray(), items as Dictionary<string, object>))
                    {
                        yield return dict;
                    }
                }
                else
                {
                    yield return items as Dictionary<string, object>;
                }
            }
        }

        /// <summary>
        ///     The setup serializer.
        /// </summary>
        /// <returns>
        ///     The <see cref="JavaScriptSerializer" />.
        /// </returns>
        public static JavaScriptSerializer SetupSerializer()
        {
            var result = new JavaScriptSerializer { MaxJsonLength = int.MaxValue, RecursionLimit = int.MaxValue };
            return result;
        }

        #endregion
    }
}