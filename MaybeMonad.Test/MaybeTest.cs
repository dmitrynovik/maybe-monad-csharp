using FluentAssertions;
using Xunit;

namespace MaybeMonad.Test
{
    public class MaybeTest
    {
        [Fact]
        public void When_Value_Null_FromValue_Equals_None() => Maybe<string>.None().Should().Be(Maybe<string>.FromValue(null));

        [Fact]
        public void When_Value_Some_FromValue_Equals_Value() => Maybe<string>.Some("abc").Should().Be("abc");

        [Fact]
        public void When_Value_Some_FromValue_Equals_Some() => Maybe<string>.Some("abc").Should().Be(Maybe<string>.Some("abc"));

        [Fact]
        public void When_Value_Some_It_DoesNot_Equal_None() => Maybe<string>.Some("abc").Should().NotBe(Maybe<string>.None());
    }
}
