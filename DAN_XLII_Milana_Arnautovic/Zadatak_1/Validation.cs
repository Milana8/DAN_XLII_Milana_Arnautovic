using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
   public  class Validation
    {
        /// <summary>
        /// Method for extracting date of birth from JMBG
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string GetDateFromJMBG(string s)
        {
            //If fifth digit is 0 than person is born in 2000 or later
            if (Convert.ToInt16(s.Substring(4, 1)) == 0)
            {
                return "2" + s.Substring(4, 3) + "-" + s.Substring(2, 2) + "-" + s.Substring(0, 2);
            }
            else
            {
                return "1" + s.Substring(4, 3) + "-" + s.Substring(2, 2) + "-" + s.Substring(0, 2);
            }
        }

        
        public bool JMBGisValid(string JMBG, out DateTime dateOfBirth)
        {
            dateOfBirth = new DateTime();
            if (JMBG.Length != 13)
                return false;

            DateTime now = DateTime.Now;
            for (int i = 0; i < JMBG.Length; i++)
            {
                if (!Char.IsNumber(JMBG, i))
                    return false;
            }

            int year = 0;

            if (Char.GetNumericValue(JMBG[4]) == 0)
            {

                year = 2000 + 10 * (int)Char.GetNumericValue(JMBG[5]) + (int)Char.GetNumericValue(JMBG[6]);

                if (year > now.Year)
                {

                    return false;
                }

            }
            else if (Char.GetNumericValue(JMBG[4]) == 9)
            {
                year = 1900 + 10 * (int)Char.GetNumericValue(JMBG[5]) + (int)Char.GetNumericValue(JMBG[6]);
            }
            else
            {
                return false;
            }

            int month = (int)Char.GetNumericValue(JMBG[2]) * 10 + (int)Char.GetNumericValue(JMBG[3]);


            if (year == now.Year && month > now.Month)
            {
                return false;
            }

            if (month == 0 || month > 12)
                return false;

            int day = (int)Char.GetNumericValue(JMBG[0]) * 10 + (int)Char.GetNumericValue(JMBG[1]);

            if (year == now.Year && month == now.Month && day >= now.Day)
            {
                return false;
            }

            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                if (day == 0 || day > 31)
                    return false;
            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                if (day == 0 || day > 30)
                    return false;
            }
            else if (month == 2)
            {
                if (DateTime.IsLeapYear(year))
                {
                    if (day == 0 || day > 29)
                        return false;
                }
                else
                {
                    if (day == 0 || day > 28)
                        return false;
                }

            }
            dateOfBirth = new DateTime(year, month, day);
            return true;
        }

       
        public string GenetrateRegisterNumber()
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 9; i++)
            {
                int randomNumber = random.Next(10);
                sb.Append(randomNumber);
            }
            return sb.ToString();

        }

       
        public string GenetratePhoneNumber()
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                int randomNumber = random.Next(7);
                sb.Append(randomNumber);
            }
            return sb.ToString();

        }
    }
}
