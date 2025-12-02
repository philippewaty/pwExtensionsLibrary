using System;
using pwExtensionsLibrary;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        public enum TraficLightsColors
        {
            [Description("Indicates Stop")]
            Red,
            [Description("Indicates Nothing")]
            Blue,
            [Description("Indicates Go")]
            Green
        }

        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }

        static void Main(string[] args)
        {
            int start = 10;
            int end = 20;
            int num = 10;

            bool isBetween = num.Between(start, end);
            bool isNotBetween = num.Between(start, end, false, false);

            DateTime ddeb = new DateTime(2020, 03, 01, 0, 0, 0);
            DateTime dfin = new DateTime(2020, 03, 31, 23, 59, 59);
            DateTime dtest = new DateTime(2020, 03, 14, 13, 59, 30);
            bool isDateBetween = dtest.Between(ddeb, dfin);
            DateTime dtest2 = new DateTime(2020, 02, 14, 13, 59, 30);
            isDateBetween = dtest2.Between(ddeb, dfin);

            TestString();

            Console.WriteLine("");
            TestConversion();

            Console.WriteLine("");
            TestDates();

            Console.WriteLine("");
            TestBoolean();

            Console.WriteLine("");
            TestNumber();

            Console.WriteLine("");
            TestDirectoryInfo();

            Console.WriteLine("");
            TestEnum();

            Console.WriteLine("");
            TestEnumerable();

            Console.WriteLine("");
            TestDBCommand();

            Console.WriteLine("");
            TestCollection();

            Console.WriteLine("Push a key...");
            Console.ReadKey();
        }

        static void TestString()
        {
            Console.WriteLine("STRING");
            Console.WriteLine("------");
            string originalString = "hello world";
            Console.WriteLine("Capitalize string : " + originalString.Capitalize());
            string test = "john.doe@gmail@com";
            Console.WriteLine("abc".Repeat(3));
            Console.WriteLine(test.Repeat(2));
            Console.WriteLine($"{test} is valid email : {test.IsValidEmailAddress()}");
            test = "john.doe@gmail";
            Console.WriteLine($"{test} is valid email : {test.IsValidEmailAddress()}");
            test = "john.doe@gmail.com";
            Console.WriteLine($"{test} is valid email : {test.IsValidEmailAddress()}");
            Console.WriteLine("Suppress leading zero from 000123456789 : {0}", "000123456789".SuppressLeadingZero());

            Console.WriteLine("Format");
            Console.WriteLine(@"Hello @firstname @lastname @mail".Format(new { firstname = "John", lastname = "Doe", mail = "john.doe@gmail.com" }));

            Console.WriteLine("SafeSubstring");
            Console.WriteLine("SafeSubstring".SafeSubstring(0, 5));
            Console.WriteLine("SafeSubstring".SafeSubstring(0, 50));
            Console.WriteLine("SafeSubstring".SafeSubstring(20, 5));

            string s = "this is a test";
            Console.WriteLine(String.Format("{0} ends with s : {1}", s, s.EnsureEndsWith("s")));
            Console.WriteLine(String.Format("{0} begins with 'this' : {1}", s, s.EnsureStartsWith("un ")));

            Console.WriteLine(StringHelper.RandomString(10, false, true, false, false));
            Console.WriteLine(StringHelper.RandomString(10, true, true, true, true));

            Console.WriteLine("The quick brown fox jumps over the lazy dog".LimitTextLength(20, false));
            Console.WriteLine("The quick brown fox jumps over the lazy dog".LimitTextLength(20, true));

            Console.WriteLine("Server in Electronics, Computers {0}", "Server".In("Electronics", "Computers"));
            Console.WriteLine("Server in Electronics, Computers, Server {0}", "Server".In("Electronics", "Computers", "Server"));

            string Text = "#Hello world. This is a [test]";
            Console.WriteLine("Delete chars [ ] : {0}", Text.DeleteChars('[', ']'));
            Console.WriteLine("Delete chars e,i : {0}", Text.DeleteChars('e', 'i'));

            string nullValue = null;
            Console.WriteLine(nullValue.IsNullThen("this is a null string"));

            Console.WriteLine("aaaaaaaabbbbccccddddeeeeeeeeeeee".FormatWithMask("Hello ########-#A###-####-####-############ Oww"));

            Console.WriteLine("abcdef".FormatWithMask("###-#"));

            Console.WriteLine("Firstchar of abcdef: " + "abcdef".FirstChar());

            string stringToValidate = "";
            Console.WriteLine($"Validate string '{stringToValidate}': " + stringToValidate.Validate(true));
            Console.WriteLine($"Validate string '{stringToValidate}': " + stringToValidate.Validate(false));

            stringToValidate = "string to validate";
            Console.WriteLine($"Validate string '{stringToValidate}': " + stringToValidate.Validate(true));
            Console.WriteLine($"Validate string '{stringToValidate}': " + stringToValidate.Validate(true, 2, 10));
            Console.WriteLine($"Validate string '{stringToValidate}': " + stringToValidate.Validate(true, 10, 15));

            stringToValidate = "string to val";
            Console.WriteLine($"Validate string '{stringToValidate}': " + stringToValidate.Validate(true, 10, 15));

            s = "test";
            Console.WriteLine(s.IfNullElse("Null Alternate Value"));
            s = "";
            Console.WriteLine(s.IfNullElse("Null Alternate Value"));

            List<string> testStrings = new List<string>();
            testStrings.Add("DIOM2816X14M");
            testStrings.Add("SJ2-35954A-SMT-TR");
            testStrings.Add("SOIC127P700X230-16M");
            testStrings.Add("SQFP50P1200X1200X170-64M");

            var query1 = from testLike in testStrings
                         where testLike.Like("DIOM%")
                         select testLike;
            Console.WriteLine(query1.FirstOrDefault());

            var query2 = from testLike in testStrings
                         where testLike.Like("SJ2%TR")
                         select testLike;
            Console.WriteLine(query2.FirstOrDefault());

            var query3 = from testLike in testStrings
                         where testLike.Like("SOIC127%16%")
                         select testLike;
            Console.WriteLine(query3.FirstOrDefault());

            var query4 = from testLike in testStrings
                         where testLike.Like("SQFP50P%64%")
                         select testLike;
            Console.WriteLine(query4.FirstOrDefault());

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Test IsNullOrEmpty".IsEqualIgnoreCase("TEST ISNULLOREMPTY"));
            Console.WriteLine("Test IsNullOrEmpty".IsEqualIgnoreCase("test isnullorempty"));
            Console.WriteLine("Test IsNullOrEmpty".IsEqualIgnoreCase("test is null or empty"));
        }

        static void TestConversion()
        {
            Console.WriteLine("CONVERSION");
            Console.WriteLine("----------");

            Console.WriteLine("".ConvertTo<Boolean>(false, true));
            Console.WriteLine("0".ConvertTo<bool>());
            Console.WriteLine("1".ConvertTo<bool>());
            Console.WriteLine("".ConvertTo<Int32>(0, true));
            Console.WriteLine("123".ConvertTo().ToInt32());
            Console.WriteLine(123f.ConvertTo<Int32>());
            Console.WriteLine(123.ConvertTo<Int64>());
            Console.WriteLine("Conversion failed with default value : " + "123d".ConvertTo<Double>(456, true));
            Console.WriteLine("Conversion succeeded : " + "123".ConvertTo<Double>(456, true));
            try
            {
                Console.WriteLine("123d".ConvertTo<Double>(456));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine("123d".IsNumeric());
            Console.WriteLine("123".IsNumeric());
            Console.WriteLine("123.456".IsNumeric());
            Console.WriteLine("123,456".IsNumeric());

            var value = "123";
            var numeric = value.ConvertTo().ToInt32();

            string sample = null;
            try
            {
            int? k = sample.ConvertTo<int?>(); // returns null 
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            DateTime? dn = "01/12/2008".ConvertTo<DateTime?>();
        }

        static void TestDates()
        {
            Console.WriteLine("DATES");
            Console.WriteLine("-----");

            DateTime myDate = DateTime.Today;

            Console.WriteLine(myDate.GetFirstDayOfMonth().ToString("dd/MM/yyyy"));

            Console.WriteLine(String.Format("Is date {0} is a weekend? : {1}", myDate.ToString("dd MMMM yyyy"), myDate.IsWeekend()));
            myDate = new DateTime(2012, 12, 23);
            Console.WriteLine(String.Format("Is date {0} is a weekend? : {1}", myDate.ToString("dd MMMM yyyy"), myDate.IsWeekend()));
            Console.WriteLine(String.Format("Is date {0} today ? : {1}", myDate.ToString("dd MMMM yyyy"), myDate.IsToday()));
            myDate = DateTime.Today;
            Console.WriteLine(String.Format("Is date {0} today ? : {1}", myDate.ToString("dd MMMM yyyy"), myDate.IsToday()));
            Console.WriteLine(String.Format("Day number {0} is {1}", myDate.ToString("dd MMMM yyyy"), myDate.GetDayOfYear()));

            myDate = new DateTime(2010, 2, 1);
            Console.WriteLine(String.Format("Number days in {0} : {1}", myDate.ToString("dd/MM/yyyy"), myDate.GetCountDaysOfMonth()));
            myDate = new DateTime(2012, 2, 1);
            Console.WriteLine(String.Format("Number days in {0} : {1}", myDate.ToString("dd/MM/yyyy"), myDate.GetCountDaysOfMonth()));
            myDate = new DateTime(2012, 3, 3);
            Console.WriteLine(String.Format("Number days in {0} : {1}", myDate.ToString("dd/MM/yyyy"), myDate.GetCountDaysOfMonth()));
            myDate = new DateTime(2012, 4, 1);
            Console.WriteLine(String.Format("Number days in {0} : {1}", myDate.ToString("dd/MM/yyyy"), myDate.GetCountDaysOfMonth()));
            myDate = new DateTime(2012, 7, 7);
            Console.WriteLine(String.Format("Number days in {0} : {1}", myDate.ToString("dd/MM/yyyy"), myDate.GetCountDaysOfMonth()));

            myDate = new DateTime(1973, 10, 3);
            DateTime myDate2 = new DateTime(2013, 10, 3);
            Console.WriteLine(String.Format("Age from {0} to {1} : {2}", myDate.ToString("dd/MM/yyyy"), myDate2.ToString("dd/MM/yyyy"), myDate.CalculateAge(myDate2)));
            Console.WriteLine(String.Format("Age from {0} now : {1}", myDate.ToString("dd/MM/yyyy"), myDate.CalculateAge()));
            Console.WriteLine(String.Format("Week number of {0} : {1}", myDate.ToString("dd/MM/yyyy"), myDate.GetWeekOfYear()));

            Console.WriteLine("isDate 01/01/2018 ? {0}", "01/01/2018".IsDate());
            Console.WriteLine("isDate 29/02/2016 ? {0}", "29/02/2016".IsDate());
            Console.WriteLine("isDate 29/02/2017 ? {0}", "29/02/2017".IsDate());
            Console.WriteLine("isDate 01/01/2018test ? {0}", "01/01/2018test".IsDate());
            Console.WriteLine("isDate 32/01/2018 ? {0}", "32/01/2018".IsDate());
            Console.WriteLine("isDate 01/13/2018 ? {0}", "01/13/2018".IsDate());
            Console.WriteLine("22:03:34 -> {0}", "22:03:34".StringToTimeSpan());

            Console.WriteLine("23 March 2003 {0}", "23 March 2003".IsDate());
            Console.WriteLine("1 August -2003 {0}", "1 August -2003".IsDate());
            Console.WriteLine("2013-1-25 {0}", "2013-1-25".IsDate());
            Console.WriteLine("May 30, 2009 {0}", "May 30, 2009".IsDate());
            Console.WriteLine("23 JulyAugust 2003 {0}", "23 JulyAugust 2003".IsDate());
            Console.WriteLine("38 August 2003 {0}", "38 August 2003".IsDate());
            Console.WriteLine("2013-13-25 {0}", "2013-13-25".IsDate());
            Console.WriteLine("2013-10-92 {0}", "2013-10-92".IsDate());
            Console.WriteLine("-2013-1-3 {0}", "-2013-1-3".IsDate());
            Console.WriteLine("May 32, 2009 {0}", "May 32, 2009".IsDate());
            Console.WriteLine("One flew over the Cookoo's nest {0}", "One flew over the Cookoo's nest".IsDate());
            Console.WriteLine("'' {0}", "".IsDate());
            Console.WriteLine("empty string {0}", string.Empty.IsDate());
            Console.WriteLine("null string {0}", ((string)null).IsDate());

            Console.WriteLine("Last day of month Today : {0}", DateTime.Today.GetLastDayOfMonth());
            Console.WriteLine("Last day of month 01/02/2016 : {0}", new DateTime(2016, 2, 1).GetLastDayOfMonth());

            Console.WriteLine("isDateBetween : today bewteen 01/01/2017 and 31/07/2017 : {0}", DateTime.Today.IsBetween(new DateTime(2017, 1, 1), new DateTime(2017, 07, 31)));
            Console.WriteLine("isDateBetween : today bewteen 01/01/2017 and 31/07/2099 : {0}", DateTime.Today.IsBetween(new DateTime(2017, 1, 1), new DateTime(2099, 07, 31)));
            Console.WriteLine("isDateBetween : today bewteen 01/01/2018 and 31/07/2017 : {0}", DateTime.Today.IsBetween(new DateTime(2018, 1, 1), new DateTime(2017, 07, 31)));

        }

        static void TestBoolean()
        {
            Console.WriteLine("BOOLAN");
            Console.WriteLine("------");
            Console.WriteLine($"TRUE : {true.GetBoolString()}");
            Console.WriteLine($"FALSE : {false.GetBoolString()}");
            Console.WriteLine("y : {0}", "y".AsBoolean());
            Console.WriteLine("n : {0}", "n".AsBoolean());
            Console.WriteLine("non : {0}", "non".AsBoolean());
            Console.WriteLine("vrai : {0}", "vrai".AsBoolean());
            Console.WriteLine("faux : {0}", "faux".AsBoolean());
            Console.WriteLine("t : {0}", "t".AsBoolean());
            Console.WriteLine("1 : {0}", "1".AsBoolean());
        }

        static void TestNumber()
        {
            Console.WriteLine("NUMBER");
            Console.WriteLine("------");
            Console.WriteLine($"5 is between 1 and 10? : {5.IsBetween(1, 10)}");
            Console.WriteLine($"11 is between 1 and 10? : {11.IsBetween(1, 10)}");
        }

        static void TestDirectoryInfo()
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            System.IO.FileInfo[] fis = di.GetFiles();

            Console.WriteLine(string.Format("Number of files : {0}", di.GetFiles().Length));
            Console.WriteLine(string.Format("Number of files dll + txt: {0}", di.GetFiles("*.dll", "*.txt").Length));

        }

        static void TestEnum()
        {
            Console.WriteLine("ENUM");
            Console.WriteLine("----");
            Console.WriteLine($"{TraficLightsColors.Blue} = {TraficLightsColors.Blue.GetEnumDescription()}");
            Console.WriteLine($"{TraficLightsColors.Green} = {TraficLightsColors.Green.GetEnumDescription()}");
            Console.WriteLine($"{TraficLightsColors.Red} = {TraficLightsColors.Red.GetEnumDescription()}");
            Console.WriteLine("");
            string stop = "Indicates Stop";
            Console.WriteLine($"Indicates Stop = {stop.ToEnum<TraficLightsColors>()}");
        }

        static void TestEnumerable()
        {
            var list = new List<Customer>();

            Console.WriteLine("ENUMERABLE");
            Console.WriteLine("----------");

            var personList = new List<Person>();
            personList.Add(new Person
            {
                FirstName = "Alex",
                LastName = "Friedman",
                Age = 27
            });
            personList.Add(new Person
            {
                FirstName = "Jack",
                LastName = "Bauer",
                Age = 45

            });

            personList.Add(new Person
            {
                FirstName = "Cloe",
                LastName = "O'Brien",
                Age = 35
            });
            personList.Add(new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 30
            });

            Console.WriteLine("ToHtmlTable");
            Console.WriteLine("-----------");
            string html = @"<style type = ""text/css""> .tableStyle{border: solid 5 green;} 
th.header{ background-color:#FF3300} tr.rowStyle { background-color:#33FFFF; 
border: solid 1 black; } tr.alternate { background-color:#99FF66; 
border: solid 1 black;}</style>";
            html += personList.ToHtmlTable("tableStyle", "header", "rowStyle", "alternate");
            Console.WriteLine(html);


            Console.WriteLine("");
            Console.WriteLine("OrderBy");
            Console.WriteLine("-------");
            list.Add(new Customer() { Name = "Doe" });
            list.Add(new Customer() { Name = "Skywalker" });
            list.Add(new Customer() { Name = "Bond" });

            var result = list.OrderBy("Name desc");
            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
        }

        static void TestDBCommand()
        {
            string ConnectionString = "Data Source=:memory:;Version=3;New=True;";
            string sql;
            using (SQLiteConnection cnx = new SQLiteConnection(ConnectionString))
            {
                cnx.Open();

                string sqlTablePerson = "create table person (name varchar(20), age int)";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlTablePerson, cnx))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "insert into person (name, age) values ('John', 30)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, cnx))
                {
                    cmd.ExecuteNonQuery();
                }

                using (SQLiteCommand cmd = new SQLiteCommand("select * from person where name = @name", cnx))
                {
                    var parameter = cmd.CreateParameter();
                    parameter.ParameterName = "@name";
                    parameter.Value = "John";
                    cmd.Parameters.Add(parameter);
                    Console.WriteLine(cmd.ActualCommandText());
                }

                string sqlTableTeacher = "create table teacher (name varchar(20))";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlTableTeacher, cnx))
                {
                    cmd.ExecuteNonQuery();
                }

            }
        }

        static void TestCollection()
        {
            List<Customer> customers = new List<Customer>();

            Customer customer = new Customer()
            {
                Age = 10,
                Name = "John Doe"
            };

            //Generic condition
            customers.AddIf(() => TodayIsFriday(), customer);

            //Item specific condition
            customers.AddIf(p => p.Age >= 8, customer);

            customer.Age = 6;
            customers.AddIf(p => p.Age >= 8, customer);


            var items = new List<TestItem> {
            new TestItem {ItemID = 1, ItemName = "TestItem"},
            new TestItem {ItemID = 2, ItemName = "Wigit"},
            new TestItem {ItemID = 3, ItemName = "TestItem2"},
            new TestItem {ItemID = 4, ItemName = "Foo"},
            new TestItem {ItemID = 5, ItemName = "Bar"},
            new TestItem {ItemID = 6, ItemName = "TestFooBarItem"}
      };

            Console.WriteLine("Items starting with Test using delegate");
            Func<TestItem, bool> itemNameFilter = delegate (TestItem testItem) { return testItem.ItemName.StartsWith("Test"); };
            foreach (var testItem in items.Filter(itemNameFilter))
            {
                Console.WriteLine(testItem.ItemName);
            }

            Console.WriteLine("Items with ItemName containing Item and ItemID > 2 using Lamda Expression");
            foreach (var testItem in items.Filter(x => x.ItemName.StartsWith("Test") && x.ItemID > 2))
            {
                Console.WriteLine(testItem.ItemName);
            }


            List<Customer> custs = new List<Customer>{
        new Customer {FirstName = "Peggy", AcctBalance = 12442.98},
        new Customer {FirstName = "Sally", AcctBalance = 32.39},
        new Customer {FirstName = "Billy", AcctBalance = 25.33},
        new Customer {FirstName = "Tommy", AcctBalance = 12345}
        };

            bool showAccountBalancesUnder5000 = false;

            var custList = custs.WhereIf(showAccountBalancesUnder5000, c => c.AcctBalance < 5000).ToList(); //will not perform the filtering

            showAccountBalancesUnder5000 = true;

            var custListUnder5000 = custs.WhereIf(showAccountBalancesUnder5000, c => c.AcctBalance < 5000).ToList(); //will perform the filtering
        }

        static bool TodayIsFriday()
        {
            return DateTime.Today.DayOfWeek == DayOfWeek.Friday;
        }

    }

}

class Customer
{
    public string Name { get; set; }
    public string FirstName { get; set; }
    public int Age { get; set; }
    public double AcctBalance { get; set; }
}

class TestItem
{
    public int ItemID { get; set; }
    public string ItemName { get; set; }
}

