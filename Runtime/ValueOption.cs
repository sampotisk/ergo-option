#nullable enable
using System;
using UnityEngine;

namespace ErgoOption
{
    [Serializable]
    public struct ValueOption<T> where T : struct
    {
        [SerializeField] private T value;
        [SerializeField] private bool hasValue;

        public T Value
        {
            get => value;
            set => this.value = value;
        }

        public T? AsNullable
        {
            get => hasValue ? value : null;
            set
            {
                if (value != null)
                {
                    this.value = value.Value;
                    hasValue = true;
                }
                else
                {
                    this.value = default;
                    hasValue = false;
                }
            }
        }

        public ValueOption(T value)
        {
            this.value = value;
            hasValue = true;
        }

        public static implicit operator ValueOption<T>(T value) => new(value);
        
        public static implicit operator bool(ValueOption<T> option) => option.hasValue;

        public static implicit operator T?(ValueOption<T> option) => option.AsNullable;

        public static bool Some(T value) => new ValueOption<T>(value);

        public static ValueOption<T> None => new ValueOption<T>();

        // ReSharper disable once ParameterHidesMember
        public bool Try(out T value)
        {
            if (hasValue)
            {
                value = this.value;
                return true;
            }

            value = default;
            return false;
        }
    }
}