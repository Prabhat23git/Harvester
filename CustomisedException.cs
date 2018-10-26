using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat.Automation.UI.Utilities
{
    public class CustomisedException
    {
        private static String fieldValue = null;
        private static String errorMessage = null;


        public CustomisedException(String fieldValue, String errorMessage)
        {

            CustomisedException.fieldValue = fieldValue;
            CustomisedException.errorMessage = errorMessage;
        }

        public static String GetFieldValue()
        {
            return fieldValue;
        }

        public static void setFieldValue(String fieldValue)
        {
            CustomisedException.fieldValue = fieldValue;
        }

        public static String getErrorMessage()
        {
            return errorMessage;
        }

        public static void setErrorMessage(String errorMessage)
        {
            CustomisedException.errorMessage = errorMessage;
        }

    }
}
