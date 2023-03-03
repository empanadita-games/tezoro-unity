using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netezos.Rpc.Queries.Post
{
    /// <summary>
    /// Rpc query to access operations parsing
    /// </summary>
    public class ParseOperationsQuery : RpcMethod
    {
        internal ParseOperationsQuery(RpcQuery baseQuery, string append) : base(baseQuery, append) { }

        /// <summary>
        /// Parses operations and returns the operations content
        /// </summary>
        /// <param name="operations">List of operation</param>
        /// <param name="checkSignature">Check signature (optional)</param>
        /// <returns></returns>
        public IEnumerator PostAsync(List<object> operations, bool? checkSignature = null)
            => PostAsync(new
            {
                operations,
                check_signature = checkSignature
            });

        /// <summary>
        /// Parses operations and returns the operations content
        /// </summary>
        /// <typeparam name="T">Type of the object to deserialize to</typeparam>
        /// <param name="operations">List of operation</param>
        /// <param name="checkSignature">Check signature (optional)</param>
        /// <returns></returns>
        public IEnumerator PostAsync<T>(List<object> operations, bool? checkSignature = null)
            => PostAsync<T>(new
            {
                operations,
                check_signature = checkSignature
            });
    }
}