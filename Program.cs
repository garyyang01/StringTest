using System;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace StringTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Interpolation();
            Console.WriteLine();
            AtSymbol();
            Console.WriteLine();
            MixMoneyAndAtSymbol();
            Console.WriteLine();
            DateTimeFormat();
            Console.WriteLine();
            NumberFormat();
            Console.WriteLine();
            CustomFormat();
            var summery= BenchmarkRunner.Run<StringAdder>();
        }

        #region StringExample

        private static void Interpolation()
        {
            const string str = "1";
            const string str2 = "2";
            const string str3 = "3";
            Console.WriteLine("Interpolation Example:");
            Console.WriteLine(@"$""{str}+{str2}={str3}""");
            Console.WriteLine($"{str}+{str2}={str3}");
            Console.WriteLine();
            Console.WriteLine(@"string.Format(""{0}+{1}+{2}"",str,str2,str3)");
            Console.WriteLine(string.Format("{0}+{1}+{2}", str, str2, str3));
        }

        private static void AtSymbol()
        {
            const string case1 = @"
            select a, b ,c
            from d
            Where e";
            const string case2 = @"Https://www.google.com";
            Console.WriteLine("AtSymbol Example:");
            Console.WriteLine(@"@""
            select a, b ,c
            from d
            Where e""");
            Console.WriteLine(case1);
            Console.WriteLine();
            Console.WriteLine(@"@""Https://www.google.com""");
            Console.WriteLine(case2);
        }

        private static void MixMoneyAndAtSymbol()
        {
            var columns = new string[] { "A", "B", "C", "D" };
            const string table = "E";
            const string where = @"A=1 and B=2 and c<>3";
            var sql = $@"
                SELECT {string.Join(",", columns)}
                FROM {table}
                WHERE {where}";
            Console.WriteLine("MixMoneyAndAtSymbol Example:");
            Console.WriteLine(@"
                $@""SELECT { string.Join("","", columns)}
                FROM { table}
                WHERE { where}""
            ");
            Console.WriteLine(sql);
        }

        private static void DateTimeFormat()
        {
            var time = new DateTime(2021, 6, 3, 14, 0, 0);
            Console.WriteLine("DateTimeFormat Example:");
            Console.WriteLine(@"var time = new DateTime(2021, 6, 3, 14, 0, 0);");
            Console.WriteLine(@"time.ToString(""yyyy-MM-dd hh:mm:ss"")");
            Console.WriteLine(time.ToString("yyyy-MM-dd hh:mm:ss"));
            Console.WriteLine();
            Console.WriteLine(@"time.ToString(""yyyy-MM-dd HH:mm:ss"")");
            Console.WriteLine(time.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine();
            Console.WriteLine(@"$""{time:yyyy-MM-dd HH:mm:ss}""");
            Console.WriteLine($"{time:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine();
            Console.WriteLine(@"time.ToString(""hh:mm:ss tt zz"")");
            Console.WriteLine(time.ToString("hh:mm:ss tt zz"));
        }

        private static void NumberFormat()
        {
            const decimal dec = 10000.2365m;
            const int integer = 254;
            Console.WriteLine("NumberFormat Example:");
            Console.WriteLine(@"const decimal dec = 10000.2365m;");
            Console.WriteLine(@"const int integer = 254;");
            Console.WriteLine();
            Console.WriteLine(@"dec.ToString(""P"")");
            Console.WriteLine(dec.ToString("P"));
            Console.WriteLine();
            Console.WriteLine(@"dec.ToString(""C"")");
            Console.WriteLine(dec.ToString("C"));
            Console.WriteLine();
            Console.WriteLine(@"dec.ToString(""N"")");
            Console.WriteLine(dec.ToString("N"));
            Console.WriteLine();
            Console.WriteLine(@"integer.ToString(""X"")");
            Console.WriteLine(integer.ToString("X"));
            Console.WriteLine();
            Console.WriteLine(@"dec.ToString(""000000.##"")");
            Console.WriteLine(dec.ToString("000000.##"));
            Console.WriteLine();
            Console.WriteLine(@"dec.ToString(""000.#"")");
            Console.WriteLine(dec.ToString("000.#"));
        }

        private static void CustomFormat()
        {
            var people = new People();
            Console.WriteLine("CustomFormat Example:");
            Console.WriteLine(@"var people = new People();");
            Console.WriteLine();
            Console.WriteLine(@"people.ToString()");
            Console.WriteLine(people.ToString());
            Console.WriteLine();
            Console.WriteLine(@"people.ToString(""H"")");
            Console.WriteLine(people.ToString("H"));
            Console.WriteLine();
            Console.WriteLine(@"people.ToString(""W"")");
            Console.WriteLine(people.ToString("W"));
            Console.WriteLine();
            Console.WriteLine(@"people.ToString(""ALL"")");
            Console.WriteLine(people.ToString("ALL"));
        }

        #endregion

    }

    public class People : IFormattable
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly decimal _height;
        private readonly decimal _weight;

        public People()
        {
            _firstName = "Harry";
            _lastName = "Potter";
            _height = 188.0m;
            _weight = 54.5m;
        }

        public override string ToString()
        {
            return $"{_firstName} {_lastName}";
        }

        public string ToString(string format) => ToString(format, null);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return format switch
            {
                "H" => _height.ToString("###.##"),
                "W" => _weight.ToString("##.#"),
                "ALL" => $"Hi. My name is {_firstName} {_lastName}. I am {_height:###.##} tall and weigh {_weight:##.#}.",
                _ => throw new FormatException($"invalid format string {format}")
            };
        }
    }

    [MemoryDiagnoser]
    public class StringAdder
    {
        public readonly string TestString="TestString";

        public readonly Trans Txn = new Trans(){PlayerAccountId = "123456", PlayerCustId = 123456, ExternalRefno = "text1234", Amount = 10.0m, Type = "Deposit"};
        [Benchmark]
        public void Case1_String_Add_With_Different_String()
        {
            String s = String.Empty;
            for (int i = 0; i < 10; i++)
            {
                s += i.ToString();
            }
        }

        [Benchmark]
        public void Case1_StringBuilder_Add_With_Different_String()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                sb.Append(i.ToString());
            }
            string s = sb.ToString();
        }

        [Benchmark]
        public void Case2_String_Add_With_Same_String()
        {
            string s = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                s += TestString;
            }
        }
        [Benchmark]
        public void Case2_StringBuilder_Add_With_Same_String()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                sb.Append(TestString);
            }
            string s = sb.ToString();
        }

        [Benchmark]
        public void Case3_String_Add_With_Value()
        {
            string s = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                s += "TestString";
            }

        }
        [Benchmark]
        public void Case3_StringBuilder_Add_With_Value()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                sb.Append("TestString");
            }
            string s = sb.ToString();
        }
        [Benchmark]
        public void Case4_String_Add_Through_Plus()
        {
            string s = "TestString" + "TestString" + "TestString" + "TestString" + "TestString" + "TestString" + "TestString" +
                       "TestString" + "TestString" + "TestString";
        }
        [Benchmark]
        public void Case4_String_Add_Without_Plus()
        {
            string s = "TestStringTestStringTestStringTestStringTestStringTestStringTestStringTestStringTestStringTestString";
        }

        [Benchmark]
        public void Case4_String_Add_With_Interpolation()
        {
            string s = $"{TestString}{TestString}{TestString}{TestString}{TestString}{TestString}{TestString}{TestString}{TestString}{TestString}";
        }
        [Benchmark]
        public void Case5_Triton_Alert_Format()
        {
            var sbBody = new StringBuilder();
            sbBody.Append("<table border=\"1\"><tr><th> AccountId </th><th> CustomerId </th><th> TransactionId </th><th> Amount </th><th> TransactionType </th></tr>");
            sbBody.Append("<tr><td>" + Txn.PlayerCustId + "</td><td>" + Txn.PlayerCustId + "</td><td>" + Txn.ExternalRefno + "</td><td>" + Txn.Amount + "</td><td>" + Txn.Type + "</td></tr><br>");
            sbBody.Append("Please follow this SOP to deal with : https://kelutral.atlassian.net/wiki/spaces/KLDP/pages/159744061/Transfer+Transaction+Timeout+SOP");
            string s = sbBody.ToString();
        }
        [Benchmark]
        public void Case5_Enhance_Alert_Format()
        {
            string s =
                @"<table border=""1""><tr><th> AccountId </th><th> CustomerId </th><th> TransactionId </th><th> Amount </th><th> TransactionType </th></tr>"
                + "<tr><td>" + Txn.PlayerCustId + "</td><td>" + Txn.PlayerCustId + "</td><td>" + Txn.ExternalRefno +
                "</td><td>" + Txn.Amount + "</td><td>" + Txn.Type + "</td></tr><br>"
                + @"Please follow this SOP to deal with : https://kelutral.atlassian.net/wiki/spaces/KLDP/pages/159744061/Transfer+Transaction+Timeout+SOP";
        }



    }

    public class Trans
    {
        public string PlayerAccountId { get; set; }
        public int PlayerCustId { get; set; }
        public string ExternalRefno { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
    }
}