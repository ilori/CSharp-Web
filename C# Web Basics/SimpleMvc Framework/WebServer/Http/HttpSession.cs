﻿namespace WebServer.Http
{
    using System.Collections.Generic;
    using Common;
    using Contracts;

    public class HttpSession : IHttpSession
    {
        private readonly IDictionary<string, object> values;

        public HttpSession(string id)
        {
            CoreValidator.ThrowIfNullOrEmpty(id, nameof(id));

            this.Id = id;
            this.values = new Dictionary<string, object>();
        }

        public string Id { get; }

        public void Add(string key, object value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNull(value, nameof(value));

            this.values[key] = value;
        }

        public void Clear() => this.values.Clear();

        public object Get(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if (!this.values.ContainsKey(key))
            {
                return null;
            }

            return this.values[key];
        }

        public void Remove(string key)
        {
            this.values.Remove(key);
        }

        public T Get<T>(string key)
            => (T) this.Get(key);

        public bool Contains(string key) => this.values.ContainsKey(key);
    }
}