using System;
using Xunit;
using FluentAssertions;

namespace Monads
{
    public class Person
    {
        public Address Address { get; set; }
    }

    public class Address
    {
        public string PostCode { get; set; }
    }

    public class AddressHelper
    {
        public const string UnknownAddress = "Unknown";

        public string GetAddress(Person person)
        {
            if (person !=null && person.Address != null)
            {
                return person.Address.PostCode ?? UnknownAddress;
            }
            return UnknownAddress;
        }

        public string GetAddressWithMonads(Person person)
        {
            return person.With(p => p.Address).With(address => address.PostCode).Return(s => s, UnknownAddress);
        }
    }

    public static class Extensions
    {
        public static TResult With<TInput, TResult>(this TInput input, Func<TInput, TResult> func)
            where TResult : class 
            where TInput : class
        {
            return input == null ? null : func(input);
        }

        public static TResult Return<TInput, TResult>(this TInput input, Func<TInput, TResult> func, TResult failureValue)
            where TResult : class
            where TInput : class
        {
            return input == null ? failureValue : func(input);
        }
    }


    public class PersonTests
    {
        private readonly AddressHelper addressHelper = new AddressHelper();

        [Fact]
        public void given_null_person_should_return_unknwon_address()
        {
            var result = addressHelper.GetAddressWithMonads(null);
            result.Should().Be(AddressHelper.UnknownAddress);
        }

        [Fact]
        public void given_null_address_should_return_unknwon_address()
        {
            var result = addressHelper.GetAddressWithMonads(new Person());
            result.Should().Be(AddressHelper.UnknownAddress);
        }

        [Fact]
        public void given_null_postcode_should_return_unknwon_address()
        {
            var result = addressHelper.GetAddressWithMonads(new Person{Address = new Address()});
            result.Should().Be(AddressHelper.UnknownAddress);
        }

        [Fact]
        public void given_not_null_postcode_should_return_the_postcode()
        {
            const string postCode = "post code";
            var result = addressHelper.GetAddressWithMonads(new Person { Address = new Address{PostCode = postCode}});
            result.Should().Be(postCode);
        }
    }
}
