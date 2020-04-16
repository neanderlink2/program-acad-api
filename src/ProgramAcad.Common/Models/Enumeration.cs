﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProgramAcad.Common.Models
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; set; }

        public int Id { get; set; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        protected Enumeration()
        {

        }
        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Enumeration otherValue)
            {
                var typeMatches = GetType() == obj.GetType();
                var valueMatches = Id.Equals(otherValue.Id);

                return typeMatches && valueMatches;
            }

            return false;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                return default;

            return matchingItem;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

    }
}
