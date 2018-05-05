using Presentation;
using System.Collections;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(IList), typeof(string))]
    public class FloatListToStringConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            IList ListValue = value as IList;
            if (ListValue == null)
                return "";

            string Result = "";

            foreach (object Item in ListValue)
                if (Item is float)
                {
                    if (Result.Length > 0)
                        Result += ", ";

                    Result += (float)Item;
                }

            return Result;
        }
    }
}
