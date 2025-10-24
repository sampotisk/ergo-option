#nullable enable
using System;
using UnityEngine;

namespace ErgoOption
{
    [Serializable]
    public struct Option<T> where T : class?
    {
        [SerializeField] private T value;

        public T Value
        {
            get => value;
            set => this.value = value;
        }

        public T? AsNullable
        {
            get
            {
                if (value is not Component unityObject) return value;

                return unityObject ? value : null;
            }
            set
            {
                if (!IsNull(value))
                {
                    this.value = value!;
                }
            }
        }

        public Option(T value)
        {
            this.value = value;
        }

        public static implicit operator Option<T>(T value) => new(value);
        
        public static implicit operator bool(Option<T> option) => !IsNull(option.value);

        public static implicit operator T?(Option<T> option) => option.AsNullable;

        public static bool Some(T value) => new Option<T>(value);

        public static Option<T> None => new Option<T>();

        // ReSharper disable once ParameterHidesMember
        public bool Try(out T? value)
        {
            if (!IsNull(this.value))
            {
                value = this.value;
                return true;
            }

            value = null;
            return false;
        }

        private static bool IsNull(T? nullable)
        {
            if (nullable is not Component unityObject) return nullable == null;

            return !unityObject;
        }
    }
}