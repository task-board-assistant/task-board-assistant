﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaskBoardAssistant.Core.Models;
using TaskBoardAssistant.Core.Services.Resources;

namespace TaskBoardAssistant.Core.Services
{
    public static class Extensions
    {
        public static bool NullCheckEquals<T>(this T item1, T item2)
        {
            return (item1 == null && item2 == null) ||
                (item1 != null && item2 != null && item1.Equals(item2));
        }

        public static TEnum GetEnum<TEnum>(this IDictionary<string, string> dictionary, string key)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), dictionary[key], true);
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
             TKey key,
             TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static TValue GetKeyOrThrow<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string message)
        {
            try
            {
                return dictionary[key];
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException(message);
            }
        }

        public static bool NotNullAndEquals<T>(this T t1, T t2)
        {
            return t1 != null && t1.Equals(t2);
        }

        public static bool IsNullOrAfter(this DateTime? t1, DateTime? t2)
        {
            return t1 == null || t1 > t2;
        }

        public static bool IsNullOrBefore(this DateTime? t1, DateTime? t2)
        {
            return t1 == null || t1 < t2;
        }

        public static bool IsNullOrEquals<T>(this T t1, T t2)
        {
            return t1 == null || t1.Equals(t2);
        }

        public static bool EqualsIgnoreCase(this string t1, string t2)
        {
            return t1.ToLower().Equals(t2.ToLower());
        }

        public static bool IsNullOrEqualsIgnoreCase(this string t1, string t2)
        {
            return t1 == null || (t2 != null && t1.ToLower().Equals(t2.ToLower()));
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key)
        {
            return dictionary.GetValueOrDefault(key, default(TValue));
        }

        public static DateTime? ToRelativeDateTime(this string s)
        {
            if (s == null)
            {
                return null;
            }
            var split = s.Split('@');
            if (split.Length == 2)
            {
                var date = split[0];
                var time = split[1];
                DateTime dateOnly = date.ToDate();
                TimeSpan timeOnly = time.ToTime();
                return dateOnly.Date.Add(timeOnly);
            }
            else if (split.Length == 1)
            {
                return s.ToDate();
            }
            else
            {
                throw new Exception("Not a valid relative datetime");
            }
        }

        private static DateTime GetDay(string s)
        {
            var now = DateTime.Now.Date;
            switch (s.ToLower())
            {
                case "today":
                    return now;
                case "tomorrow":
                    return now.AddDays(1);
                case "sunday":
                    return now.NextDayOfWeek(DayOfWeek.Sunday);
                case "monday":
                    return now.NextDayOfWeek(DayOfWeek.Monday);
                case "tuesday":
                    return now.NextDayOfWeek(DayOfWeek.Tuesday);
                case "wednesday":
                    return now.NextDayOfWeek(DayOfWeek.Wednesday);
                case "thursday":
                    return now.NextDayOfWeek(DayOfWeek.Thursday);
                case "friday":
                    return now.NextDayOfWeek(DayOfWeek.Friday);
                case "saturday":
                    return now.NextDayOfWeek(DayOfWeek.Saturday);
                default:
                    throw new Exception("Invalid relative date");
            }
        }

        public static DateTime ToDate(this string s)
        {
            if (s.Contains('+'))
            {
                var split = s.Split('+');
                var day = GetDay(split[0]);
                var plus = int.Parse(split[1]);
                return day.AddDays(plus);
            }
            return GetDay(s);
        }

        public static TimeSpan ToTime(this string s)
        {
            if (TimeSpan.TryParse(s, out TimeSpan result))
                return result;
            return default(TimeSpan);
        }

        public static DateTime NextDayOfWeek(this DateTime d, DayOfWeek day)
        {
            return d.AddDays(d.DayOfWeek.DaysToAdd(day));
        }

        public static int DaysToAdd(this DayOfWeek current, DayOfWeek target)
        {
            if(current > target)
            {
                return 7 - (current - target);
            }
            if(current < target)
            {
                return target - current;
            }
            return 0;
        }

        public static DateTime? ToDateTime(this string s)
        {
            if (DateTime.TryParse(s, out DateTime result))
                return result;
            return null;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> data)
        {
            return data == null || !data.Any();
        }

        public static ResourceService GetResourceService(this ITaskBoardFactory factory, ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Board:
                case ResourceType.Repo:
                    return factory.BoardService;
                case ResourceType.List:
                    return factory.ListService;
                case ResourceType.Card:
                    return factory.CardService;
                case ResourceType.Label:
                    return factory.LabelService;
                default:
                    throw new Exception("Non-supported resource type");
            }
        }
    }
}
