using System;
using System.Text;
using System.Text.RegularExpressions;

namespace app_c
{
    public partial class hw1 : Form
    {
        public hw1()
        {
            InitializeComponent();
        }

        public static string NumberToRoman(int number)
        {
            if (number < 0 || number > 1000)
                throw new ArgumentException("Value must be in the range 0 - 1000.");

            if (number == 0) return "N";

            int[] values = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] numerals = new string[]
            { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < 13; i++)
            {
                while (number >= values[i])
                {
                    number -= values[i];
                    result.Append(numerals[i]);
                }
            }

            return result.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox2.Clear();
            this.textBox3.Clear();


            Dictionary<string, int> units_dict = new Dictionary<string, int>
            {
                ["null"] = 0,["ein"] = 1, ["einer"] = 1,["zwei"] = 2,["drei"] = 3,
                ["vier"] = 4,["fünf"] = 5,["sechs"] = 6,["sieben"] = 7,["acht"] = 8,["neun"] = 9
            };

            Dictionary<string, int> special_tens_dict = new Dictionary<string, int>
            {
                ["elf"] = 11, ["zwölf"] = 12, ["dreizehn"] = 13, ["vierzehn"] = 14, ["fünfzehn"] = 15,
                ["sechszehn"] = 16, ["siebzehn"] = 17, ["achtzehn"] = 18, ["neunzehn"] = 19
            };

            Dictionary<string, int> dozens_dict = new Dictionary<string, int>
            {
                ["zehn"] = 10, ["zwanzig"] = 20, ["dreißig"] = 30, ["vierzig"] = 40,
                ["fünfzig"] = 50, ["sechzig"] = 60, ["siebzig"] = 70, ["achtzig"] = 80, ["neunzig"] = 90
            };

            Dictionary<string, int> units = new Dictionary<string, int>
            {
                ["null"] = 0,
                ["ein"] = 1,
                ["einer"] = 1,
                ["einer"] = 1,
                ["zwei"] = 2,
                ["drei"] = 3,
                ["vier"] = 4,
                ["fünf"] = 5,
                ["sechs"] = 6,
                ["sieben"] = 7,
                ["acht"] = 8,
                ["neun"] = 9,
                ["zehn"] = 10,
                ["elf"] = 11,
                ["zwölf"] = 12,
                ["dreizehn"] = 13,
                ["vierzehn"] = 14,
                ["fünfzehn"] = 15,
                ["sechszehn"] = 16,
                ["siebzehn"] = 17,
                ["achtzehn"] = 18,
                ["neunzehn"] = 19,
                ["zwanzig"] = 20,
                ["dreißig"] = 30,
                ["vierzig"] = 40,
                ["fünfzig"] = 50,
                ["sechzig"] = 60,
                ["siebzig"] = 70,
                ["achtzig"] = 80,
                ["neunzig"] = 90,
                ["hundert"] = 100
            };

            string enterede_string = this.textBox1.Text;
            enterede_string = Regex.Replace(enterede_string, @"\s+", " ");
            string[] line = enterede_string.Split(' ');

            int count = 0;

            bool flag = true;
            int units_count = 0;
            int special_tens_count = 0;
            int dozens_count = 0;
            int hundert_count = 0;


            //ошибки

            for (int i = 0; i < line.Length; i++)
            {
                if (line.Length == 1)
                {
                    if (units.ContainsKey(line[i]) || special_tens_dict.ContainsKey(line[i]) || dozens_dict.ContainsKey(line[i]))
                        continue;

                    else
                    {
                        this.textBox2.Text = "В данном случае могут стоять только единицы и числа [10-19,20,30,...,90]";
                        flag = false;
                        break;
                    }

                }

                if (line.Length >= 2)
                {
                    //десятки
                    if (dozens_dict.ContainsKey(line[i]) && i != line.Length - 1 && dozens_dict.ContainsKey(line[i+1]))
                    {
                        this.textBox2.Text = "Два разряда десятков не могут идти подряд";
                        flag = false;
                        break;
                    }
                    

                    if(dozens_dict.ContainsKey(line[i]) && i != line.Length - 1 && special_tens_dict.ContainsKey(line[i + 1]))
                    {
                        this.textBox2.Text = "Число [11-19] не может стоять после разряда десятков";
                        flag = false;
                        break;
                    }


                    if (units_dict.ContainsKey(line[i]) && i != line.Length - 1 && dozens_dict.ContainsKey(line[i + 1]))
                    {
                        this.textBox2.Text = "Разряд десятков не может стоять после разряда единиц. " +
                            "Между числом единичного формата и числом десятичного формата необходимо поставить 'und'";
                        flag = false;
                        break;
                    }


                    if (dozens_dict.ContainsKey(line[i]) && i != line.Length - 1 && units_dict.ContainsKey(line[i + 1]))
                    {
                        this.textBox2.Text = "Разряд десятков не может стоять перед разрядом единиц";
                        flag = false;
                        break;
                    }


                    if (dozens_dict.ContainsKey(line[i]) && i != line.Length - 1 && line[i + 1] == "und")
                    {
                        this.textBox2.Text = "'und' не может стоять после разряда десятков";
                        flag = false;
                        break;
                    }


                    if (units_dict.ContainsKey(line[i]) && i < line.Length - 2 && line[i + 1] == "und" && line[i + 2] == "zehn")
                    {
                        this.textBox2.Text = "Неправильно образовано число [11-19]. Для обозначения этих чисел существуют специальные слова";
                        flag = false;
                        break;
                    }


                    if (dozens_dict.ContainsKey(line[i]) && i != line.Length - 1 && line[i + 1] == "hundert")
                    {
                        this.textBox2.Text = "Разряд сотен не может стоять после разряда десятков";
                        flag = false;
                        break;
                    }


                    //единицы

                    if (units_dict.ContainsKey(line[i]) && i != line.Length - 1 && units_dict.ContainsKey(line[i + 1]) )
                    {
                        this.textBox2.Text = "Два разряда единиц не могут идти подряд";
                        flag = false;
                        break;
                    }


                    //11-19

                    if (special_tens_dict.ContainsKey(line[i]) && i != line.Length - 1 && units_dict.ContainsKey(line[i + 1]))
                    {
                        this.textBox2.Text = "Разряд единиц не может стоять после чисел [11-19]";
                        flag = false;
                        break;
                    }


                    if (special_tens_dict.ContainsKey(line[i]) && i != line.Length - 1 && dozens_dict.ContainsKey(line[i + 1]))
                    {
                        this.textBox2.Text = "Разряд десятков не может стоять после чисел [11-19]";
                        flag = false;
                        break;
                    }


                    if (special_tens_dict.ContainsKey(line[i]) && i != line.Length - 1 && line[i + 1] == "hundert")
                    {
                        this.textBox2.Text = "Разряд сотен не может стоять после чисел [11-19]";
                        flag = false;
                        break;
                    }


                    if (special_tens_dict.ContainsKey(line[i]) && i != line.Length - 1 && special_tens_dict.ContainsKey(line[i + 1]))
                    {
                        this.textBox2.Text = "Числа [11-19] не могут идти подряд";
                        flag = false;
                        break;
                    }


                    if (units_dict.ContainsKey(line[i]) && i != line.Length - 1 && special_tens_dict.ContainsKey(line[i + 1]))
                    {
                        this.textBox2.Text = "Число [11-19] не может стоять после разряда единиц";
                        flag = false;
                        break;
                    }


                    //und

                    if (i == 0 && line[i] == "und")
                    {
                        this.textBox2.Text = "'und' не может стоять на первой позиции";
                        flag = false;
                        break;
                    }


                    if (line[i] == "und" && i != line.Length - 1 && line[i + 1] == "und")
                    {
                        this.textBox2.Text = "Ошибка: невозможно поставить два 'und' подряд";
                        flag = false;
                        break;
                    }


                    if (line[i] == "und" && i != line.Length - 1 && units_dict.ContainsKey(line[i + 1]))
                    {
                        this.textBox2.Text = "'und' не может стоять перед разрядом единиц";
                        flag = false;
                        break;
                    }


                    if (line[i] == "und" && i != line.Length - 1 && special_tens_dict.ContainsKey(line[i + 1]))
                    {
                        this.textBox2.Text = "'und' не может стоять перед числами [11-19]";
                        flag = false;
                        break;
                    }


                    if (line[i] == "und" && i == line.Length - 1)
                    {
                        this.textBox2.Text = "'und' не может стоять на последнем месте.";
                        flag = false;
                        break;
                    }


                    if (special_tens_dict.ContainsKey(line[i]) && i != line.Length - 1 && line[i + 1] == "und")
                    {
                        this.textBox2.Text = "'und' не может стоять после чисел [11-19]";
                        flag = false;
                        break;
                    }


                    if (dozens_dict.ContainsKey(line[i]) && i != line.Length - 1 && line[i + 1] == "und")
                    {
                        this.textBox2.Text = "'und' не может стоять после разряда десятков";
                        flag = false;
                        break;
                    }


                    if (line[i] == "hundert" && i != line.Length - 1 && line[i + 1] == "und")
                    {
                        this.textBox2.Text = "'und' не может стоять после разряда сотен";
                        flag = false;
                        break;
                    }


                    if (line[i] == "und" && i != line.Length - 1 && line[i + 1] == "hundert")
                    {
                        this.textBox2.Text = "'und' не может стоять перед разрядом сотен";
                        flag = false;
                        break;
                    }


                    // сотни

                    if (line[i] == "hundert" && i != line.Length - 1 && line[i + 1] == "hundert")
                    {
                        this.textBox2.Text = "Ошибка: два разряда сотен не могут идти подряд";
                        flag = false;
                        break;
                    }


                    // all

                    if (!units.ContainsKey(line[i]) && line[i] != "und")
                    {
                        this.textBox2.Text = "Ошибка: слово '" + line[i] + "' введено некорректно.";
                        flag = false;
                        break;
                    }

                }


                if (units_dict.ContainsKey(line[i]))
                    units_count += 1;

                if (special_tens_dict.ContainsKey(line[i]))
                    special_tens_count += 1;

                if (dozens_dict.ContainsKey(line[i]))
                    dozens_count += 1;

                if (line[i] == "hundert")
                    hundert_count += 1;

            }


            if (units_count > 2)
            {
                this.textBox2.Text = "В числе не может быть больше 2-х разрядов единиц";
                flag = false;
            }

            if (special_tens_count > 1)
            {
                this.textBox2.Text = "В числе не могут встречаться числа [11-19] больше 1-го раза";
                flag = false;
            }

            if (dozens_count > 1)
            {
                this.textBox2.Text = "В числе не может быть больше 1-го разряда десятков";
                flag = false;
            }

            if (hundert_count > 1)
            {
                this.textBox2.Text = "В числе не может быть больше 1-го разряда сотен";
                flag = false;
            }

            


            // Находим число

            if (flag)
            {
                if (line.Length == 1)
                {
                    for (int it = 0; it < line.Length; it++)
                    {
                        string new_line = String.Join(" ", line[it].ToString());

                        foreach (KeyValuePair<string, int> s in units)
                        {
                            if (new_line == s.Key)
                                count += units[s.Key];
                        }
                    }
                }

                else
                {
                    if (line[1] == "hundert")
                    {
                        string new_line = String.Join(" ", line[0].ToString());
                        foreach (KeyValuePair<string, int> s in units_dict)
                        {
                            if (new_line == s.Key)
                                count += units_dict[s.Key] * 100;
                        }
                        for (int it = 2; it < line.Length; it++)
                        {
                            new_line = String.Join(" ", line[it].ToString());
                            foreach (KeyValuePair<string, int> s in units)
                            {
                                if (new_line == s.Key)
                                    count += units[s.Key];
                            }
                        }
                    }

                    else
                    {
                        for (int it = 0; it < line.Length; it++)
                        {
                            string new_line = String.Join(" ", line[it].ToString());
                            foreach (KeyValuePair<string, int> s in units)
                            {
                                if (new_line == s.Key)
                                    count += units[s.Key];
                            }
                        }
                    }
                }

                this.textBox3.Text = NumberToRoman(count);
                this.textBox2.Text = count.ToString();
            }

        }

       
    }
}