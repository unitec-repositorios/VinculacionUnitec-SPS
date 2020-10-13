using System;
using System.Collections.Generic;
using System.Text;

namespace BotPuroSqlServer
{
     
    public class Cache
    {
    StudentInfo[] MemoryCache = new StudentInfo[25];

    int fillCacheDirection = 0;

    int cachePosition = 0;

        public bool existInCache(string telegramUser)
        {

            for (int i = 0; i < 50; i++)
            {
                if (MemoryCache[i].telegramUser == telegramUser)
                {
                    return true;
                }
            }

            return false;
        }

        public  string getHours(string accountNumber)
        {

            for (int i = 0; i < 50; i++)
            {
                if (MemoryCache[i].accountNumber == accountNumber)
                {
                    return MemoryCache[i].hours;
                }
            }

            return "0";

        }


        public string getHoursDetails(string accountNumber)
        {

            for (int i = 0; i < 50; i++)
            {
                if (MemoryCache[i].accountNumber == accountNumber)
                {
                    return MemoryCache[i].proyectDetails;
                }
            }

            return "0";

        }

        public void addToCache(string _telegramUser, string _accountNumber, string _name, string _proyectDetails, string _hours)
        {


            StudentInfo newStudentInfo = new StudentInfo();


            newStudentInfo.telegramUser = _telegramUser;
            newStudentInfo.accountNumber = _accountNumber;
            newStudentInfo.name = _name;
            newStudentInfo.proyectDetails = _proyectDetails;
            newStudentInfo.hours = _hours;

            MemoryCache[cachePosition] = newStudentInfo;

            if (fillCacheDirection == 0 && cachePosition == 0)
            {

                cachePosition++;
            }
            else if (fillCacheDirection == 1 && cachePosition == 0)
            {

                fillCacheDirection = 0;
                cachePosition++;
            }
            else if (fillCacheDirection == 0 && cachePosition == 49)
            {

                fillCacheDirection = 1;
                cachePosition--;
            }
            else if (fillCacheDirection == 1 && (cachePosition < 49 && cachePosition > 0))
            {

                cachePosition--;
            }
            else if (fillCacheDirection == 0 && (cachePosition < 49 && cachePosition > 0))
            {

                cachePosition++;
            }

        }


    }
}
