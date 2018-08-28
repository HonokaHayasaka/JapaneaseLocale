using System;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Globalization;
using System.Collections.Generic;

namespace Honoka.JapanLocale.Activities
{
    public class ToJapaneaseEra : CodeActivity
    {
        /// <summary>
        /// Input Date
        /// </summary>
        [Category("Input")]
        [RequiredArgument]
        [Description("DateTime Value")]
        public InArgument<DateTime> DateValue { get; set; }

        /// <summary>
        /// DateStyle
        /// </summary>
        [Category("Options")]
        [RequiredArgument]
        [Description("Set Output String Style : ggyy = full-expression era")]
        public InArgument<String> DateFormat { get; set; } = "ggyy年M月d日";

        /// <summary>
        /// DateTime as result
        /// </summary>
        [Category("Output")]
        [Description("Japanease Style Era Value")]
        public OutArgument<String> Result { get; set; }

        /// <summary>
        /// Execute Activity
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(CodeActivityContext context)
        {
            var ci = new CultureInfo("ja-JP", true);
            ci.DateTimeFormat.Calendar = new JapaneseCalendar();

            Result.Set(context, DateValue.Get(context).ToString(DateFormat.Get(context), ci));
        }
    }
}
