using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolDetailsSheets.Helper
{
    public enum TypeEnum
    {
Controller,
Fighter,
Mage ,
Marksman,
Slayer ,
Tank,
Specialist
    }
    static class EnumHelper {
        static string GetTypeEnum(TypeEnum type)
        {
            string result = "undefined";
            switch (type)
            {
                case TypeEnum.Controller:
                    result = "Controller";
                    break;
                case TypeEnum.Fighter:
                    result = "Fighter";
                    break;
                case TypeEnum.Mage:
                    result = "Mage";
                    break;
                case TypeEnum.Marksman:
                    result = "Marksman";
                    break;
                case TypeEnum.Slayer:
                    result = "Slayer";
                    break;
                case TypeEnum.Tank:
                    result = "Tank";
                    break;
                case TypeEnum.Specialist:
                    result = "Specialist";
                    break;
                default:
                 
                    break;
            }
            return result;
        }
    }
}
