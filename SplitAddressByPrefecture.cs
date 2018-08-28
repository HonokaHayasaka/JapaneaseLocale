using System;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace Honoka.JapanLocale.Activities
{
    /// <summary>
    /// Splits Japanease Style Address with Prefecture And Others
    /// </summary>
    [DisplayName("Split Japanease Address")]
    public class SplitAddressByPrefecture : CodeActivity
    {

        /// <summary>
        /// Input Data : Japanease Address, starts with Prefecture
        /// </summary>
        [Category("Input")]
        [Description("Address of Japan, Like 東京都新宿区西新宿2丁目8-1")]
        [RequiredArgument]
        public InArgument<string> AddressString { get; set; }

        /// <summary>
        /// if sets true, throws Exception with no match address.
        /// </summary>
        [Category("Option")]
        [Description("Throws Exception with no match")]
        public InArgument<bool> ThrowExceptionOnNoMatch { get; set; } = false;

        /// <summary>
        /// Result : Prefecture
        /// </summary>
        [Category("Output")]
        [Description("Sets with Japanease Prefecture, Like 東京都")]
        public OutArgument<string> Prefecture { get; set; }

        /// <summary>
        /// Result : Other Address
        /// </summary>
        [Category("Output")]
        [Description("Sets Address without Prefecture, Like 新宿区西新宿2丁目8-1")]
        public OutArgument<string> AddressWithoutPrefecture { get; set; }

        /// <summary>
        /// Executes Activity
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(CodeActivityContext context)
        {
            var address = AddressString.Get(context).TrimStart();

            var prefs = new string[] { "北海道"
                                       , "青森県", "岩手県", "宮城県", "秋田県", "山形県", "福島県"
                                       , "茨城県", "栃木県", "群馬県", "埼玉県", "千葉県", "東京都", "神奈川県"
                                       , "新潟県", "富山県", "石川県", "福井県", "山梨県", "長野県", "岐阜県", "静岡県", "愛知県"
                                       , "三重県", "滋賀県", "京都府", "大阪府", "兵庫県", "奈良県", "和歌山県"
                                       , "鳥取県", "島根県", "岡山県", "広島県", "山口県"
                                       , "徳島県", "香川県", "愛媛県", "高知県"
                                       , "福岡県", "佐賀県", "長崎県", "熊本県", "大分県", "宮崎県", "鹿児島県", "沖縄県" };

            foreach(string p in prefs)
            {
                if(address.StartsWith(p))
                {
                    Prefecture.Set(context,p);
                    AddressWithoutPrefecture.Set(context, p.Length < address.Length ? address.Substring(p.Length) : string.Empty);
                    return;
                }
            }

            // Not found from here.
            if(ThrowExceptionOnNoMatch.Get(context))
            {
                throw new ArgumentException("Prefecture No Match");
            }

            Prefecture.Set(context, string.Empty);
            AddressWithoutPrefecture.Set(context, address);

        }
    }
}
