namespace Xamarin.Forms.Essentials.Controls
{
    [ContentProperty("Content")]
    [Preserve(AllMembers = true)]

    public class  StateCondition : View
    {
        public object State { get; set; }
        public View Content { get; set; }
    }
}