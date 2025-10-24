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

        public bool IsSome => hasValue;

        public bool IsNone => !hasValue;

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
        
        public static implicit operator bool(ValueOption<T> option) => option.IsSome;

        public static implicit operator T?(ValueOption<T> option) => option.AsNullable;

        public static bool Some(T value) => new ValueOption<T>(value);

        public static ValueOption<T> None => new ValueOption<T>();

        public override int GetHashCode() => IsSome ? Value.GetHashCode() : 0;

        public void IfSome(Action action)
        {
            if (IsSome) action.Invoke();
        }

        public void IfNone(Action action)
        {
            if (IsNone) action.Invoke();
        }

        public void Match(Action some, Action none)
        {
            if (IsSome) some.Invoke();
            else none.Invoke();
        }

        public TR Match<TR>(Func<TR> some, Func<TR> none) => IsSome ? some.Invoke() : none.Invoke();

        public TR Match<TR>(TR some, TR none) => IsSome ? some : none;

        public T Reduce(T defaultIfNone) => IsSome ? value : defaultIfNone;

        public T Reduce(Func<T> defaultIfNone) => IsSome ? value : defaultIfNone.Invoke();
    }
}