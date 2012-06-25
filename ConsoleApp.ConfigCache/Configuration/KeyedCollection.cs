using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ConsoleApp.ConfigCache.Configuration
{
    /// <summary>
    /// Provide a collection of key embedded in the realization of
    /// <see cref = "IKeyedObject" /> property the collection subkey 
    /// <b> Key </ b> collection class.
    /// </summary>
    /// <typeparam name="T">implement IKeyedObject Class.With a collection of key attributes in the collection subkey</typeparam>
    public class KeyedCollection<T> : KeyedCollection<string, T> where T:IKeyedObject
    {

        /// <summary>
        /// Initialized using the default equality comparer 
        /// <b> the KeyedCollection </ b> a new instance of the class.
        /// <remarks>String keys are not case sensitive</remarks>
        /// </summary>
        public KeyedCollection():base(StringComparer.InvariantCultureIgnoreCase){}

        /// <summary>
        /// Initialization using the new instance of the specified strings are equal comparator 
        /// <b> the KeyedCollection </ b> class.
        /// </summary>
        /// <param name="comparer">The <see cref="StringComparer"/> compare the key you want to use.</param>
        public KeyedCollection(StringComparer comparer): base(comparer ?? StringComparer.InvariantCultureIgnoreCase){}

        /// <summary>
        /// T is indexer.
        /// </summary>
        /// <param name="key">key elements.</param>
        /// <returns>T</returns>
        public T this[string key] 
        {
            get
            {
                T item = default(T);
                if (Contains(key))
                    item = base[key];

                return item;
            }
            set
            {
                AddAndReplace(value);
            }
        }

        /// <summary>
        /// Adds the query string.
        /// If the specified name already exists, the previous value will be replaced.
        /// </summary>
        /// <param name="item">T key elements.</param>
        public void AddAndReplace(T item)
        {
            if (item == null)
                return;

            if (base.Contains(item.Key))
                base.Remove(item.Key);

            base.Add(item);
        }

        /// <summary>
        /// Extract the key from the specified element.
        /// </summary>
        /// <param name="item">Extract the key elements.</param>
        /// <returns>Specifies the element's key.</returns>
        protected override string GetKeyForItem(T item)
        {
            return item.Key;
        }

        /// <summary>
        /// Insert key elements to Collection
        /// </summary>
        /// <param name="index">number</param>
        /// <param name="item">T key elements</param>
        protected override void InsertItem(int index, T item)
        {
            if (this.Contains(item.Key))
                throw new InvalidOperationException("collection have contain key is "+ item.Key);

            base.InsertItem(index, item);
        }
    }
}
