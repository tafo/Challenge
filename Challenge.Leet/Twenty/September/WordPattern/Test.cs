﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Challenge.Leet.Twenty.September.WordPattern
{
    public class Test
    {
        private readonly ITestOutputHelper _outputHelper;
        public Test(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [MemberData(nameof(InputAndOutput))]
        public void Check(string pattern, string terms, bool output)
        {
            var solution = new Solution();
            var timer = Stopwatch.StartNew();
            solution.WordPattern(pattern, terms).Should().Be(output);
            timer.Stop();
            _outputHelper.WriteLine($"{timer.ElapsedTicks}");
        }

        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public static IEnumerable<object[]> InputAndOutput()
        {
            return new List<object[]>
            {
                new object[]{"", "", true},
                new object[]{"", "cat", false},
                new object[]{"a", "", false},
                new object[]{"ab", "cat cat", false},
                new object[]{"aa", "cat cat", true},
                new object[]{"abba", "dog cat cat dog", true},
                new object[]{"abba", "dog cat cat fish", false},
                new object[]{"aaaa", "dog cat cat dog", false},
                new object[]{"abba", "dog dog dog dog", false}
            };
        }
    }
}