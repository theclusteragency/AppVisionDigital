using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reflection;
using Akavache;
using AppFom.Models;

namespace AppFom.Helpers
{
    public class CacheManager
    {

        /// <summary>
        /// Inicializa el cache y crea el esquema con los objetos que lo componen.
        /// </summary>
        public void Init()
        {

            SetCachedObject<User>(CacheKeys.User,
                                  GetCachedObject<User>(CacheKeys.User) == null ?
                                  new User() : GetCachedObject<User>(CacheKeys.User));

        }

        /// <summary>
        /// Almacena o actualiza cualquier tipo de objeto en cache
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="data">Data</param>
        /// <param name="expday">Expday</param>
        /// <typeparam name="T">Parametro genérico</typeparam>
        public void SetCachedObject<T>(string key, T data, int expday = 10000)
        {

            var cachedObj = GetCachedObject<T>(key);

            if (cachedObj != null)
            {

                UpdateCachedObject<T>(key, cachedObj, data);

            }
            else
            {

                BlobCache.LocalMachine.InsertObject(key,
                                                     data,
                                                    DateTimeOffset.Now
                                                    .AddHours(expday)
                                                    );

            }

        }

        /// <summary>
        /// Obtiene cualquier objeto almacenado en cache a través de su llave de
        /// acceso
        /// </summary>
        /// <returns>The cached object.</returns>
        /// <param name="key">Key.</param>
        /// <typeparam name="T">Parametro genérico</typeparam>
        public T GetCachedObject<T>(string key)
        {

            var result = BlobCache
                          .LocalMachine
                          .GetObject<T>(key)
                          .Catch<T, KeyNotFoundException>(ex => Observable.Return((T)default(T)))
                          .Wait();

            return result;
        }

        /// <summary>
        /// Actualiza cualquier objeto en cache
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="data">Data.</param>
        /// <param name="ndata">Ndata.</param>
        /// <param name="expday">Expday.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected void UpdateCachedObject<T>(string key, T data, T ndata, int expday = 10000)
        {
            var nobj = data;
            if (!(data is IList || data is string))
            {
                nobj = MergeObjects(data, ndata);
            }

            BlobCache.LocalMachine.Invalidate(key);

            BlobCache.LocalMachine.InsertObject(key,
                                                 nobj,
                                                 DateTimeOffset
                                                .Now.AddHours(expday)
                                                );
        }

        /// <summary>
        /// Este método hace el merge de propiedades entre objetos del mismo tipo
        /// Objeto uno origen cache
        /// Objeto dos nuevo origen 
        /// </summary>
        /// <returns>Objeto compuesto</returns>
        /// <param name="obj1">Objeto cache</param>
        /// <param name="obj2">Nuevo objeto</param>
        protected T MergeObjects<T>(T obj1, T obj2)
        {
            var objResult = Activator.CreateInstance(typeof(T));

            var allProperties = typeof(T).GetRuntimeProperties();
            foreach (var pi in allProperties)
            {
                object defaultValue;
                if (pi.PropertyType.GetTypeInfo().IsValueType)
                {
                    defaultValue = Activator.CreateInstance(pi.PropertyType);
                }
                else
                {
                    defaultValue = null;
                }

                var value = pi.GetValue(obj2, null);

                if (value != defaultValue)
                {
                    pi.SetValue(objResult, value, null);
                }
                else
                {
                    value = pi.GetValue(obj1, null);

                    if (value != defaultValue)
                    {
                        pi.SetValue(objResult, value, null);
                    }
                }
            }

            return (T)objResult;
        }

    }

}
