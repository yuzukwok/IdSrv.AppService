﻿using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdSrv.Lib
{
    internal static class StringExtensions
    {
        [DebuggerStepThrough]
        public static string ToSpaceSeparatedString(this IEnumerable<string> list)
        {
            var sb = new StringBuilder(100);

            foreach (var element in list)
            {
                sb.Append(element + " ");
            }

            return sb.ToString().Trim();
        }

        [DebuggerStepThrough]
        public static IEnumerable<string> FromSpaceSeparatedString(this string input)
        {
            input = input.Trim();
            return input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        [DebuggerStepThrough]
        public static bool IsMissing(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        [DebuggerStepThrough]
        public static bool IsMissingOrTooLong(this string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            if (value.Length > maxLength)
            {
                return true;
            }

            return false;
        }

        [DebuggerStepThrough]
        public static bool IsPresent(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static string EnsureTrailingSlash(this string url)
        {
            if (!url.EndsWith("/"))
            {
                return url + "/";
            }

            return url;
        }

        public static string RemoveLeadingSlash(this string url)
        {
            if (url != null && url.StartsWith("/"))
            {
                url = url.Substring(1);
            }

            return url;
        }

        public static string RemoveTrailingSlash(this string url)
        {
            if (url != null && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        public static string CleanUrlPath(this string url)
        {
            if (String.IsNullOrWhiteSpace(url)) url = "/";

            if (url != "/" && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        public static string AddQueryString(this string url, string query)
        {
            if (!url.Contains("?"))
            {
                url += "?";
            }
            else if (!url.EndsWith("&"))
            {
                url += "&";
            }

            return url + query;
        }

        public static string AddHashFragment(this string url, string query)
        {
            if (!url.Contains("#"))
            {
                url += "#";
            }

            return url + query;
        }

        public static string GetOrigin(this string url)
        {
            if (url != null && (url.StartsWith("http://") || url.StartsWith("https://")))
            {
                var idx = url.IndexOf("//", StringComparison.Ordinal);
                if (idx > 0)
                {
                    idx = url.IndexOf("/", idx + 2, StringComparison.Ordinal);
                    if (idx >= 0)
                    {
                        url = url.Substring(0, idx);
                    }
                    return url;
                }
            }

            return null;
        }

        public static Stream ToStream(this string s)
        {
            if (s == null) throw new ArgumentNullException("s");

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(s);
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
    internal static class ClaimExtensions
    {
        public static bool HasValue(this Claim claim)
        {
            return (claim != null && claim.Value.IsPresent());
        }
    }
    internal static class ScopeExtensions
    {
        [DebuggerStepThrough]
        public static string ToSpaceSeparatedString(this IEnumerable<Scope> scopes)
        {
            var scopeNames = from s in scopes
                             select s.Name;

            return string.Join(" ", scopeNames.ToArray());
        }

        [DebuggerStepThrough]
        public static IEnumerable<string> ToStringList(this IEnumerable<Scope> scopes)
        {
            var scopeNames = from s in scopes
                             select s.Name;

            return scopeNames;
        }

        [DebuggerStepThrough]
        public static bool IncludesAllClaimsForUserRule(this IEnumerable<Scope> scopes, ScopeType type)
        {
            foreach (var scope in scopes)
            {
                if (scope.Type == type)
                {
                    if (scope.IncludeAllClaimsForUser)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
