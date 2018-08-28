using System;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Globalization;

namespace Honoka.JapanLocale.Activities
{
    [DisplayName("Convert From Japanease Era Date")]
    public class FromJapaneaseEra : CodeActivity
    {
        /// <summary>
        /// Input Date
        /// </summary>
        [Category("Input")]
        [RequiredArgument]
        [Description("Date Expression with Japanease Era")]
        public InArgument<String> Expression { get; set; }

        /// <summary>
        /// DateStyle
        /// </summary>
        [Category("Options")]
        [RequiredArgument]
        [Description("Set Input String Style")]
        public InArgument<String> DateFormat { get; set; } = "ggyy年M月d日";

        /// <summary>
        /// DateTime as result
        /// </summary>
        [Category("Output")]
        [Description("DateTime Value")]
        public OutArgument<DateTime> Result { get; set; }

        /// <summary>
        /// Executes Activity
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(CodeActivityContext context)
        {
            var ci = new CultureInfo("ja-JP", true);
            ci.DateTimeFormat.Calendar = new JapaneseCalendar();

            Result.Set(context, DateTime.ParseExact(Expression.Get(context), DateFormat.Get(context), ci));
        }
    }
}
