using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    { // It will be a Base for all Cache alternatives

        T Get<T>(string key); // A method in a T type and gets a T type with param str key
        object Get(string key); // It is an another way of Get but it needs Type Casting
        void Add(string key, object value, int duration);
        bool IsAdd(string key); // Cache ?
        void Remove(string key);
        void RemoveByPattern(string pattern); // Ex. Remove Caches include "Get" pattern

    }
}
