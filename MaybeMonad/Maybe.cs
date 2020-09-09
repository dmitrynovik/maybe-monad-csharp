using System;

namespace MaybeMonad
{
    public class Maybe<T>
    {
        private readonly T _value;

        private Maybe(T value) { _value = value; }

        public static Maybe<T> Some(T value)
        {
            if (Equals(default, value))
                throw new ArgumentException("Cannot create Maybe.Some with default value", nameof(value));

            return new Maybe<T>(value);
        }

        public static Maybe<T> None() => new Maybe<T>(default);

        public static Maybe<T> FromValue(T value) => Equals(default, value) ? None() : Some(value);

        public T GetOrElse(T defaultValue) => Equals(default, _value) ? defaultValue : _value;

        public Maybe<R> Map<R>(Func<Maybe<T>, R> f) => Equals(default, _value) ? Maybe<R>.None() : Maybe<R>.Some(f(this));

        public Maybe<R> FlatMap<R>(Func<Maybe<T>, Maybe<R>> f) => Equals(default, _value) ? Maybe<R>.None() : f(this);

        public override string ToString() => Equals(default, _value) ? "None" : $"Some<{_value}>";

        public override bool Equals(object obj)
        {
            return (obj is Maybe<T> maybe) && Equals(_value, maybe._value) || (obj is T t && Equals(t, _value));
        }

        public override int GetHashCode() => Equals(default, _value) ? 0 : _value.GetHashCode();
    }
}
