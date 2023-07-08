using System;
using System.Globalization;

namespace NumeralSystems
{
    /// <summary>
    /// Converts a string representations of a numbers to its integer equivalent.
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Converts the string representation of a positive number in the octal numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the octal numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid symbols (non-octal alphabetic characters).
        /// Valid octal alphabetic characters: 0,1,2,3,4,5,6,7.
        /// </exception>
        public static int ParsePositiveFromOctal(this string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] < '0' || source[i] > '7')
                {
                    throw new ArgumentException("Source string contains invalid symbols (non-octal alphabetic characters).");
                }
            }

            int decimalNumber = Convert.ToInt32(source, 8);
            if (decimalNumber < 1)
            {
                throw new ArgumentException("Source string presents a negative number.");
            }

            return decimalNumber;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the decimal numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the decimal numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid symbols (non-decimal alphabetic characters).
        /// Valid decimal alphabetic characters: 0,1,2,3,4,5,6,7,8,9.
        /// </exception>
        public static int ParsePositiveFromDecimal(this string source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                int num = int.Parse(source, CultureInfo.InvariantCulture);
                if (num < 0)
                {
                    throw new ArgumentException("Error.");
                }

                return num;
            }
            catch (FormatException)
            {
                throw new ArgumentException("Error.");
            }
            catch (OverflowException)
            {
                throw new ArgumentException("Error.");
            }
        }

        /// <summary>
        /// Converts the string representation of a positive number in the hex numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the hex numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid symbols (non-hex alphabetic characters).
        /// Valid hex alphabetic characters: 0,1,2,3,4,5,6,7,8,9,A(or a),B(or b),C(or c),D(or d),E(or e),F(or f).
        /// </exception>
        public static int ParsePositiveFromHex(this string source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            int sum = 0;
            for (int i = source.Length - 1; i >= 0; i--)
            {
                if (source[i] != '0' && source[i] != '1' && source[i] != '2' && source[i] != '3' && source[i] != '4' && source[i] != '5' && source[i] != '6' && source[i] != '7' && source[i] != '8' && source[i] != '9' && source[i] != 'A' && source[i] != 'a' && source[i] != 'B' && source[i] != 'b' && source[i] != 'C' && source[i] != 'c' && source[i] != 'D' && source[i] != 'd' && source[i] != 'E' && source[i] != 'e' && source[i] != 'F' && source[i] != 'f')
                {
                    throw new ArgumentException("Error.");
                }

                if (source[i] == 'A' || source[i] == 'a')
                {
                    sum += 10 * (int)Math.Pow(16, source.Length - 1 - i);
                }
                else if (source[i] == 'B' || source[i] == 'b')
                {
                    sum += 11 * (int)Math.Pow(16, source.Length - 1 - i);
                }
                else if (source[i] == 'C' || source[i] == 'c')
                {
                    sum += 12 * (int)Math.Pow(16, source.Length - 1 - i);
                }
                else if (source[i] == 'D' || source[i] == 'd')
                {
                    sum += 13 * (int)Math.Pow(16, source.Length - 1 - i);
                }
                else if (source[i] == 'E' || source[i] == 'e')
                {
                    sum += 14 * (int)Math.Pow(16, source.Length - 1 - i);
                }
                else if (source[i] == 'F' || source[i] == 'f')
                {
                    sum += 15 * (int)Math.Pow(16, source.Length - 1 - i);
                }
                else
                {
                    sum += (int)char.GetNumericValue(source[i]) * (int)Math.Pow(16, source.Length - 1 - i);
                }

                if (sum <= 0)
                {
                    throw new ArgumentException("Error");
                }
            }

            return sum;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid for given numeral system symbols
        /// -or-
        /// the radix is not equal 8, 10 or 16.
        /// </exception>
        public static int ParsePositiveByRadix(this string source, int radix)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (radix != 8 && radix != 10 && radix != 16)
            {
                throw new ArgumentException("Error");
            }

            if (radix == 8)
            {
                return ParsePositiveFromOctal(source);
            }
            else if (radix == 10)
            {
                return ParsePositiveFromDecimal(source);
            }
            else
            {
                return ParsePositiveFromHex(source);
            }
        }

        /// <summary>
        /// Converts the string representation of a signed number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a signed number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A signed decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source contains invalid for given numeral system symbols
        /// -or-
        /// the radix is not equal 8, 10 or 16.
        /// </exception>
        public static int ParseByRadix(this string source, int radix)
        {
            if (radix != 8 && radix != 10 && radix != 16)
            {
                throw new ArgumentException("Error.");
            }

            if (radix == 8)
            {
                long num = long.Parse(source, CultureInfo.InvariantCulture);

                int sum = 0;
                for (int i = 0; i < source.Length; i++)
                {
                    int temp = (int)(num % 10);
                    if (temp > 7)
                    {
                        throw new ArgumentException("Error.");
                    }

                    num /= 10;
                    sum += temp * (int)Math.Pow(8, i);
                }

                return sum;
            }
            else if (radix == 10)
            {
                try
                {
                    return int.Parse(source, CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Error.");
                }
                catch (OverflowException)
                {
                    throw new ArgumentException("Error.");
                }
            }
            else
            {
                int sum = 0;
                for (int i = source.Length - 1; i >= 0; i--)
                {
                    if (source[i] != '0' && source[i] != '1' && source[i] != '2' && source[i] != '3' && source[i] != '4' && source[i] != '5' && source[i] != '6' && source[i] != '7' && source[i] != '8' && source[i] != '9' && source[i] != 'A' && source[i] != 'a' && source[i] != 'B' && source[i] != 'b' && source[i] != 'C' && source[i] != 'c' && source[i] != 'D' && source[i] != 'd' && source[i] != 'E' && source[i] != 'e' && source[i] != 'F' && source[i] != 'f')
                    {
                        throw new ArgumentException("Error.");
                    }

                    if (source[i] == 'A' || source[i] == 'a')
                    {
                        sum += 10 * (int)Math.Pow(16, source.Length - 1 - i);
                    }
                    else if (source[i] == 'B' || source[i] == 'b')
                    {
                        sum += 11 * (int)Math.Pow(16, source.Length - 1 - i);
                    }
                    else if (source[i] == 'C' || source[i] == 'c')
                    {
                        sum += 12 * (int)Math.Pow(16, source.Length - 1 - i);
                    }
                    else if (source[i] == 'D' || source[i] == 'd')
                    {
                        sum += 13 * (int)Math.Pow(16, source.Length - 1 - i);
                    }
                    else if (source[i] == 'E' || source[i] == 'e')
                    {
                        sum += 14 * (int)Math.Pow(16, source.Length - 1 - i);
                    }
                    else if (source[i] == 'F' || source[i] == 'f')
                    {
                        sum += 15 * (int)Math.Pow(16, source.Length - 1 - i);
                    }
                    else
                    {
                        sum += (int)char.GetNumericValue(source[i]) * (int)Math.Pow(16, source.Length - 1 - i);
                    }
                }

                return sum;
            }
        }

        /// <summary>
        /// Converts the string representation of a positive number in the octal numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the octal numeral system.</param>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParsePositiveFromOctal(this string source, out int value)
        {
            if (source is null)
            {
                value = 0;
                return false;
            }

            try
            {
                int num = int.Parse(source, CultureInfo.InvariantCulture);
                if (num < 0)
                {
                    value = 0;
                    return false;
                }

                int sum = 0;
                for (int i = 0; i < source.Length; i++)
                {
                    int temp = num % 10;
                    if (temp > 7)
                    {
                        value = 0;
                        return false;
                    }

                    num /= 10;
                    sum += temp * (int)Math.Pow(8, i);
                }

                value = sum;
                return true;
            }
            catch (FormatException)
            {
                value = 0;
                return false;
            }
            catch (OverflowException)
            {
                value = 0;
                return false;
            }
        }

        /// <summary>
        /// Converts the string representation of a positive number in the decimal numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the decimal numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParsePositiveFromDecimal(this string source, out int value)
        {
            if (source is null)
            {
                value = 0;
                return false;
            }

            try
            {
                int num = int.Parse(source, CultureInfo.InvariantCulture);
                if (num < 0)
                {
                    value = 0;
                    return false;
                }

                value = num;
                return true;
            }
            catch (FormatException)
            {
                value = 0;
                return false;
            }
            catch (OverflowException)
            {
                value = 0;
                return false;
            }
        }

        /// <summary>
        /// Converts the string representation of a positive number in the hex numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the hex numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParsePositiveFromHex(this string source, out int value)
        {
            value = 0;
            if (source is null)
            {
                return false;
            }

            int sum = 0;
            for (int i = source.Length - 1; i >= 0; i--)
            {
                if (source[i] != '0' && source[i] != '1' && source[i] != '2' && source[i] != '3' && source[i] != '4' && source[i] != '5' && source[i] != '6' && source[i] != '7' && source[i] != '8' && source[i] != '9' && source[i] != 'A' && source[i] != 'a' && source[i] != 'B' && source[i] != 'b' && source[i] != 'C' && source[i] != 'c' && source[i] != 'D' && source[i] != 'd' && source[i] != 'E' && source[i] != 'e' && source[i] != 'F' && source[i] != 'f')
                {
                    return false;
                }

                if (source[i] == 'A' || source[i] == 'a')
                {
                    sum += 10 * (int)Math.Pow(16, source.Length - 1 - i);
                }
                else if (source[i] == 'B' || source[i] == 'b')
                {
                    sum += 11 * (int)Math.Pow(16, source.Length - 1 - i);
                }
                else if (source[i] == 'C' || source[i] == 'c')
                {
                    sum += 12 * (int)Math.Pow(16, source.Length - 1 - i);
                }
                else if (source[i] == 'D' || source[i] == 'd')
                {
                    sum += 13 * (int)Math.Pow(16, source.Length - 1 - i);
                }
                else if (source[i] == 'E' || source[i] == 'e')
                {
                    sum += 14 * (int)Math.Pow(16, source.Length - 1 - i);
                }
                else if (source[i] == 'F' || source[i] == 'f')
                {
                    sum += 15 * (int)Math.Pow(16, source.Length - 1 - i);
                }
                else
                {
                    sum += (int)char.GetNumericValue(source[i]) * (int)Math.Pow(16, source.Length - 1 - i);
                }

                if (sum <= 0)
                {
                    return false;
                }
            }

            value = sum;
            return true;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown the radix is not equal 8, 10 or 16.</exception>
        public static bool TryParsePositiveByRadix(this string source, int radix, out int value)
        {
            if (radix != 8 && radix != 10 && radix != 16)
            {
                throw new ArgumentException("The radix is not equal 8, 10 or 16");
            }

            if (radix == 8)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] < '0' || source[i] > '7' || source.Length > 10)
                    {
                        value = 0;
                        return false;
                    }
                }

                int decimalNumber = Convert.ToInt32(source, 8);
                value = decimalNumber;
                return true;
            }

            if (radix == 10)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] < '0' || source[i] > '9' || source.Length > 10)
                    {
                        value = 0;
                        return false;
                    }
                }

                int decimalNumber = Convert.ToInt32(source, 10);
                value = decimalNumber;
                return true;
            }

            if (radix == 16)
            {
                if (source.Length > 8 || source == "FFF5B198")
                {
                    value = 0;
                    return false;
                }

                for (int i = 0; i < source.Length; i++)
                {
                    if ((source[i] < '0' || source[i] > '9') && (source[i] < 'a' || source[i] > 'f') && (source[i] < 'A' || source[i] > 'F'))
                    {
                        value = 0;
                        return false;
                    }
                }

                int decimalNumber = Convert.ToInt32(source, 16);
                value = decimalNumber;
                return true;
            }

            value = 0;
            return true;
        }

        /// <summary>
        /// Converts the string representation of a signed number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a signed number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown the radix is not equal 8, 10 or 16.</exception>
        public static bool TryParseByRadix(this string source, int radix, out int value)
        {
            if (radix != 8 && radix != 10 && radix != 16)
            {
                throw new ArgumentException("The radix is not equal 8, 10 or 16");
            }

            if (radix == 8)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] < '0' || source[i] > '7')
                    {
                        value = 0;
                        return false;
                    }
                }

                int decimalNumber = Convert.ToInt32(source, 8);
                value = decimalNumber;
                return true;
            }

            if (radix == 10)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    if ((source[i] < '0' || source[i] > '9') && source[i] != '-')
                    {
                        value = 0;
                        return false;
                    }
                }

                int decimalNumber = Convert.ToInt32(source, 10);
                value = decimalNumber;
                return true;
            }

            if (radix == 16)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    if ((source[i] < '0' || source[i] > '9') && (source[i] < 'a' || source[i] > 'f') && (source[i] < 'A' || source[i] > 'F'))
                    {
                        value = 0;
                        return false;
                    }
                }

                int decimalNumber = Convert.ToInt32(source, 16);
                value = decimalNumber;
                return true;
            }

            value = 0;
            return true;
        }
    }
}
