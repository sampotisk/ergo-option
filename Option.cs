#nullable enable
using System;
using UnityEngine;

namespace ErgoOption
{
    [Serializable]
    public struct Option<T> where T : class
    {
        [SerializeField] private T value;

        public T Value
        {
            get => value;
            set => this.value = value;
        }

        public bool IsSome => !IsNull(value);

        public bool IsNone => IsNull(value);

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
        
        public static implicit operator bool(Option<T> option) => option.IsSome;

        public static implicit operator T?(Option<T> option) => option.AsNullable;

        public static bool Some(T value) => new Option<T>(value);

        public static bool None() => new Option<T>();

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

        private static bool IsNull(T? nullable)
        {
            if (nullable is not Component unityObject) return nullable == null;

            return !unityObject;
        }
    }
}