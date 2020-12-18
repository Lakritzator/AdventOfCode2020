using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace AdventOfCode.Solutions
{
    public class Day4 : AdventOfCodeBase
    {
        private readonly List<Dictionary<string, string>> _passports = new List<Dictionary<string, string>>();

        /// <summary>
        /// Add passport data to the list of passports
        /// </summary>
        /// <param name="currentPassport"></param>
        private void AddPassport(string currentPassport)
        {
            if (string.IsNullOrEmpty(currentPassport))
            {
                return;
            }
            var passport = Regex
                        .Matches(currentPassport, @"([a-zA-Z0-9#]+):([a-zA-Z0-9#]+)\s+")
                        .Select(m => (Key: m.Groups[1].Value, m.Groups[2].Value))
                        .ToDictionary(kv => kv.Key, k => k.Value);
            _passports.Add(passport);
        }

        public Day4()
        {
            Assert.True(File.Exists(InputFilename));

            var passportFile = File.ReadAllLines(this.InputFilename);
            var passportString = "";
            foreach(var passportLine in passportFile)
            {
                if (!string.IsNullOrWhiteSpace(passportLine))
                {
                    passportString += passportLine + " ";
                }
                else
                {
                    AddPassport(passportString);
                    passportString = "";
                }
            }
            // Process the last
            AddPassport(passportString);
        }

        private readonly Func<Dictionary<string,string>, bool> _isValidPassportSimple = p => p.Count == 8 || (p.Count == 7 && !p.ContainsKey("cid"));

        private readonly Func<Dictionary<string, string>, bool> _isValidPassport = p => {
            foreach (var key in p.Keys)
            {
                var value = p[key];
                switch (key)
                {
                    case "byr":
                        // Birth Year - four digits; at least 1920 and at most 2002.
                        var birthYear = int.Parse(value);
                        if (birthYear >= 1920 && birthYear <= 2002)
                        {
                            break;
                        }
                        return false;
                    case "iyr":
                        // Issue Year - four digits; at least 2010 and at most 2020.
                        var issueYear = int.Parse(value);
                        if (issueYear >= 2010 && issueYear <= 2020)
                        {
                            break;
                        }
                        return false;
                    case "eyr":
                        // (Expiration Year) - four digits; at least 2020 and at most 2030.
                        var expirationYear = int.Parse(value);
                        if (expirationYear >= 2020 && expirationYear <= 2030)
                        {
                            break;
                        }
                        return false;
                    case "hgt":
                        // (Height) - a number followed by either cm or in:
                        // If cm, the number must be at least 150 and at most 193.
                        // If in, the number must be at least 59 and at most 76.
                        var match = Regex.Matches(value, @"^(\d+)(cm|in)$");
                        if (match.Count != 1)
                        {
                            return false;
                        }
                        var height = int.Parse(match[0].Groups[1].Value);

                        switch (match[0].Groups[2].Value)
                        {
                            case "in":
                                if (height >= 59 && height <= 76)
                                {
                                    break;
                                }
                                return false;
                            case "cm":
                                if (height >= 150 && height <= 193)
                                {
                                    break;
                                }
                                return false;
                            default:
                                return false;
                        }
                        break;
                    case "hcl":
                        //(Hair Color) - a # followed by exactly six characters 0-9 or a-f.
                        if (Regex.IsMatch(value, @"^#[a-f0-9]{6}$"))
                        {
                            break;
                        }
                        return false;
                    case "ecl":
                        // (Eye Color) - exactly one of: amb blu brn gry grn hzl oth
                        if (p[key].Length == 3 && "amb,blu,brn,gry,grn,hzl,oth".IndexOf(value, StringComparison.Ordinal) >= 0)
                        {
                            break;
                        }
                        return false;
                    case "pid":
                        // (Passport ID) - a nine-digit number, including leading zeroes.
                        if (Regex.IsMatch(value, @"^\d{9}$"))
                        {
                            break;
                        }
                        return false;
                    case "cid":
                        break;
                    default:
                        return false;
                }
            }
            if (p.Count < 7 || (p.Count == 7 && p.ContainsKey("cid")))
            {
                return false;
            }
            return true;
        };

        public override string AnswerPartOne()
        {
            var answer = _passports.Where(_isValidPassportSimple).Count();
            return $"{answer}";
        }

        public override string AnswerPartTwo()
        {
            var answer = _passports.Where(_isValidPassport).Count();
            return $"{answer}";
        }

    }
}
